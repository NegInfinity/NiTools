using UnityEngine;

using static NiTools.Shortcuts.Constructors;

namespace NiTools.Shortcuts{
using Vec3i = Vector3Int;

public static partial class VectorFuncs{
	public static Vec3i cross(Vec3i a, Vec3i b){
		//yzx zxy zxy yzx
		return vec3i(
			a.y * b.z - a.z * b.y,
			a.z * b.x - a.x * b.z,
			a.x * b.y - a.y * b.x
		);
	}
}

}
