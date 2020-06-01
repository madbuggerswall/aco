﻿using System.Collections;
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

	public void removeVertex(Vertex vertex) {
		vertex.removeEdges();
		vertices.Remove(vertex);
		Destroy(vertex.gameObject);
	}
}