using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NiTools{

public static partial class RasterText{

public class GlyphIndexes{
	protected string _glyphs;
	protected Dictionary<char, int> _indexes = new();
	
	public string glyphs => _glyphs;
	public int numGlyphs => _glyphs.Length;

	public bool hasGlyph(char c){
		return _indexes.ContainsKey(c);
	}

	public (int index, bool found) getGlyphIndex(char c, int defaultVal = 0){
		int index;
		if (_indexes.TryGetValue(c, out index))
			return (index, true);
		return (defaultVal, false);
	}

	void buildGlyphIndexes(string symbols_){
		_glyphs = symbols_;
		_indexes.Clear();
		for(int i = 0; i < _glyphs.Length; i++){
			if (!_indexes.TryAdd(_glyphs[i], i)){
				Debug.LogWarning($"Duplicate symbol {_glyphs[i]} found at position {i}");
			}
		}
	}

	public GlyphIndexes(string glyphs_){
		buildGlyphIndexes(glyphs_);
	}

	public GlyphIndexes(GlyphIndexes other_){
		buildGlyphIndexes(other_.glyphs);
	}
}

}
}
