using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using static NiTools.Shortcuts.Constructors.FloatVectors;
using static NiTools.Shortcuts.Constructors.IntVectors;

namespace NiTools.Polygonizer.VisualDebug{

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DebugBlockPolygonizerVisualization: MonoBehaviour{
	[SerializeField]Material mat;
	MeshFilter meshFilt;
	MeshRenderer meshRend;
	Mesh mesh;

	List<Vector3> verts = new();
	List<Vector3> norms = new();
	List<Vector2> uvs = new();
	List<int> idx = new();

	float sphereRadius = 5.0f;

	IEnumerator controlRoutine(){
		while(true){
			yield return new WaitForSeconds(0.1f);
			sphereRadius = 5.0f + Mathf.Sin(Mathf.PI * Time.time * 2.0f/5.0f);
			//Debug.Log($"sphereRadius: {sphereRadius}");
			rebuildMesh();
		}
	}

	void OnEnable(){
		TryGetComponent(out meshFilt);
		TryGetComponent(out meshRend);
		mesh = new Mesh();
		meshFilt.mesh = mesh;
		meshRend.material = mat;

		rebuildMesh();

		StartCoroutine(controlRoutine());
	}

	int matGetter(Vector3Int arg){
		var v = vec3(arg.x, arg.y, arg.z);
		var dist = Mathf.Sqrt(Vector3.Dot(v, v));
		if (dist <= sphereRadius)
			return 1;
		return 0;
	}

	bool barrierCheck(int srcMat, int dstMat){
		return (srcMat != 0) && (srcMat != dstMat);
	}

	void quadBuilder(Vector3 a, Vector3 b, Vector3 c, Vector3 d, int matIndex){
		var ac = c - a;
		var bd = d - b;
		var n = Vector3.Cross(ac, bd).normalized;
		uvs.Add(vec2(0.0f, 1.0f));
		uvs.Add(vec2(1.0f, 1.0f));
		uvs.Add(vec2(1.0f, 0.0f));
		uvs.Add(vec2(0.0f, 0.0f));
		norms.Add(n);
		norms.Add(n);
		norms.Add(n);
		norms.Add(n);
		var baseIdx = verts.Count;
		idx.Add(baseIdx + 0);
		idx.Add(baseIdx + 1);
		idx.Add(baseIdx + 2);
		idx.Add(baseIdx + 3);
		verts.Add(a);
		verts.Add(b);
		verts.Add(c);
		verts.Add(d);
	}

	void rebuildMesh(){
		var builder = new NiTools.Polygonizer.BlockPolygonizer(
			matGetter, barrierCheck, quadBuilder
		);

		verts.Clear();
		norms.Clear();
		uvs.Clear();
		idx.Clear();

		builder.build(
			vec3i(-16, -16, -16),
			vec3i(16, 16, 16)
		);
		mesh.Clear();
		mesh.SetVertices(verts);
		mesh.SetNormals(norms);
		mesh.SetUVs(0, uvs);
		mesh.SetIndices(idx, MeshTopology.Quads, 0);
	}

	void OnDisable(){
		meshFilt.mesh = null;
		Destroy(mesh);
	}
}

}