using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using static NiTools.Shortcuts.Constructors;

namespace NiTools.Polygonizer.CustomTypes{

using Vec3f = Vector3;

/*
Unity builtin Vector3Int has awful performance.
*/
public struct Vec3i{
	public int x;
	public int y;
	public int z;
	public Vec3i(int x_, int y_, int z_){
		x = x_;
		y = y_;
		z = z_;
	}

	public static Vec3i Min(Vec3i a, Vec3i b){
		return new Vec3i(
			Mathf.Min(a.x, b.x),
			Mathf.Min(a.y, b.y),
			Mathf.Min(a.z, b.z)
		);
	}

	public static Vec3i Max(Vec3i a, Vec3i b){
		return new Vec3i(
			Mathf.Max(a.x, b.x),
			Mathf.Max(a.y, b.y),
			Mathf.Max(a.z, b.z)
		);
	}

	public Vector3 toVec3(){
		return new Vec3f(x, y, z);
	}

	public static Vec3i operator+(Vec3i a, Vec3i b){
		return new Vec3i(a.x + b.x, a.y + b.y, a.z + b.z);
	}

	public static Vec3i operator-(Vec3i a, Vec3i b){
		return new Vec3i(a.x - b.x, a.y - b.y, a.z - b.z);
	}
}

}//Polygonizer.CustomTypes
