using UnityEngine;

using static NiTools.Shortcuts.Constructors.FloatVectors;

namespace NiTools.Shortcuts{
using Vec2 = Vector2;

public static partial class VectorFuncs{	
	public static Vec2 add(Vec2 a, Vec2 b){
		return a + b;
	}
	public static Vec2 lerp(Vec2 a, Vec2 b, float t){
		return Vector3.LerpUnclamped(a, b, t);
	}
}

}