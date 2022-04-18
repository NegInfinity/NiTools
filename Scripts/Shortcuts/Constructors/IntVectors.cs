using UnityEngine;

namespace NiTools.Shortcuts.Constructors{
using Vec3i = Vector3Int;
using Vec2i = Vector2Int;

public static partial class IntVectors{
	public static Vec2i vec2i(int x, int y)
		=> new Vec2i(x, y);

	public static Vec3i vec3i(int x, int y, int z)
		=> new Vec3i(x, y, z);
		
}

}
