using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {
	[SerializeField] List<Vertex> vertices;

	GameObject vertexPrefab;
	GameObject edgePrefab;

	void Awake() {
		vertexPrefab = Resources.Load("Prefabs/Vertex", typeof(GameObject)) as GameObject;
		edgePrefab = Resources.Load("Prefabs/Edge", typeof(GameObject)) as GameObject;
		initialize();
	}

	void initialize() {
		GameObject vertexStart = Instantiate(vertexPrefab);
		GameObject vertexEnd = Instantiate(vertexPrefab);
		GameObject edge = Instantiate(edgePrefab);

		vertexStart.transform.position = Vector3.zero;
		vertexEnd.transform.position = new Vector3(0, 4, 0);

		vertexStart.transform.SetParent(this.transform);
		vertexEnd.transform.SetParent(this.transform);

		vertices.Add(vertexStart.GetComponent<Vertex>());
		vertices.Add(vertexEnd.GetComponent<Vertex>());
		edge.GetComponent<Edge>().initialize(vertexStart.GetComponent<Vertex>(), vertexEnd.GetComponent<Vertex>());
	}

	public void addVertex(Vector3 position) {
		GameObject vertex = Instantiate(vertexPrefab);
		vertex.transform.position = new Vector3(position.x, position.y, 0);
		vertex.transform.SetParent(this.transform);
		vertices.Add(vertex.GetComponent<Vertex>());
	}

	public void addEdge(Vertex start, Vertex end) {
		Edge edge = Instantiate(edgePrefab).GetComponent<Edge>();
		edge.initialize(start, end);
		edge.transform.SetParent(this.transform);
	}

	public Edge findEdge(Vertex start, Vertex end) {
		foreach (Edge startEdge in start.getEdges()) {
			foreach (Edge endEdge in end.getEdges()) {
				if (startEdge == endEdge) {
					return startEdge;
				}
			}
		}
		return null;
	}

	public void removeVertex(Vertex vertex) {
		vertex.removeEdges();
		vertices.Remove(vertex);
		Destroy(vertex.gameObject);
	}

	public void disableSource() {
		foreach (Vertex vertex in vertices) {
			if (vertex.getIsSource())
				vertex.toggleIsSource();
		}
	}

	public void disableDestination() {
		foreach (Vertex vertex in vertices) {
			if (vertex.getIsDestination())
				vertex.toggleIsDestination();
		}
	}

	public Vertex getSource() {
		foreach (Vertex vertex in vertices)
			if (vertex.getIsSource())
				return vertex;
		return null;
	}

	public Vertex getDestination() {
		foreach (Vertex vertex in vertices)
			if (vertex.getIsDestination())
				return vertex;
		return null;
	}
}