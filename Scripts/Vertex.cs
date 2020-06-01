using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour {
	[SerializeField] List<Edge> edges;

	void Awake() {

	}

	public void addEdge(Edge edge) {
		edges.Add(edge);
	}

	public void updateEdges() {
		foreach (Edge edge in edges) {
			edge.updatePosition();
		}
	}

	public void removeEdges() {
		foreach (Edge edge in edges) {
			Vertex vertex = edge.getStart();
			if (vertex != this)
				vertex.removeEdge(edge);
			else
				edge.getEnd().removeEdge(edge);
			Destroy(edge.gameObject);
		}
		edges.Clear();
	}

	public void removeEdge(Edge edge) {
		edges.Remove(edge);
	}
}