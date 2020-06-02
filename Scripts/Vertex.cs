using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour {
	[SerializeField] List<Edge> edges;

	bool isSource;
	bool isDestination;

	static Color yellow = new Color32(0xFF, 0xFA, 0x00, 0xFF);
	static Color green = new Color32(0x00, 0xE5, 0x69, 0xFF);
	static Color red = new Color32(0xFF, 0x00, 0x51, 0xFF);

	void Awake() {
	}

	public void addEdge(Edge edge) {
		edges.Add(edge);
	}

	public List<Edge> getEdges(){
		return edges;
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

	public void toggleIsDestination() {
		isDestination = !isDestination;
		isSource = false;
		adjustColor();
	}

	public void toggleIsSource() {
		isSource = !isSource;
		isDestination = false;
		adjustColor();
	}

	public bool getIsDestination() {
		return isDestination;
	}

	public bool getIsSource() {
		return isSource;
	}

	void adjustColor() {
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		if (isDestination)
			renderer.material.color = green;
		else if(isSource)
			renderer.material.color = red;
		else
			renderer.material.color = yellow;
	}
}