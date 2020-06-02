using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntGenerator : MonoBehaviour {
	Graph graph;

	GameObject antPrefab;
	int antCount;
	
	void Awake() {
		graph = FindObjectOfType<Graph>();
		antPrefab = Resources.Load("Prefabs/Ant", typeof(GameObject)) as GameObject;
	}

	void generate(){
		Vertex source = graph.getSource();
		if(source != null){

		}
	}
}
