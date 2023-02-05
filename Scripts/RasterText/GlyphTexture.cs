using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NiTools{

public static partial class RasterText{

public class GlyphTexture: System.IDisposable{
	public Texture2D tex;
	List<Rect> rects = new();
	GlyphIndexes _glyphIndexes;

	public GlyphIndexes glyphIndexes => _glyphIndexes;
	public bool hasGlyph(char c) => _glyphIndexes.hasGlyph(c);
	public (int index, bool found) getGlyphIndex(char c, int defaultVal = 0) 
		=> _glyphIndexes.getGlyphIndex(c, defaultVal);
		
	public Rect getGlyphRect(int index) => rects[index];
	public Rect getGlyphRect(char c) => rects[glyphIndexes.getGlyphIndex(c, 0).index];
	public int numGlyphs => rects.Count;

	public void Dispose(){
		if (tex){
			Object.Destroy(tex);
			tex = null;
		}
	}	

	(int width, int height) calculateTextureSize(int glyphWidth, int glyphHeight, int numGlyphs){
		int totalArea = glyphWidth * glyphHeight * numGlyphs;
		int startSize = 1 << Mathf.FloorToInt(Mathf.Log(Mathf.Sqrt(totalArea))/Mathf.Log(2.0f));
		int texWidth = startSize;
		int texHeight = startSize;
		int getNumTexGlyphs(){
			return (texWidth/glyphWidth) * (texHeight/glyphHeight);
		}
		int maxSize = 1 << 30;
		while(true){
			if (getNumTexGlyphs() >= numGlyphs)
				break;
			texWidth = texWidth << 1;
			if (getNumTexGlyphs() >= numGlyphs)
				break;
			texHeight = texHeight << 1;
			if ((texWidth >= maxSize) || (texHeight >= maxSize))
				throw new System.ArgumentException(
					$"Cannot fit {numGlyphs} {glyphWidth}x{glyphHeight} glyph(s) into maxSize of {maxSize}");
		}
		return (texWidth, texHeight);
	}

	void buildTexture(GlyphData glyphs){
		var texSize = calculateTextureSize(glyphs.glyphWidth, glyphs.glyphHeight, glyphs.numGlyphs);		
		rects.Clear();

		tex = new Texture2D(texSize.width, texSize.height, TextureFormat.RGB24, 0, false);
		var glyphWidth = glyphs.glyphWidth;
		var glyphHeight = glyphs.glyphHeight;
		var numGlyphs = glyphs.numGlyphs;
		var xGlyphs = texSize.width / glyphWidth;
		var yGlyphs = texSize.height / glyphHeight;
		var pixelData = new Color32[texSize.width * texSize.height];
		var stride = texSize.width;

		var fg = new Color32(255, 255, 255, 255);
		var bg = new Color32(0, 0, 0, 255);
		float uSize = (float)glyphWidth/texSize.width;
		float vSize = (float)glyphWidth/texSize.width;
		for(int glyphIndex = 0; glyphIndex < numGlyphs; glyphIndex++){
			int dstX = (glyphIndex % xGlyphs) * glyphWidth;
			int dstY = (glyphIndex / xGlyphs) * glyphHeight;

			int dstPixelOffset = (texSize.height - 1 - dstY)*stride;
			int dstScanLine = dstPixelOffset;

			/*
			The data is arranged from left to right, bottom to top
			https://docs.unity3d.com/ScriptReference/Texture2D.GetPixels.html
			*/
			for(int y = 0; y < glyphHeight; y++){
				for(int x = 0; x < glyphWidth; x++){
					pixelData[dstScanLine + x + dstX] = glyphs.getGlyphBitByIndex(glyphIndex, x, y) ? fg: bg;
				}
				dstScanLine -= stride;
			}

			var texRect = new Rect(
				(float)dstX / texSize.width,
				(float)dstY / texSize.height,
				uSize, vSize
			);
			rects.Add(texRect);
		}
		tex.SetPixels32(pixelData);
		tex.Apply();
	}

	public GlyphTexture(GlyphData glyphs){
		_glyphIndexes = new GlyphIndexes(glyphs.glyphIndexes);
		buildTexture(glyphs);
	}
}

}

}