using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphControl : MonoBehaviour {
	Graph graph;

	GameObject target;
	Vector3 screenPosition;
	Vector3 offset;
	bool isMouseDragging;

	Vertex start;
	Vertex end;

	void Awake() {
		graph = FindObjectOfType<Graph>();
	}

	void Update() {
		dragVertex();
		addOrRemoveElement();
	}

	void dragVertex() {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hitInfo;
			target = getClickedObject(out hitInfo);
			if (target != null) {
				isMouseDragging = true;
				screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
				offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
			}
		}

		if (Input.GetMouseButtonUp(0)) {
			isMouseDragging = false;
		}

		if (isMouseDragging) {
			Vertex vertex = target.GetComponent<Vertex>();
			if (vertex != null) {
				target.transform.position = getMouseWorldPos();
				vertex.updateEdges();
			}
		}
	}

	GameObject getClickedObject(out RaycastHit hit) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray.origin, ray.direction * 20, out hit))
			return hit.collider.gameObject;
		else
			return null;
	}

	Vector3 getMouseWorldPos() {
		Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;
		return currentPosition.x * Vector3.right + currentPosition.y * Vector3.up;
	}

	void removeElement(GameObject clickedObject) {
		Vertex vertex = clickedObject.GetComponent<Vertex>();
		Edge edge = clickedObject.GetComponent<Edge>();
		if (vertex != null)
			graph.removeVertex(vertex);
		else if (edge != null)
			edge.remove();
	}

	void addOrRemoveElement() {
		if (Input.GetMouseButtonDown(1)) {
			RaycastHit hitInfo;
			GameObject clickedObject = getClickedObject(out hitInfo);
			if (clickedObject != null) {
				removeElement(clickedObject);
			} else {
				Vector3 vertexPos = getMouseWorldPos();
				graph.addVertex(vertexPos);
			}
		}

		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			if (Input.GetMouseButtonDown(0)) {
				RaycastHit hitInfo;
				GameObject clickedObject = getClickedObject(out hitInfo);
				if (start == null && clickedObject != null)
					start = clickedObject.GetComponent<Vertex>();
				else if (start != null && clickedObject != null) {
					end = clickedObject.GetComponent<Vertex>();
					if (end != null && start != end) {
						graph.addEdge(start, end);
						start = null;
						end = null;
					}
				}
			}
		}
	}
}
