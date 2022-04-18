using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace NiTools.MarchingCubes{
using static CubeUtilities;

public class MarchingCube{
	public int code = 0;
	public List<Vector3Int> points = new();

	public MarchingCube(){			
	}

	public MarchingCube(int code_, List<Vector3Int> points_){
		code = code_;
		points = points_;
	}
	public MarchingCube(int code_, Vector3Int[] points_){
		code = code_;
		points = points_.ToList();
	}

	public bool canCombineWith(MarchingCube other){
		return canCombineCubeCodes(code, other.code);
	}

	public MarchingCube combinedWith(MarchingCube other){
		MarchingCube result = new();
		result.code = code | other.code;
		result.points.AddRange(points);
		result.points.AddRange(other.points);
		return result;
	}

	public MarchingCube inverted(){
		MarchingCube result = new();
		result.code = code ^ 0xff;
		result.points.Clear();
		for(int i = 0; i < points.Count; i += 3){
			result.points.Add(points[i]);
			result.points.Add(points[i + 2]);
			result.points.Add(points[i + 1]);
		}
		return result;
	}
}



}