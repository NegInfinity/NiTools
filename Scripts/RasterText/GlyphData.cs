using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NiTools{

public static partial class RasterText{

public class GlyphData{
	protected int _glyphWidth;
	protected int _glyphHeight;
	protected int _numGlyphs;

	protected GlyphIndexes _glyphIndexes;
	protected BitArray _data;

	public int glyphWidth => _glyphWidth;
	public int glyphHeight => _glyphHeight;
	public string characters => _glyphIndexes.glyphs;
	public int numGlyphs => _numGlyphs;
	public int numGlyphBits => _glyphWidth * _glyphHeight;
	public int numTotalBits => numGlyphs * numGlyphBits;

	public GlyphIndexes glyphIndexes => _glyphIndexes;
	public bool hasGlyph(char c) => _glyphIndexes.hasGlyph(c);
	public (int index, bool found) getGlyphIndex(char c, int defaultVal = 0) 
		=> _glyphIndexes.getGlyphIndex(c, defaultVal);

	/*
	In source data, lowest bit is right-most pixel, and scanlines continue from up to down. 
	This is a convention used in text modes.

	However, within stored data, bits go from left to right, from top to bottom, glyph after glyph
	*/
	void initBitArray(int glyphWidth_, int glyphHeight_, int numGlyphs_){
		_glyphWidth = glyphWidth_;
		_glyphHeight = glyphHeight_;
		_numGlyphs = numGlyphs_;
		_data = new BitArray(numTotalBits, false);
	}

	public bool isValidGlyphIndex(int glyphIndex){
		return (glyphIndex >= 0) && (glyphIndex < numGlyphs);
	}

	public bool isValidGlyphCoord(int x, int y){
		return (x >= 0) && (y >= 0) && (x < glyphWidth) && (y < glyphHeight);
	}

	public bool getRawBit(int bitOffset){
		return _data[bitOffset];
	}

	public int getGlyphOffsetFromIndex(int index){
		return index * numGlyphBits;
	}

	public int getGlyphXyOffset(int x, int y){
		return x + y * glyphWidth;
	}

	public int getBitIndex(int glyphIndex, int x, int y){
		return getGlyphOffsetFromIndex(glyphIndex) + getGlyphXyOffset(x, y);
	}

	public bool getGlyphBitByIndex(int glyphIndex, int x, int y){
		if (!isValidGlyphIndex(glyphIndex) || !isValidGlyphCoord(x, y))
			return false;
		return _data[getBitIndex(glyphIndex, x, y)];
	}

	protected void _setGlyphBitByIndex(int glyphIndex, int x, int y, bool val){
		if (!isValidGlyphIndex(glyphIndex) || !isValidGlyphCoord(x, y))
			throw new System.ArgumentOutOfRangeException();
		_data[getBitIndex(glyphIndex, x, y)] = val;
	}

	protected delegate bool BitTestDelegate<Val>(Val val, int bitIndex);
	protected void loadVals<Val>(Val[] intData, BitTestDelegate<Val> bitTest){
		int srcBaseOffset = 0;
		int dstBaseOffset = 0;
		for(int glyphIndex = 0; glyphIndex < numGlyphs; glyphIndex++){
			int dstScanline = dstBaseOffset;
			for(int y = 0; y < glyphHeight; y++){
				var srcLine = intData[srcBaseOffset + y];
				for(int x = 0; x < glyphWidth; x++){
					var srcBitIndex = (glyphWidth - 1 - x);
					_data[dstScanline + x] = bitTest(srcLine, srcBitIndex);
				}
				dstScanline += glyphHeight;
			}
			srcBaseOffset += glyphHeight;
			dstBaseOffset += numGlyphBits;
		}
	}

	protected void loadInts(int[] intData){
		loadVals(intData, (val, bit) => ((1 << bit) & val) != 0);
	}

	protected void loadBytes(byte[] byteData){
		loadVals(byteData, (val, bit) => ((1 << bit) & val) != 0);
	}

	public GlyphData(int glyphWidth_, int glyphHeight_, string symbols_, int[] srcData_){
		initBitArray(glyphWidth_, glyphHeight_, symbols_.Length);
		_glyphIndexes = new GlyphIndexes(symbols_);
		loadInts(srcData_);
	}

	public GlyphData(int glyphWidth_, int glyphHeight_, string symbols_, byte[] srcData_){
		initBitArray(glyphWidth_, glyphHeight_, symbols_.Length);
		_glyphIndexes = new GlyphIndexes(symbols_);
		loadBytes(srcData_);
	}
}

}

}
