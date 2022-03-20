using UnityEngine;

namespace NiTools{

public static class Procedurals2D{
	static Vector2 getSinCos(float radiansAngle){
		return new Vector2(Mathf.Cos(radiansAngle), Mathf.Sin(radiansAngle));
	}

	public static void unitCircleLoop2D(int numSegments, LineDelegate2d lineCallback){
		if (lineCallback == null)
			throw new System.ArgumentNullException();
		if (numSegments < 3)
			numSegments = 3;

		unitArcLoop2D(0.0f, (float)Mathf.PI * 2.0f, numSegments, lineCallback);
	}

	public static void unitArcLoop2D(float startAngle, float endAngle, int numSegments, LineDelegate2d lineCallback){
		if (lineCallback == null)
			throw new System.ArgumentNullException();

		if (numSegments <= 0)
			throw new System.ArgumentException();

		var prevSinCos = getSinCos(startAngle);
		float angleStep = (endAngle - startAngle)/(float)numSegments;
		for(int i = 1; i <= numSegments; i++){
			float curAngle = startAngle + angleStep * (float)i;
			var curSinCos = getSinCos(curAngle);
			lineCallback(prevSinCos, curSinCos);
			prevSinCos = curSinCos;
		}
	}
}

}