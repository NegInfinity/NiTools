using UnityEngine;


namespace NiTools.MarchingCubes{
using static CubeUtilities;
using static BitUtilities;

public struct IntRot{
	public Vector3Int right;
	public Vector3Int up;
	public Vector3Int forward{
		get => cross(right, up);
	}

	/*
	public IntRot(){
		right = Vector3Int.right;
		up = Vector3Int.up;
	}
	*/

	public IntRot(Vector3Int right_, Vector3Int up_){
		right = right_;
		up = up_;
	}

	public Vector3Int transformVector(Vector3Int arg){
		var result = vec3i(0, 0, 0);
		result += arg.x * right;
		result += arg.y * up;
		result += arg.z * forward;
		return result;
	}

	public MarchingCube transformCube(MarchingCube src){
		MarchingCube result = new();
		result.code = transformCubeIndex(src.code);
		foreach(var srcVec in src.points){
			result.points.Add(transformVector(srcVec));
		}
		return result;
	}

	public int transformCubeIndex(int src){
		int result = 0;
		for(int i = 0; i < 8; i++){
			if (!hasBit(src, i))
				continue;
			var cornerIndex = i;
			var vector = cornerIndexToVector(cornerIndex);
			var newVector = transformVector(vector);
			var newIndex = cornerVectorToIndex(newVector);
			result = result | getBitmask(newIndex);
		}
		return result;
	}
}

}