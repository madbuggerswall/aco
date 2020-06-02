using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour {
	Vertex start;
	Vertex end;
	float cost;
	float pheremone;
	public static float evaporizationSpeed = 0.5f;
	public static float lineThickness = 0.06f;

	void Awake() {
		pheremone = 1;
	}

	void Update() {
		evaporate();
	}

	void adjustColor() {
		float green = pheremone * 20 / 255f;
		GetComponent<MeshRenderer>().material.color = new Color(0, green, 1f);
	}

	void evaporate() {
		if (pheremone > 1f) {
			pheremone -= evaporizationSpeed * Time.deltaTime;
			adjustColor();
		}
	}

	void setPosition(Vector3 start, Vector3 end) {
		Vector3 direction = end - start;
		Vector3 midPoint = direction / 2f + start;
		transform.position = midPoint;
		transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
		transform.eulerAngles = new Vector3(transform.eulerAngles.x * Mathf.Sign(direction.x), 90, -90);
		transform.localScale = new Vector3(lineThickness, 1, direction.magnitude / 10f);
	}

	public void initialize(Vertex start, Vertex end) {
		this.start = start;
		this.end = end;
		start.addEdge(this);
		end.addEdge(this);
		cost = Vector3.Distance(start.transform.position, end.transform.position);
		setPosition(start.transform.position, end.transform.position);
	}

	public void updatePosition() {
		setPosition(start.transform.position, end.transform.position);
	}

	public void remove() {
		start.removeEdge(this);
		end.removeEdge(this);
		Destroy(gameObject);
	}

	public void addPheremone(float pheremone) {
		this.pheremone += pheremone;
		adjustColor();
	}

	public float getPheremone() {
		return pheremone;
	}

	public Vertex getStart() {
		return start;
	}

	public Vertex getEnd() {
		return end;
	}
}
