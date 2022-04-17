using UnityEngine;

namespace NiTools.Shortcuts{

public static partial class ScalarFuncs{
	public static float min(float a, float b){
		return Mathf.Min(a, b);
	}
	public static float max(float a, float b){
		return Mathf.Max(a, b);
	}	
	public static float abs(float a){
		return Mathf.Abs(a);
	}
	//uses radians
	public static float sin(float a){
		return Mathf.Sin(a);
	}
	//uses radians
	public static float cos(float a){
		return Mathf.Cos(a);
	}
	//uses radians
	public static float tan(float a){
		return Mathf.Tan(a);
	}
	public static float lerp(float a, float b, float t){
		return Mathf.LerpUnclamped(a, b, t);
	}
	public static float clamp(float val, float min, float max){
		return Mathf.Clamp(val, min, max);
	}
	public static float clamp01(float val){
		return Mathf.Clamp01(val);
	}
	//returns value in radians
	public static float acos(float arg){
		return Mathf.Acos(arg);
	}
	//returns value in radians
	public static float asin(float arg){
		return Mathf.Asin(arg);
	}
	//returns value in radians
	public static float atan(float arg){
		return Mathf.Atan(arg);
	}
	public static float log10(float arg){
		return Mathf.Log10(arg);
	}
	public static float log(float arg){
		return Mathf.Log(arg);
	}
	public static float exp(float arg){
		return Mathf.Exp(arg);
	}
}

}
