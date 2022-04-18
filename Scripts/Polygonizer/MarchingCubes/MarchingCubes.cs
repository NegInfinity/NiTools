using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace NiTools.MarchingCubes{
using static CubeUtilities;

public class MarchingCubes{
	Dictionary<int, MarchingCube> _cubes = new();

	public int numCubes{
		get => _cubes.Count;
	}

	public IEnumerable<int> cubeCodes{
		get => _cubes.Keys;
	}

	public IEnumerable<MarchingCube> cubes{
		get => _cubes.Values;
	}

	void registerCubes(IEnumerable<MarchingCube> cubes){
		foreach(var cur in cubes)
			registerCube(cur);
	}

	bool registerCube(MarchingCube cube){
		bool result = _cubes.TryAdd(cube.code, cube);
		return result;
	}

	/*
	void registerPermutations(MarchingCube cube, bool inverse){
		registerCube(cube);
		if (inverse){
			var inverted = cube.inverted();
			registerCube(inverted);
		}

		for(int i = 1; i < rotations.Length; i++){
			var curRot = rotations[i];
			var newCube = curRot.transformCube(cube);
			registerCube(newCube);
			if (inverse){
				var newInverted = newCube.inverted();
				registerCube(newInverted);
			}
		}
	}
	*/

	static IEnumerable<MarchingCube> buildPermutations(MarchingCube cube, IntRot[] rotations, bool inverse){
		HashSet<int> codes = new();
		if (codes.Add(cube.code))
			yield return cube;

		if (inverse){
			var inverted = cube.inverted();
			if (codes.Add(inverted.code))
				yield return inverted;
		}

		for(int i = 1; i < rotations.Length; i++){
			var curRot = rotations[i];
			//Debug.Log($"introt:{i}: x:{curRot.right}; y{curRot.up}; z: {curRot.forward}");
			var newCube = curRot.transformCube(cube);
			if (codes.Add(newCube.code))
				yield return newCube;
			if (inverse){
				var newInverted = newCube.inverted();
				if (codes.Add(newInverted.code))
					yield return newInverted;
			}
		}
	}

	static IEnumerable<MarchingCube> combinePermutations(IEnumerable<MarchingCube> a, IEnumerable<MarchingCube> b, HashSet<int> knownCodes){
		foreach(var x in a){
			foreach(var y in b){
				if (x.canCombineWith(y)){
					var combined = x.combinedWith(y);;
					if (knownCodes.Add(combined.code))
						yield return combined;
					var invCombined = combined.inverted();
					if (knownCodes.Add(invCombined.code))
						yield return invCombined;
				}
			}
		}
	}

	static IntRot[] buildRotations(){
		Vector3Int xposi3 = vec3i(1, 0, 0);
		Vector3Int yposi3 = vec3i(0, 1, 0);
		Vector3Int zposi3 = vec3i(0, 0, 1);
		Vector3Int xnegi3 = vec3i(-1, 0, 0);
		Vector3Int ynegi3 = vec3i(0, -1, 0);
		Vector3Int znegi3 = vec3i(0, 0, -1);

		IntRot[] rotations = {
			//ypos up
			new IntRot(xposi3, yposi3),
			new IntRot(xnegi3, yposi3),
			new IntRot(zposi3, yposi3),
			new IntRot(znegi3, yposi3),

			//yneg up
			new IntRot(xposi3, ynegi3),
			new IntRot(xnegi3, ynegi3),
			new IntRot(zposi3, ynegi3),
			new IntRot(znegi3, ynegi3),

			//xpos up
			new IntRot(yposi3, xposi3),
			new IntRot(ynegi3, xposi3),
			new IntRot(zposi3, xposi3),
			new IntRot(znegi3, xposi3),

			//xneg up
			new IntRot(yposi3, xnegi3),
			new IntRot(ynegi3, xnegi3),
			new IntRot(zposi3, xnegi3),
			new IntRot(znegi3, xnegi3),

			//zpos up
			new IntRot(yposi3, zposi3),
			new IntRot(ynegi3, zposi3),
			new IntRot(xposi3, zposi3),
			new IntRot(xnegi3, zposi3),

			//zneg up
			new IntRot(yposi3, znegi3),
			new IntRot(ynegi3, znegi3),
			new IntRot(xposi3, znegi3),
			new IntRot(xnegi3, znegi3)
		};

		return rotations;
	}

	public void rebuild(){
		_cubes.Clear();

		var cube0 = new MarchingCube(0, new Vector3Int[]{});
		var cube1 = new MarchingCube(1, new Vector3Int[]{
			vec3i(0, -1, -1),
			vec3i(-1, 0, -1),
			vec3i(-1, -1, 0)
		});

		var cube3 = new MarchingCube(3, new Vector3Int[]{
			vec3i(-1, -1, 0),
			vec3i(1, -1, 0),
			vec3i(-1, 0, -1),
			vec3i(-1, 0, -1),
			vec3i(1, -1, 0),
			vec3i(1, 0, -1)
		});

		var cube7 = new MarchingCube(7, new Vector3Int[]{
			vec3i(-1, 0, 1),
			vec3i(0, -1, 1),
			vec3i(1, -1, 0),
			vec3i(-1, 0, 1),
			vec3i(1, -1, 0),
			vec3i(1, 0, -1),
			vec3i(-1, 0, 1),
			vec3i(1, 0, -1),
			vec3i(-1, 0, -1)
		});

		var cube15 = new MarchingCube(15, new Vector3Int[]{
			vec3i(-1, 0, 1),
			vec3i(1, 0, 1),
			vec3i(1, 0, -1),
			vec3i(-1, 0, 1),
			vec3i(1, 0, -1),
			vec3i(-1, 0, -1)
		});

		/*
		1 + 2 + 4 + 8*0 + 16
		*/
		var cube23 = new MarchingCube(23, new Vector3Int[]{
			vec3i(-1, 1, 0),
			vec3i(-1, 0, 1),
			vec3i(1, 0, -1),
			vec3i(-1, 1, 0),
			vec3i(1, 0, -1),
			vec3i(0, 1, -1),

			vec3i(-1, 0, 1),
			vec3i(0, -1, 1),
			vec3i(1, -1, 0),
			vec3i(-1, 0, 1),
			vec3i(1, -1, 0),
			vec3i(1, 0, -1)
		});

		/*
		1 + 2 + 4*0 + 8 + 16
		*/
		var cube27 = new MarchingCube(27, new Vector3Int[]{
			vec3i(-1, 1, 0),
			vec3i(1, 0, 1),
			vec3i(0, 1, -1),

			vec3i(0, 1, -1),
			vec3i(1, 0, 1),
			vec3i(1, 0, -1),

			vec3i(-1, 1, 0),
			vec3i(-1, -1, 0),
			vec3i(0, -1, 1),

			vec3i(-1, 1, 0),
			vec3i(0, -1, 1),
			vec3i(1, 0, 1)
		});

		//rotated and mirrored version of the previous cube
		/*
		1 + 2 * 0 + 4 + 8 + 16
		*/
		var cube29 = new MarchingCube(29, new Vector3Int[]{
			vec3i(-1, 1, 0),
			vec3i(-1, 0, 1),
			vec3i(1, 0, 1),

			vec3i(-1, 1, 0),
			vec3i(1, 0, 1),
			vec3i(0, 1, -1),

			vec3i(0, 1, -1),
			vec3i(1, 0, 1),
			vec3i(1, -1, 0),

			vec3i(0, 1, -1),
			vec3i(1, -1, 0),
			vec3i(0, -1, -1)
		});

		/*
		registerPermutations(cube0, true);
		registerPermutations(cube1, true);
		registerPermutations(cube3, true);
		registerPermutations(cube7, true);
		registerPermutations(cube15, false);
		*/
		var rotations = buildRotations();

		var cube0perms = buildPermutations(cube0, rotations, true).ToList();
		var cube1perms = buildPermutations(cube1, rotations, true).ToList();
		var cube3perms = buildPermutations(cube3, rotations, true).ToList();
		var cube7perms = buildPermutations(cube7, rotations, true).ToList();
		var cube15perms = buildPermutations(cube15, rotations, true).ToList();
		var cube23perms = buildPermutations(cube23, rotations, true).ToList();
		var cube27perms = buildPermutations(cube27, rotations, true).ToList();
		var cube29perms = buildPermutations(cube29, rotations, true).ToList();

		/*
		Debug.Log($"cube0perms: {cube0perms.Count}");
		Debug.Log($"cube1perms: {cube1perms.Count}");
		Debug.Log($"cube3perms: {cube3perms.Count}");
		Debug.Log($"cube7perms: {cube7perms.Count}");
		Debug.Log($"cube15perms: {cube15perms.Count}");
		Debug.Log($"cube23perms: {cube23perms.Count}");
		Debug.Log($"cube27perms: {cube27perms.Count}");
		Debug.Log($"cube29perms: {cube29perms.Count}");
		*/

		var combined = cube0perms
			.Concat(cube1perms)
			.Concat(cube3perms)
			.Concat(cube7perms)
			.Concat(cube15perms)
			.Concat(cube23perms)
			.Concat(cube27perms)
			.Concat(cube29perms)
			.ToList();

		var known = new HashSet<int>();
		foreach(var cur in combined)
			known.Add(cur.code);

		//Debug.Log($"total combined: {combined.Count}");

		int iteration = 0;		
		while(true){
			var newPerms = combinePermutations(combined, combined, known).ToList();
			//Debug.Log($"new perms: {newPerms.Count}; iteration: {iteration}");
			if (newPerms.Count == 0)
				break;
			iteration++;
			combined.AddRange(newPerms);
		}
		registerCubes(combined);
		/*
		registerCube(cube0);
		registerCube(cube1);
		registerCube(cube3);
		registerCube(cube7);
		registerCube(cube15);
		registerCube(cube23);
		registerCube(cube27);
		registerCube(cube29);
		*/
	}

	struct EdgeData{
		public Vector3Int start;
		public Vector3Int mid;
		public Vector3Int end;
		public EdgeData(Vector3Int start_, Vector3Int mid_, Vector3Int end_){
			start = start_;
			mid = mid_;
			end = end_;
		}
	}

	EdgeData mkSrcEdge(Vector3Int start, Vector3Int mid, Vector3Int end){
		return new EdgeData(start, mid, end);
	}

	public CompressedMarchingCubes compress(){
		var srcEdges = new EdgeData[]{
			mkSrcEdge(vec3i(-1, -1, -1), vec3i(0, -1, -1), vec3i(1, -1, -1)),
			mkSrcEdge(vec3i(-1, -1, 1), vec3i(0, -1, 1), vec3i(1, -1, 1)),

			mkSrcEdge(vec3i(-1, -1, -1), vec3i(-1, -1, 0), vec3i(-1, -1, 1)),
			mkSrcEdge(vec3i(1, -1, -1), vec3i(1, -1, 0), vec3i(1, -1, 1)),

			mkSrcEdge(vec3i(-1, -1, -1), vec3i(-1, 0, -1), vec3i(-1, 1, -1)),
			mkSrcEdge(vec3i(1, -1, -1), vec3i(1, 0, -1), vec3i(1, 1, -1)),
			mkSrcEdge(vec3i(-1, -1, 1), vec3i(-1, 0, 1), vec3i(-1, 1, 1)),
			mkSrcEdge(vec3i(1, -1, 1), vec3i(1, 0, 1), vec3i(1, 1, 1)),

			mkSrcEdge(vec3i(-1, 1, -1), vec3i(0, 1, -1), vec3i(1, 1, -1)),
			mkSrcEdge(vec3i(-1, 1, 1), vec3i(0, 1, 1), vec3i(1, 1, 1)),

			mkSrcEdge(vec3i(-1, 1, -1), vec3i(-1, 1, 0), vec3i(-1, 1, 1)),
			mkSrcEdge(vec3i(1, 1, -1), vec3i(1, 1, 0), vec3i(1, 1, 1))
		};

		List<Edge> edges = new();
		Dictionary<Vector3Int, int> edgeToIndex = new();
		for(int i = 0; i < srcEdges.Length; i++){
			var src = srcEdges[i];
			edgeToIndex.Add(src.mid, i);
			var startIdx = cornerVectorToIndex(src.start);
			var endIdx = cornerVectorToIndex(src.end);
			edges.Add(new Edge(startIdx, endIdx));
		}
		List<Face> faces = new();
		List<byte> edgeData = new();
		List<int> faceData = new();
		List<byte> bytes = new();

		foreach(var cur in edges){
			edgeData.Add(cur.start);
			edgeData.Add(cur.end);
		}
		//result.edges = edgeData.ToArray();
		for(int cubeCode = 0; cubeCode < 256; cubeCode++){
			MarchingCube cube;
			Face face = new();
			face.offset = bytes.Count;
			if (tryGetCube(cubeCode, out cube)){
				for(int i = 0; i < cube.points.Count; i++){
					var point = cube.points[i];
					int edgeIndex = 0;
					if (!edgeToIndex.TryGetValue(point, out edgeIndex)){
						Debug.LogWarning($"Could not find point {point}");
					}
					else{
						bytes.Add((byte)edgeIndex);
					}
					face.numVerts++;
				}				
			}
			faces.Add(face);
		}
		foreach(var cur in faces){
			faceData.Add(cur.offset);
			faceData.Add(cur.numVerts);
		}
		//result.faces = faceData.ToArray();
		//result.indices = bytes.ToArray();
		return new CompressedMarchingCubes(
			edgeData.ToArray(),
			faceData.ToArray(),
			bytes.ToArray()
		);
	}

	public bool hasCube(int code){
		return _cubes.ContainsKey(code);
	}

	public bool tryGetCube(int code, out MarchingCube cube){
		return _cubes.TryGetValue(code, out cube);
	}

}

}