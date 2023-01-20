using UnityEngine;

namespace NiTools{

public static partial class ColorExtensions{
	public static Color intRgbToColor(int code, float alpha = 1.0f){
		var result = (Color)intRgbToColor32(code);
		result.a = alpha;
		return result;
	}

	public static Color32 intRgbToColor32(int code, byte alpha = 0xFF){
		byte r = (byte)((code >> 16) & 0xFF);
		byte g = (byte)((code >> 8) & 0xFF);
		byte b = (byte)(code & 0xFF);
		return new Color32(r, g, b, alpha);
	}
}

}//namespace NiTools
