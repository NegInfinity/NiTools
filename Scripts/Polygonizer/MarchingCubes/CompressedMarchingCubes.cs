using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace NiTools.MarchingCubes{

public struct Edge{
	public byte start;
	public byte end;
	public Edge(int start_, int end_){
		start = (byte)start_;
		end = (byte)end_;
	}
}

public struct Face{
	public int offset;
	public int numVerts;
	public Face(int offset_, int numVerts_){
		offset = offset_;
		numVerts = numVerts_;
	}
}

public class CompressedMarchingCubes{
	byte[] _edges;//edge indexes for interpolation
	int[] _faces;//face indexes --> offset, then num vertices
	byte[] _indices;//vertex indexes

	public int numEdges{
		get => _edges.Length / 2;
	}
	public int numFaces{
		get => _faces.Length / 2;
	}
	public int numIndexes{
		get => _indices.Length;
	}
	public bool isValid => (_edges != null) && (_faces != null) && (_indices != null) 
		&& (numEdges != 0) && (numFaces != 0) && (numIndexes != 0);
	public byte[] indices{
		get => _indices;
	}
	public Edge getEdge(int index){
		return new Edge(_edges[index << 1], _edges[(index << 1) + 1]);
	}
	public Face getFace(int index){
		return new Face(_faces[index << 1], _faces[(index << 1) + 1]);
	}

	static IEnumerable<IEnumerable<T>> chunk<T>(IEnumerable<T> arg, int chunkSize){
		while(arg.Any()){
			yield return arg.Take(chunkSize);
			arg = arg.Skip(chunkSize);
		}
	}

	public string makeCodeData(){
		var sb = new System.Text.StringBuilder();
		sb.AppendLine("new(");
		sb.AppendLine("\tnew byte[]{");
		sb.AppendLine($"\t\t{string.Join(", ", _edges)}");
		sb.AppendLine("\t},");
		sb.AppendLine("\tnew int[]{");
		sb.AppendLine(
			string.Join(",\n",
				chunk(_faces, 16).Select(
					arg => $"\t\t{string.Join(", ", arg)}"
				)
			)
		);
		sb.AppendLine("\t},");
		sb.AppendLine("\tnew byte[]{");
		sb.AppendLine(
			string.Join(",\n",
				chunk(_indices, 18).Select(
					arg => $"\t\t{string.Join(", ", arg)}"
				)
			)
		);
		sb.AppendLine("\t}");
		//var edgeString = $"new byte[]{{{string.Join(", ", _edges)}}}";
		//var faceString = $"new int[]{{{string.Join(", ", _faces)}}}";
		//var indexString = $"new byte[]{{{string.Join(", ", _indices)}}}";
		sb.AppendLine(");");
		return sb.ToString();
	}

	public CompressedMarchingCubes(byte[] edges_, int[] faces_, byte[] indices_){
		_edges = edges_;
		_faces = faces_;
		_indices = indices_;
	}

	public CompressedMarchingCubes(){}
}

}