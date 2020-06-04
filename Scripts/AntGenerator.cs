using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class AntGenerator : MonoBehaviour {
	Graph graph;

	GameObject antPrefab;
	public static int antCount = 0;

	void Awake() {
		graph = FindObjectOfType<Graph>();
		antPrefab = Resources.Load("Prefabs/Ant", typeof(GameObject)) as GameObject;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			for (int i = 0; i < 10; i++) {
				generate();
			}
		}
		if (Input.GetKeyDown(KeyCode.A)) {
				generate();
		}
	}

	public void generate() {
		Vertex source = graph.getSource();
		if (source != null) {
			GameObject ant = Instantiate(antPrefab, source.transform.position, Quaternion.identity);
			ant.GetComponent<Ant>().setTarget(source);
			antCount++;
		}
	}
}
