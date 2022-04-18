using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NiTools.Polygonizer.CustomTypes;

namespace NiTools.Polygonizer{

//using Vec3i = Vector3Int;
using Vec3f = Vector3;

/*
Minecraft-style block polygonizer. Not optimized.
*/
public struct GridPolygonizer{
	//returns material idnex for a cell given its coordinates
	public delegate int MaterialIndexGetter(Vec3i arg);

	//returns true if there should be an face between central cell with centerMaterialIndex and 
	//neighboring cell with neighborMaterialIndex
	public delegate bool MaterialBarrierDelegate(int centerMaterialIndex, int neighborMaterialIndex);

	//retruns "cell value" for given coord. Can be 1.0 for filled cell or -1.0 for empty one.
	public delegate float CellValueDelegateInt(Vec3i coord);

	//retruns "cell value" for given coord. Can be 1.0 for filled cell or -1.0 for empty one.
	public delegate float CellValueDelegate(Vec3f coord);

	public delegate void QuadBuilderDelegate(Vec3f a, Vec3f b, Vec3f c, Vec3f d, int materialIndex);

	MaterialIndexGetter matGetter;
	MaterialBarrierDelegate barrierCheck;
	CellValueDelegate cellValue;
	QuadBuilderDelegate quadDelegate;

	struct BarrierTableEntry{
		public Vec3i diff;
		public Vec3i cellPosBase;
		public Vec3i cellRight;
		public Vec3i cellUp;
		public BarrierTableEntry(Vec3i diff_, 
				Vec3i cellPosBase_, Vec3i cellRight_, Vec3i cellUp_){
			diff = diff_;
			cellPosBase = cellPosBase_;
			cellRight = cellRight_;
			cellUp = cellUp_;
		}
	}

	static Vec3i vec3i(int x_, int y_, int z_){
		return new Vec3i(x_, y_, z_);
	}

	static Vec3f vec3f(Vec3i arg){
		return new Vec3f(arg.x, arg.y, arg.z);
	}

	static Vec3f vec3f(float x, float y, float z){
		return new Vec3f(x, y, z);
	}

	static readonly BarrierTableEntry[] barrierTable = new[]{
		new BarrierTableEntry(
			vec3i(-1, 0, 0), 
			vec3i(-1, -1, 0), vec3i(0, 0, -1), vec3i(0, 1, 0)
		),
		new BarrierTableEntry(
			vec3i(1, 0, 0), 
			vec3i(0, -1, -1), vec3i(0, 0, 1), vec3i(0, 1, 0)
		),

		new BarrierTableEntry(
			vec3i(0, 0, -1), 
			vec3i(-1, -1, -1), vec3i(1, 0, 0), vec3i(0, 1, 0)
		),
		new BarrierTableEntry(
			vec3i(0, 0, 1), 
			vec3i(0, -1, 0), vec3i(-1, 0, 0), vec3i(0, 1, 0)
		),

		new BarrierTableEntry(
			vec3i(0, 1, 0), 
			vec3i(-1, 0, -1), vec3i(1, 0, 0), vec3i(0, 0, 1)
		),
		new BarrierTableEntry(
			vec3i(0, -1, 0), 
			vec3i(0, -1, -1), vec3i(-1, 0, 0), vec3i(0, 0, 1)
		),
	};

	static readonly Vec3i[] cellVertOffset = new Vec3i[]{
		vec3i(0, 0, 0),
		vec3i(1, 0, 0),
		vec3i(0, 0, 1),
		vec3i(1, 0, 1),
		vec3i(0, 1, 0),
		vec3i(1, 1, 0),
		vec3i(0, 1, 1),
		vec3i(1, 1, 1),
	};

	/*
	(idx & 1, idx & 4, idx & 2)
	*/
	static readonly Vec3f[] cellVertOffsetf = new Vec3f[]{
		vec3f(0.0f, 0.0f, 0.0f),
		vec3f(1.0f, 0.0f, 0.0f),
		vec3f(0.0f, 0.0f, 1.0f),
		vec3f(1.0f, 0.0f, 1.0f),
		vec3f(0.0f, 1.0f, 0.0f),
		vec3f(1.0f, 1.0f, 0.0f),
		vec3f(0.0f, 1.0f, 1.0f),
		vec3f(1.0f, 1.0f, 1.0f),
	};

	static readonly (int, int)[] cellEdgeIndexes = new (int, int)[]{
		(0, 1),
		(2, 3),
		(0, 2),
		(1, 3),
		(4, 5),
		(6, 7),
		(4, 6),
		(5, 7),
		(0, 4),
		(1, 5),
		(2, 6),
		(3, 7)
	};

	static readonly (Vec3i, Vec3i)[] cellEdgeOffsets = new (Vec3i, Vec3i)[]{
		(vec3i(0, 0, 0), vec3i(1, 0, 0)),
		(vec3i(0, 0, 1), vec3i(1, 0, 1)),
		(vec3i(0, 0, 0), vec3i(0, 0, 1)),
		(vec3i(1, 0, 0), vec3i(1, 0, 1)),

		(vec3i(0, 1, 0), vec3i(1, 1, 0)),
		(vec3i(0, 1, 1), vec3i(1, 1, 1)),
		(vec3i(0, 1, 0), vec3i(0, 1, 1)),
		(vec3i(1, 1, 0), vec3i(1, 1, 1)),

		(vec3i(0, 0, 0), vec3i(0, 1, 0)),
		(vec3i(1, 0, 0), vec3i(1, 1, 0)),
		(vec3i(0, 0, 1), vec3i(0, 1, 1)),
		(vec3i(1, 0, 1), vec3i(1, 1, 1)),
	};

	bool crossesZero(float startVal, float endVal){
		return (startVal > 0.0f) != (endVal > 0.0f);
	}

	Vec3f getIntersectPos(Vec3f startPos, Vec3f endPos, float startVal, float endVal){
		var edgeLerp = (0.0f - startVal)/(endVal - startVal);
		var pos = Vector3.Lerp(startPos, endPos, edgeLerp);
		return pos;
	}

	bool isFilledValue(float cellVal){
		return cellVal > 0.0f;
	}

	static Vec3f mul(Vec3f a, Vec3f b){
		return vec3f(a.x * b.x, a.y * b.y, a.z * b.z);
	}

	static Vec3f getCellVertCord(Vec3f corner, int idx, Vec3f cellSize){
		Vec3f result = corner;
		if ((idx & 0x1) != 0)
			result.x += cellSize.x;
		if ((idx & 0x2) != 0)
			result.z += cellSize.z;
		if ((idx & 0x4) != 0)
			result.y += cellSize.y;
		return result;
	}

	Vec3f sampleNormal(Vec3f point, float delta = 0.05f){
		var dx = vec3f(delta, 0.0f, 0.0f);
		var dy = vec3f(0.0f, delta, 0.0f);
		var dz = vec3f(0.0f, 0.0f, delta);

		var n = vec3f(
			cellValue(point + dx) - cellValue(point - dx),
			cellValue(point + dy) - cellValue(point - dy),
			cellValue(point + dz) - cellValue(point - dz)
		) / delta;
		n = n.normalized;
		return -n;
	}

	//Vec3f[] cellVertCoordBuffer = new Vec3f[8];
	//float[] cellVertValBuffer = new float[8];

	Vec3f getCellCenterSimple(Vec3f cellCorner, Vec3f cellSize, ref bool err){
		int numVerts = 0;
		//var cornerCoord = vec3f(cellCoord);
		Vec3f result = Vec3f.zero;//vec3f(cellCoord) + vec3f(0.5f, 0.5f, 0.5f);

		//System.Span<Vec3f> verts = stackalloc Vec3f[8];
		System.Span<Vec3f> cellVertCoordBuffer = stackalloc Vec3f[8];
		System.Span<float> cellVertValBuffer = stackalloc float[8];

		for(int i = 0; i < cellVertOffset.Length; i++){
			cellVertCoordBuffer[i] = cellCorner + mul(cellVertOffsetf[i], cellSize);
			cellVertValBuffer[i] = cellValue(cellVertCoordBuffer[i]);
		}

		foreach(var cellEdge in cellEdgeIndexes){
			var startIdx = cellEdge.Item1;
			var endIdx = cellEdge.Item2;

			var startVal = cellVertValBuffer[startIdx];
			var endVal = cellVertValBuffer[endIdx];
			if (!crossesZero(startVal, endVal))
				continue;
			var startPos = cellVertCoordBuffer[startIdx];
			var endPos = cellVertCoordBuffer[endIdx];

			var intersectPos = getIntersectPos(startPos, endPos, startVal, endVal);

			numVerts++;
			result += intersectPos;
		}

		if (numVerts == 0){
			Debug.Log($"Zero vert coords: {cellCorner}");
			err = true;
			return cellCorner + mul(vec3f(0.5f, 0.5f, 0.5f), cellSize);
		}

		return result / numVerts;
	}

	Vec3f getCellCenterDc(Vec3f cellCorner, Vec3f cellSize, ref bool err){
		int numEdges = 0;
		//var cornerCoord = vec3f(cellCoord);
		Vec3f result = Vec3f.zero;//vec3f(cellCoord) + vec3f(0.5f, 0.5f, 0.5f);

		//System.Span<Vec3f> verts = stackalloc Vec3f[8];
		System.Span<Vec3f> cellVertCoordBuffer = stackalloc Vec3f[8];
		System.Span<float> cellVertValBuffer = stackalloc float[8];

		for(int i = 0; i < cellVertOffset.Length; i++){
			cellVertCoordBuffer[i] = cellCorner + mul(cellVertOffsetf[i], cellSize);
			cellVertValBuffer[i] = cellValue(cellVertCoordBuffer[i]);
		}

		System.Span<Vec3f> edgePositions = stackalloc Vec3f[12];
		System.Span<Vec3f> edgeNormals = stackalloc Vec3f[12];
		//int numEdgePositions = 0;

		foreach(var cellEdge in cellEdgeIndexes){
			var startIdx = cellEdge.Item1;
			var endIdx = cellEdge.Item2;

			var startVal = cellVertValBuffer[startIdx];
			var endVal = cellVertValBuffer[endIdx];
			if (!crossesZero(startVal, endVal))
				continue;
			var startPos = cellVertCoordBuffer[startIdx];
			var endPos = cellVertCoordBuffer[endIdx];

			var intersectPos = getIntersectPos(startPos, endPos, startVal, endVal);

			edgePositions[numEdges] = intersectPos;
			edgeNormals[numEdges] = sampleNormal(intersectPos);
			numEdges++;

			result += intersectPos;
		}

		if (numEdges == 0){
			Debug.Log($"Zero vert coords: {cellCorner}");
			err = true;
			return cellCorner + mul(vec3f(0.5f, 0.5f, 0.5f), cellSize);
		}

		result = result / numEdges;

		int numCycles = 20;
		for(int cycleIdx = 0; cycleIdx < numCycles; cycleIdx++){
			var force = Vec3f.zero;
			for(int edgeIdx = 0; edgeIdx < numEdges; edgeIdx++){
				var diff = result - edgePositions[edgeIdx];
				var diffProj = Vector3.Project(diff, edgeNormals[edgeIdx]);
				force += diffProj;
			}
			force *= 0.25f;
			result -= force;
		}

		return result;
	}

	Vec3f getCellCenter(Vec3f pos, Vec3f size, ref bool err){
		//return getCellCenterSimple(pos, size, ref err);
		return getCellCenterDc(pos, size, ref err);
	}

	void polygonizeVertex(Vec3i vertPos){
		var centerMat = matGetter(vertPos);
		var cellPos = vec3f(vertPos);
		var vertVal = cellValue(cellPos);
		if (!isFilledValue(vertVal))
			return;
		var cellSize = Vec3f.one;
		foreach(var cur in barrierTable){
			var neighborPos = vec3f(vertPos + cur.diff);
			var neighborVal = cellValue(neighborPos);
			if (!crossesZero(vertVal, neighborVal))
				continue;

			var faceCenter = getIntersectPos(cellPos, neighborPos, vertVal, neighborVal);
			
			var subMat = matGetter(vertPos + cur.diff);

			var dx = cur.cellRight;
			var dy = cur.cellUp;

			bool err = false;
			var a = getCellCenter(vec3f(vertPos + cur.cellPosBase + cur.cellUp), cellSize, ref err);
			var b = getCellCenter(vec3f(vertPos + cur.cellPosBase + cur.cellUp + cur.cellRight), cellSize, ref err);
			var c = getCellCenter(vec3f(vertPos + cur.cellPosBase + cur.cellRight), cellSize, ref err);
			var d = getCellCenter(vec3f(vertPos + cur.cellPosBase), cellSize, ref err);
			if (err){
				Debug.Log($"err at: {vertPos}, cellVal: {vertVal}, neighbor: {neighborPos}, neighborVal: {neighborVal}");
			}

			quadDelegate(
				a, b, c, d,
				centerMat
			);
		}

	}

	public void build(int x1, int y1, int z1, int x2, int y2, int z2){
		build(vec3i(x1, y1, z1), vec3i(x2, y2, z2));
	}

	public void build(Vec3i minCoord, Vec3i maxCoord){
		if ((matGetter == null) || (quadDelegate == null) || (barrierCheck == null))
			throw new System.ArgumentNullException();

		{
			var tmp1 = minCoord;
			var tmp2 = maxCoord;
			minCoord = Vec3i.Min(tmp1, tmp2);
			maxCoord = Vec3i.Max(tmp1, tmp2);
		}

		var iDiff = maxCoord - minCoord;
		if ((iDiff.x < 2)||(iDiff.y < 2)||(iDiff.z < 2))
			return;

		Vec3i vertPos = new();
		for(vertPos.y = minCoord.y + 1; vertPos.y < maxCoord.y; vertPos.y++){
			for(vertPos.z = minCoord.z + 1; vertPos.z < maxCoord.z; vertPos.z++){
				for(vertPos.x = minCoord.x + 1; vertPos.x < maxCoord.x; vertPos.x++){
					polygonizeVertex(vertPos);
				}
			}
		}
	}

	public GridPolygonizer(
		MaterialIndexGetter matGetter_, 
		CellValueDelegate cellValue_,
		MaterialBarrierDelegate barrierCheck_,
		QuadBuilderDelegate quadDelegate_){
		matGetter = matGetter_;
		cellValue = cellValue_;
		barrierCheck = barrierCheck_;
		quadDelegate = quadDelegate_;
	}
}

}//Polygonizer
