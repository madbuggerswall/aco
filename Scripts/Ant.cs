using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour {
	bool reachedDestination;
	bool destVisited;
	public static float speed = 4f;
	public static float pheromoneDeposit = 1f;
	Edge previousEdge;
	Edge chosenEdge;
	[SerializeField] Vertex target;
	[SerializeField] List<Vertex> pathTraveled;
	// Start is called before the first frame update
	void Start() {
		reachedDestination = false;
		pathTraveled.Add(target);
		chosenEdge = chooseEdge();
		chosenEdge.addPheromone(pheromoneDeposit);
		pathTraveled.Add(target);
	}

	// Update is called once per frame
	void Update() {
		if (!reachedTarget()) {
			Vector3 direction = (target.transform.position - transform.position).normalized;
			transform.Translate(direction * speed * Time.deltaTime);
		} else {
			if (target.getIsDestination() && !reachedDestination) {
				reachedDestination = true;
				pathTraveled.RemoveAt(pathTraveled.Count - 1);
			} else if (target.getIsSource() && reachedDestination) {
				Destroy(gameObject);
				FindObjectOfType<AntGenerator>().generate();
			} else {
				if (reachedDestination) {
					Vertex prevTarget = target;
					target = pathTraveled[pathTraveled.Count - 1];
					pathTraveled.RemoveAt(pathTraveled.Count - 1);
					FindObjectOfType<Graph>().findEdge(prevTarget, target).addPheromone(pheromoneDeposit);
				} else {
					previousEdge = chosenEdge;
					chosenEdge = chooseEdge();
					chosenEdge.addPheromone(pheromoneDeposit);
					pathTraveled.Add(target);
				}
			}
		}
	}

	bool reachedTarget() {
		float distance = Vector3.Distance(transform.position, target.transform.position);
		return distance < 0.05f;
	}

	Edge chooseEdge() {
		List<Edge> edges = new List<Edge>(target.getEdges());
		if (edges.Contains(previousEdge))
			edges.Remove(previousEdge);
		Edge chosenEdge = null;
		float upperLimit = 0;
		foreach (Edge edge in edges) {
			upperLimit += edge.getPheromone();
		}

		float chosenRange = Random.Range(0f, upperLimit);
		float probSum = 0;
		for (int i = 0; i < edges.Count; i++) {
			float edgeProbability = edges[i].getPheromone();
			probSum += edgeProbability;
			if (probSum >= chosenRange) {
				chosenEdge = edges[i];
				if (chosenEdge.getStart() != target)
					target = chosenEdge.getStart();
				else
					target = chosenEdge.getEnd();
				break;
			}
		}
		return chosenEdge;
	}

	public void setTarget(Vertex target) {
		this.target = target;
	}
}
