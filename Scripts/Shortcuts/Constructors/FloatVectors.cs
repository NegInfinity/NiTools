using UnityEngine;

namespace NiTools.Shortcuts.Constructors{
using Vec4 = Vector4;
using Vec3 = Vector3;
using Vec2 = Vector2;

public static partial class FloatVectors{
	public static Vec2 vec2(float x, float y)
		=> new Vec2(x, y);

	public static Vec3 vec3(float x, float y, float z)
		=> new Vec3(x, y, z);
	public static Vec3 vec3f(float x, float y, float z)
		=> new Vec3(x, y, z);

	public static Vec3 vec3(Vec2 v, float z)
		=> new Vec3(v.x, v.y, z);
	public static Vec3 vec3f(Vec2 v, float z)
		=> new Vec3(v.x, v.y, z);

	public static Vec4 vec4(float x, float y, float z, float w)
		=> new Vec4(x, y, z, w);
	public static Vec4 vec4(Vec3 v, float w)
		=> new Vec4(v.x, v.y, v.z, w);
	public static Vec4 vec4(Vec2 v1, Vec2 v2)
		=> new Vec4(v1.x, v1.y, v2.x, v2.y);
}

}
