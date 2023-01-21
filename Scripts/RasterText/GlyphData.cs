using UnityEngine;
using System.Collections.Generic;

namespace NiTools{

public static partial class RasterText{
	public class GlyphData<Int>{
		public int glyphWidth;
		public int glyphHeight;
		public string characters;
		public Int[] data;
		protected Dictionary<char, int> offsets = new();

		public int getGlyphIndex(char c, int defaultVal = -1){
			int result;
			if (offsets.TryGetValue(c, out result))
				return result;
			return defaultVal;
		}

		public int getGlyphOffset(char c, int defaultVal = -1){
			int result;
			if (offsets.TryGetValue(c, out result))
				return result * glyphHeight;
			return defaultVal;
		}

		public bool hasGlyph(char c){
			return offsets.ContainsKey(c);
		}

		void buildOffsets(){
			offsets.Clear();
			for(int i = 0; i < characters.Length; i++){
				var c = characters[i];
				if (offsets.ContainsKey(c)){
					Debug.LogWarning($"duplicate character {c} in glyph");
					continue;
				}
				offsets.Add(c, i);
			}
		}

		public GlyphData(int glyphWidth_, int glyphHeight_, string characters_, Int[] data_){
			glyphWidth = glyphWidth_;
			glyphHeight = glyphHeight_;
			characters = characters_;
			data = data_;
			buildOffsets();
		}
	}
}

}
