using UnityEngine;
using System.Collections.Generic;

namespace NiTools{

public static partial class RasterText{

public class RasterMeshText: System.IDisposable{
	GlyphIndexes _glyphIndexes;

	public GlyphIndexes glyphIndexes => _glyphIndexes;
	List<Mesh> fontMeshes = new();

	void clearMeshes(){
		foreach(var cur in fontMeshes)
			Object.Destroy(cur);
		fontMeshes.Clear();
	}
	public void Dispose(){
		clearMeshes();
	}

	public bool hasGlyph(char c) => _glyphIndexes.hasGlyph(c);
	public (int index, bool found) getGlyphIndex(char c, int defaultVal = 0) 
		=> _glyphIndexes.getGlyphIndex(c, defaultVal);

	public Mesh getGlyphMeshByIndex(int glyphIndex) => fontMeshes[glyphIndex];
	public Mesh getGlyphMesh(char c) => fontMeshes[getGlyphIndex(c, 0).index];

	Mesh buildGlyphMesh(GlyphData glyphData, int glyphIndex){
		List<int> trigs = new();
		List<Vector3> verts = new();
		List<Vector3> normals = new();
		List<Vector2> uvs = new();

		(Vector3 u, Vector3 v) getUvVectors(Vector3 n){
			(Vector3 u, Vector3 v) getBaseUv(Vector3 n, Vector3 startV){
				var v = startV;
				v = Vector3.ProjectOnPlane(v, n).normalized;
				var u = Vector3.Cross(n, v);
				return (u, v);
			}
			var result = getBaseUv(n, Vector3.up);
			if (result.u != Vector3.zero)
				return (result.u.normalized, result.v);
			result = getBaseUv(n, Vector3.forward);
			return (result.u, result.v.normalized);
		}

		Vector2 calcTexCoords(Vector3 p, (Vector3 u, Vector3 v) uv){
			return new Vector2(Vector3.Dot(p, uv.u)*0.5f + 0.5f, Vector3.Dot(p, uv.v)*0.5f + 0.5f);
		}

		Vector3 vec3(float x, float y, float z){
			return new Vector3(x, y, z);
		}

		void addVert(Vector3 p, Vector3 n, (Vector3 u, Vector3 v) uvPlane){
			verts.Add(p);
			normals.Add(n);
			uvs.Add(calcTexCoords(p, uvPlane));
		}

		//from left top, clockwise
		void addQuad(Vector3 a, Vector3 b, Vector3 c, Vector3 d){
			var ab = b - a;
			var bc = c - b;
			var n = Vector3.Cross(ab, bc).normalized;

			var baseIndex = verts.Count;
			var uvPlane = getUvVectors(n);
			addVert(a, n, uvPlane);
			addVert(b, n, uvPlane);
			addVert(c, n, uvPlane);
			addVert(d, n, uvPlane);
			trigs.Add(baseIndex + 0);
			trigs.Add(baseIndex + 1);
			trigs.Add(baseIndex + 2);
			trigs.Add(baseIndex + 0);
			trigs.Add(baseIndex + 2);
			trigs.Add(baseIndex + 3);
		}

		var glyphWidth = glyphData.glyphWidth;
		var glyphHeight = glyphData.glyphHeight;
		var minCoord = new Vector3(-1.0f, -1.0f, 1.0f);
		var maxCoord = new Vector3(1.0f, 1.0f, -1.0f);
		var xStep = (maxCoord.x - minCoord.x)/glyphWidth;
		var zStep = (maxCoord.z - minCoord.z)/glyphHeight;

		for(int y = 0; y < glyphHeight; y++){
			for(int x = 0; x < glyphWidth; x++){
				var bit = glyphData.getGlyphBitByIndex(glyphIndex, x, y);
				if(!bit)
					continue;
				
				var x0 = minCoord.x + xStep * x;
				var x1 = minCoord.x + xStep * (x + 1);
				var y0 = minCoord.y;
				var y1 = maxCoord.y;
				var z0 = minCoord.z + zStep * y;
				var z1 = minCoord.z + zStep * (y + 1);
				addQuad(
					vec3(x0, y1, z0),
					vec3(x1, y1, z0),
					vec3(x1, y1, z1),
					vec3(x0, y1, z1)
				);
				addQuad(
					vec3(x1, y0, z0),
					vec3(x0, y0, z0),
					vec3(x0, y0, z1),
					vec3(x1, y0, z1)
				);
				if (!glyphData.getGlyphBitByIndex(glyphIndex, x-1, y)){
					addQuad(
						vec3(x0, y1, z0),
						vec3(x0, y1, z1),
						vec3(x0, y0, z1),
						vec3(x0, y0, z0)
					);
				}
				if (!glyphData.getGlyphBitByIndex(glyphIndex, x+1, y)){
					addQuad(
						vec3(x1, y1, z1),
						vec3(x1, y1, z0),
						vec3(x1, y0, z0),
						vec3(x1, y0, z1)
					);
				}
				if (!glyphData.getGlyphBitByIndex(glyphIndex, x, y-1)){
					addQuad(
						vec3(x1, y1, z0),
						vec3(x0, y1, z0),
						vec3(x0, y0, z0),
						vec3(x1, y0, z0)
					);
				}
				if (!glyphData.getGlyphBitByIndex(glyphIndex, x, y+1)){
					addQuad(
						vec3(x0, y1, z1),
						vec3(x1, y1, z1),
						vec3(x1, y0, z1),
						vec3(x0, y0, z1)
					);
				}
			}
		}

		var mesh = new Mesh();
		mesh.SetVertices(verts);
		mesh.SetUVs(0, uvs);
		mesh.SetNormals(normals);
		mesh.SetIndices(trigs, MeshTopology.Triangles, 0);

		return mesh;
	}

	void buildGlyphs(GlyphData glyphData){
		clearMeshes();
		for(int i = 0; i < glyphData.numGlyphs; i++){
			fontMeshes.Add(buildGlyphMesh(glyphData, i));
		}
	}

	public RasterMeshText(GlyphData glyphData_){
		_glyphIndexes = new GlyphIndexes(glyphData_.glyphIndexes);
		buildGlyphs(glyphData_);
	}
}

}

}
