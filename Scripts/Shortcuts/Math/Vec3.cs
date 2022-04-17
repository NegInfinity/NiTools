using UnityEngine;

using static NiTools.Shortcuts.Constructors;

namespace NiTools.Shortcuts{
using Vec3 = Vector3;

public static partial class VectorFuncs{	
	public static Vec3 cross(Vec3 a, Vec3 b){
		return Vec3.Cross(a, b);
	}
	public static Vec3 add(Vec3 a, Vec3 b){
		return a + b;
	}
	public static Vec3 lerp(Vec3 a, Vec3 b, float t){
		return Vector3.LerpUnclamped(a, b, t);
	}
	public static float dot(Vec3 a, Vec3 b){
		return Vector3.Dot(a, b);
	}
}

}
