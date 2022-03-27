using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NiTools.Components{

[ExecuteInEditMode]
public class MultiLineRenderer: MonoBehaviour{
	[System.Serializable]
	public class Line{
		public bool loop = false;
		public int numPoints{
			get => points.Count;
		}
		public List<Vector3> points = new();
	}

	public Material material;
	public float lineWidth = 1.0f;
	public List<Line> lines = new();
	public bool useFixedVector = false;
	public bool lazyUpdate = false;
	public Vector3 fixedVector = Vector3.down;	
	public void markDirtyMesh(){
		dirtyMesh = true;
	}

	Camera lineCam{
		get => Camera.main;
	}

	Vector3 lineForwardVector{
		get => useFixedVector ? 
			fixedVector
			: transform.InverseTransformDirection(lineCam.transform.forward);
	}

	Mesh lineMesh = null;
	MeshRenderer meshRend = null;
	MeshFilter meshFilt = null;
	Vector3 lastForwardVector = Vector3.zero;
	List<int> idx = new();
	List<Vector3> verts = new();
	List<Vector2> uvs = new();
	bool dirtyMesh = false;

	bool isEditorMode(){
		return Application.isEditor && !Application.isPlaying;
	}

	void destroyMesh(){
		if (lineMesh){
			if (isEditorMode())
				DestroyImmediate(lineMesh);
			else
				Destroy(lineMesh);
			lineMesh = null;
		}
	}

	public struct CameraPose{
		public Vector3 pos;
		public Vector3 up;
		public Vector3 right;
		public Vector3 forward;
		public CameraPose(Transform t){
			pos = t.position;
			up = t.up;
			right = t.right;
			forward = t.forward;
		}
	}

	void addVertex(Vector3 pos, Vector2 uv){
		uvs.Add(uv);
		verts.Add(pos);
	}

	void addLineSegment(Vector3 start, Vector3 end, Vector3 camForward){
		var diff = end - start;
		var diffProj = Vector3.ProjectOnPlane(diff, camForward);
		if (diffProj.sqrMagnitude == 0.0f)
			return;
		var side = Vector3.Cross(camForward, diffProj).normalized;
		var sideOffs = side * 0.5f * lineWidth;
		//var forward = Vector3.Cross(side, camPos.forward).normalized;
		//var forwardOffs = forward * 0.5f * lineWidth;

		var a = start - sideOffs;// + forwardOffs;
		var b = start + sideOffs;// + forwardOffs;
		var c = end - sideOffs;// - forwardOffs;
		var d = end + sideOffs;// - forwardOffs;

		var baseIndex = verts.Count;
		addVertex(a, new Vector2(0.0f, 0.0f));
		addVertex(b, new Vector2(1.0f, 0.0f));
		addVertex(c, new Vector2(0.0f, 1.0f));
		addVertex(d, new Vector2(1.0f, 1.0f));

		idx.Add(baseIndex + 0);
		idx.Add(baseIndex + 1);
		idx.Add(baseIndex + 2);
		idx.Add(baseIndex + 1);
		idx.Add(baseIndex + 3);
		idx.Add(baseIndex + 2);
	}

	void addLineSegment(Vector3 start, Vector3 end, Vector3 camForward, float startLerp, float endLerp){
		var diff = end - start;
		var diffProj = Vector3.ProjectOnPlane(diff, camForward);
		if (diffProj.sqrMagnitude == 0.0f)
			return;
		var side = Vector3.Cross(camForward, diffProj).normalized;
		var sideOffs = side * 0.5f * lineWidth;

		var startPos = Vector3.Lerp(start, end, startLerp);
		var endPos = Vector3.Lerp(start, end, endLerp);
		var a = startPos - sideOffs;
		var b = startPos + sideOffs;
		var c = endPos - sideOffs;
		var d = endPos + sideOffs;

		var baseIndex = verts.Count;
		addVertex(a, new Vector2(0.0f, 0.0f));
		addVertex(b, new Vector2(1.0f, 0.0f));
		addVertex(c, new Vector2(0.0f, 1.0f));
		addVertex(d, new Vector2(1.0f, 1.0f));

		idx.Add(baseIndex + 0);
		idx.Add(baseIndex + 1);
		idx.Add(baseIndex + 2);
		idx.Add(baseIndex + 1);
		idx.Add(baseIndex + 3);
		idx.Add(baseIndex + 2);
	}

	void addKneeSegment(Vector3 start, Vector3 mid, Vector3 end, Vector3 camForward, float startLerp, float endLerp){
		var startDiff = mid-start;
		var endDiff = end-mid;
		var startDiffProj = Vector3.ProjectOnPlane(startDiff, camForward);
		var endDiffProj = Vector3.ProjectOnPlane(endDiff, camForward);
		var startSide = Vector3.Cross(camForward, startDiffProj).normalized;
		var endSide = Vector3.Cross(camForward, endDiffProj).normalized;

		var startDiffProjN = startDiff.normalized;
		var endDiffProjN = endDiff.normalized;
		var kneeDot = Vector3.Dot(startDiffProjN, endDiffProjN);

		var bendSide = (startSide + endSide).normalized;
		var bendScaleL = 1.0f;
		var bendScaleR = bendScaleL;
		var bendDot = Vector3.Dot(bendSide, startSide);
		if (bendDot != 0.0f){
			bendScaleL /= bendDot;
			bendScaleR /= bendDot;
		}
		var startSideOfs = startSide * 0.5f * lineWidth;
		var endSideOfs = endSide * 0.5f * lineWidth;

		var startLen = startDiffProj.magnitude;
		var endLen = endDiffProj.magnitude;
		var lerpScale = 0.95f;
		startLen *= (1.0f - startLerp) * lerpScale;
		endLen *= endLerp * lerpScale;
		var halfLineWidth = lineWidth * 0.5f;
		var maxStartHypotenuse = Mathf.Sqrt(startLen * startLen + halfLineWidth * halfLineWidth);
		var maxEndHypotenuse = Mathf.Sqrt(endLen * endLen + halfLineWidth * halfLineWidth);

		var angleDot = Vector3.Dot(endDiffProjN, startSide);
		if (angleDot > 0){
			bendScaleL = Mathf.Min(bendScaleL, 2.0f);
			bendScaleR = Mathf.Min(bendScaleR, 
				Mathf.Min(maxStartHypotenuse, maxEndHypotenuse)/halfLineWidth
			);
		}
		else{
			//bendScaleL = Mathf.Min(bendScaleL, 2.0f);
			bendScaleL = Mathf.Min(bendScaleL, 
				Mathf.Min(maxStartHypotenuse, maxEndHypotenuse)/halfLineWidth
			);
			bendScaleR = Mathf.Min(bendScaleR, 2.0f);
		}

		var startPos = Vector3.Lerp(start, mid, startLerp);
		var endPos = Vector3.Lerp(mid, end, endLerp);

		var p00 = startPos - startSideOfs;
		var p01 = startPos + startSideOfs;

		var bendSideOfsL = bendSide * bendScaleL * 0.5f * lineWidth;
		var bendSideOfsR = bendSide * bendScaleR * 0.5f * lineWidth;

		var p10 = mid - bendSideOfsL;
		var p11 = mid + bendSideOfsR;
		var p20 = endPos - endSideOfs;
		var p21 = endPos + endSideOfs;

		var baseIndex = verts.Count;
		addVertex(p00, new Vector2(0.0f, 0.0f));
		addVertex(p01, new Vector2(1.0f, 0.0f));
		addVertex(p10, new Vector2(0.0f, 1.0f));
		addVertex(p11, new Vector2(1.0f, 1.0f));

		addVertex(p10, new Vector2(0.0f, 0.0f));
		addVertex(p11, new Vector2(1.0f, 0.0f));
		addVertex(p20, new Vector2(0.0f, 1.0f));
		addVertex(p21, new Vector2(1.0f, 1.0f));

		for(int i = 0; i < 2; i++){
			idx.Add(baseIndex + 0);
			idx.Add(baseIndex + 1);
			idx.Add(baseIndex + 2);
			idx.Add(baseIndex + 1);
			idx.Add(baseIndex + 3);
			idx.Add(baseIndex + 2);

			baseIndex += 4;
		}
	}

	void rebuildMesh(){
		uvs.Clear();
		verts.Clear();
		idx.Clear();

		//var camPose = new CameraPose(lineCam.transform);
		var forwardVec = lineForwardVector;//lineCam.transform.forward;
		foreach(var curLine in lines){
			if (curLine.numPoints < 2)
				continue;
			if (curLine.numPoints == 2){
				addLineSegment(curLine.points[0], curLine.points[1], forwardVec);
				continue;
			}
			if (!curLine.loop){
				addLineSegment(curLine.points[0], curLine.points[1], forwardVec, 0.0f, 0.5f);
			}
			for(int i = 1; i < (curLine.points.Count - 1); i++){
				addKneeSegment(curLine.points[i-1], curLine.points[i], curLine.points[i+1], forwardVec, 0.5f, 0.5f);
			}
			if (!curLine.loop){
				addLineSegment(
					curLine.points[curLine.numPoints-2], 
					curLine.points[curLine.numPoints-1], 
					forwardVec, 
					0.5f, 1.0f
				);
			}
			else{
				addKneeSegment(
					curLine.points[curLine.numPoints-2], 
					curLine.points[curLine.numPoints-1], 
					curLine.points[0], 
					forwardVec, 0.5f, 0.5f
				);
				addKneeSegment(
					curLine.points[curLine.numPoints-1], 
					curLine.points[0], 
					curLine.points[1], 
					forwardVec, 0.5f, 0.5f
				);
			}
		}
		lineMesh.Clear();
		lineMesh.SetVertices(verts);
		lineMesh.SetUVs(0, uvs);
		lineMesh.SetIndices(idx, MeshTopology.Triangles, 0);
		dirtyMesh = false;
	}

	void OnEnable(){
		destroyMesh();
		lineMesh = new Mesh();
		if (!TryGetComponent(out meshRend)){
			meshRend = gameObject.AddComponent<MeshRenderer>();
		}
		if (!TryGetComponent(out meshFilt)){
			meshFilt = gameObject.AddComponent<MeshFilter>();
		}
		meshFilt.sharedMesh = lineMesh;
		lastForwardVector = Vector3.zero;
		rebuildMesh();
	}
	
	void OnDisable(){
		destroyMesh();
	}

	// Update is called once per frame
	void LateUpdate(){
		if (!lineMesh || (lines.Count == 0))
			return;
		meshRend.sharedMaterial = material;
		if (lazyUpdate){
			var curForwardVec = lineForwardVector;
			if ((curForwardVec == lastForwardVector) && !dirtyMesh){
				return;
			}
			lastForwardVector = curForwardVec;
		}
		rebuildMesh();
	}
}

}
