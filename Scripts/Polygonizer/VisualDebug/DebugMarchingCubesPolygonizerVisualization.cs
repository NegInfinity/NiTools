using UnityEngine;

using System.Collections.Generic;
using System.Collections;

using static NiTools.Shortcuts.Constructors.FloatVectors;

namespace NiTools.Polygonizer.VisualDebug{

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DebugMarchingCubesPolygonizerVisualization: MonoBehaviour{
	[SerializeField]Material mat;
	MeshFilter meshFilt;
	MeshRenderer meshRend;
	Mesh mesh;

	List<Vector3> verts = new();
	List<Vector3> norms = new();
	List<Vector2> uvs = new();
	List<int> idx = new();

	float sphereRadius = 5.0f;
	float spherePeriod = 5.0f;
	float sphereMovePeriod = 11.0f;
	float cubeRadius = 5.0f;
	float cubePeriod = 20.0f;
	Vector3 spherePos = Vector3.zero;

	static Vector3 sineLerp(Vector3 start, Vector3 end, float timePeriod){
		return Vector3.Lerp(start, end, Mathf.Sin(Mathf.PI * Time.time * 2.0f/timePeriod)*0.5f+0.5f);
	}
	static float sineLerp(float start, float end, float timePeriod){
		return Mathf.Lerp(start, end, Mathf.Sin(Mathf.PI * Time.time * 2.0f/timePeriod)*0.5f+0.5f);
	}

	IEnumerator controlRoutine(){
		while(true){
			yield return null;//yield return new WaitForSeconds(0.1f);
			sphereRadius = sineLerp(6.0f, 10.0f, spherePeriod);
			spherePos = sineLerp(new Vector3(-10.0f, 0.0f, 0.0f), new Vector3(10.0f, 0.0f, 0.0f), sphereMovePeriod);
			cubeRadius = 7.0f; //Mathf.Lerp(2.0f, 10.0f, Mathf.Sin(Mathf.PI * Time.time * 2.0f/cubePeriod)*0.5f+0.5f);			
			//radius = 5.0f;//sphereRadius = 2.0f;
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

	int matGetter(NiTools.Polygonizer.CustomTypes.Vec3i arg){
		return gridFunc(arg.toVec3()) > 0.0f ? 1: 0;
	}

	float sphereFunc(Vector3 arg){
		var v = arg - spherePos;
		var dist = Mathf.Sqrt(Vector3.Dot(v, v));
		return sphereRadius - dist;
	}

	float cubeFunc(Vector3 arg){
		var dist = Mathf.Max(
			Mathf.Max(
				Mathf.Abs(arg.x) - cubeRadius,
				Mathf.Abs(arg.y) - cubeRadius
			),
			Mathf.Abs(arg.z) - cubeRadius
		);

		return -dist;
	}

	float gridFunc(Vector3 arg){
		return Mathf.Min(cubeFunc(arg), -sphereFunc(arg));
		//return sphereFunc(arg);
	}

	bool barrierCheck(int srcMat, int dstMat){
		return (srcMat != 0) && (srcMat != dstMat);
	}

	void trigBuilder(Vector3 a, Vector3 b, Vector3 c){
		var ab = b - a;
		var ac = c - a;
		var n = Vector3.Cross(ab, ac).normalized;


		uvs.Add(vec2(a.x, a.z));
		uvs.Add(vec2(b.x, b.z));
		uvs.Add(vec2(c.x, c.z));
		norms.Add(n);
		norms.Add(n);
		norms.Add(n);
		var baseIdx = verts.Count;
		idx.Add(baseIdx + 0);
		idx.Add(baseIdx + 1);
		idx.Add(baseIdx + 2);
		verts.Add(a);
		verts.Add(b);
		verts.Add(c);
	}


	void rebuildMesh(){
		verts.Clear();
		norms.Clear();
		uvs.Clear();
		idx.Clear();


		Polygonizer.MarchingCubesPolygonizer.build(
			new Vector3(-16, -16, -16),
			new Vector3(16, 16, 16),
			new Vector3Int(32, 32, 32),
			gridFunc, trigBuilder
		);

		mesh.Clear();
		mesh.SetVertices(verts);
		mesh.SetNormals(norms);
		mesh.SetUVs(0, uvs);
		mesh.SetIndices(idx, MeshTopology.Triangles, 0);
	}

	void OnDisable(){
		meshFilt.mesh = null;
		Destroy(mesh);
	}

}

}