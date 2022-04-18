using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static NiTools.Shortcuts.Constructors.FloatVectors;

namespace NiTools.Polygonizer{

using Vec3i = Vector3Int;
using Vec3f = Vector3;

/*
Minecraft-style block polygonizer. Not optimized.
*/
public struct BlockPolygonizer{
	//returns material idnex for a cell given its coordinates
	public delegate int MaterialIndexGetter(Vec3i arg);
	//returns true if there should be an face between central cell with centerMaterialIndex and 
	//neighboring cell with neighborMaterialIndex
	public delegate bool MaterialBarrierDelegate(int centerMaterialIndex, int neighborMaterialIndex);
	public delegate void QuadBuilderDelegate(Vec3f a, Vec3f b, Vec3f c, Vec3f d, int materialIndex);

	MaterialIndexGetter matGetter;
	MaterialBarrierDelegate barrierCheck;
	QuadBuilderDelegate quadDelegate;

	struct BarrierTableEntry{
		public Vec3i diff;
		public Vec3f dx;
		public Vec3f dy;
		public Vec3f pos;
		public BarrierTableEntry(Vec3i diff_, Vec3f dx_, Vec3f dy_, Vec3f pos_){
			diff = diff_;
			dx = dx_;
			dy = dy_;
			pos = pos_;
		}
	}

	static readonly BarrierTableEntry[] barrierTable = new[]{
		new BarrierTableEntry(new Vec3i(-1, 0, 0), vec3f(0.0f, 0.0f, -0.5f), vec3f(0.0f, 0.5f, 0.0f), vec3f(-0.5f, 0.0f, 0.0f)),
		new BarrierTableEntry(new Vec3i(1, 0, 0), vec3f(0.0f, 0.0f, 0.5f), vec3f(0.0f, 0.5f, 0.0f), vec3f(0.5f, 0.0f, 0.0f)),

		new BarrierTableEntry(new Vec3i(0, 0, -1), vec3f(0.5f, 0.0f, 0.0f), vec3f(0.0f, 0.5f, 0.0f), vec3f(0.0f, 0.0f, -0.5f)),
		new BarrierTableEntry(new Vec3i(0, 0, 1), vec3f(-0.5f, 0.0f, 0.0f), vec3f(0.0f, 0.5f, 0.0f), vec3f(0.0f, 0.0f, 0.5f)),

		new BarrierTableEntry(new Vec3i(0, 1, 0), vec3f(0.5f, 0.0f, 0.0f), vec3f(0.0f, 0.0f, 0.5f), vec3f(0.0f, 0.5f, 0.0f)),
		new BarrierTableEntry(new Vec3i(0, -1, 0), vec3f(-0.5f, 0.0f, 0.0f), vec3f(0.0f, 0.0f, 0.5f), vec3f(0.0f, -0.5f, 0.0f)),
	};

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
					var centerMat = matGetter(vertPos);
					foreach(var cur in barrierTable){
						var subMat = matGetter(vertPos + cur.diff);
						if (!barrierCheck(centerMat, subMat)){
							continue;
						}
						var pos = vec3(vertPos.x, vertPos.y, vertPos.z) + cur.pos;
						quadDelegate(
							pos - cur.dx + cur.dy,
							pos + cur.dx + cur.dy,
							pos + cur.dx - cur.dy,
							pos - cur.dx - cur.dy,
							centerMat
						);
					}
				}
			}
		}
	}

	public BlockPolygonizer(
		MaterialIndexGetter matGetter_, MaterialBarrierDelegate barrierCheck_,
		QuadBuilderDelegate quadDelegate_){
		matGetter = matGetter_;
		barrierCheck = barrierCheck_;
		quadDelegate = quadDelegate_;
	}
}

}//Polygonizer
