using UnityEngine;
using System.Collections.Generic;

namespace NiTools.MarchingCubes{
using Vec3i = Vector3Int;
using Vec3f = Vector3;
using static BitUtilities;

public static class CubeUtilities{

	public static int cornerVectorToIndex(Vec3i cubeCorner){
		int result = 0;
		if (cubeCorner.x >= 0)
			result = setBit(result, 0);
		if (cubeCorner.z >= 0)
			result = setBit(result, 1);
		if (cubeCorner.y >= 0)
			result = setBit(result, 2);
		return result;
	}

	public static Vec3f cornerIndexTo0Vec3f(int index){
		return new Vec3f(
			hasBit(index, 0) ? 1.0f: 0.0f,
			hasBit(index, 2) ? 1.0f: 0.0f,
			hasBit(index, 1) ? 1.0f: 0.0f
		);
	}

	public static Vec3f getCellCorner(int cornerIndex, Vec3f cellMin, Vec3f cellMax){
		return new Vec3f(
			hasBit(cornerIndex, 0) ? cellMax.x: cellMin.x,
			hasBit(cornerIndex, 2) ? cellMax.y: cellMin.y,
			hasBit(cornerIndex, 1) ? cellMax.z: cellMin.z
		);
	}

	public static Vec3i cornerIndexToVector(int index){
		var result = vec3i(-1, -1, -1);
		if (hasBit(index, 0))
			result.x += 2;
		if (hasBit(index, 1))
			result.z += 2;
		if (hasBit(index, 2))
			result.y += 2;
		return result;
	}

	public static Vec3i vec3i(int x, int y, int z){
		return new Vec3i(x, y, z);
	}

	public static Vec3i cross(Vec3i a, Vec3i b){
		//yzx zxy zxy yzx
		return vec3i(
			a.y * b.z - a.z * b.y,
			a.z * b.x - a.x * b.z,
			a.x * b.y - a.y * b.x
		);
	}

	public static bool canCombineCubeCodes(int a, int b){
		if ((a & b) != 0)
			return false;

		for(int i = 0; i < 8; i++){
			if (!hasBit(a, i))
				continue;
			if (hasBit(b, toggleBit(i, 0)))
				return false;
			if (hasBit(b, toggleBit(i, 1)))
				return false;
			if (hasBit(b, toggleBit(i, 2)))
				return false;
		}
		return true;
	}
}

}