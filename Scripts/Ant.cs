using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour {
	bool reachedDestination;
	float speed = 4f;
	float pheremoneDeposit = 1f;

	[SerializeField] Vertex target;
	[SerializeField] List<Vertex> pathTraveled;
	[SerializeField] List<Edge> chosenEdges;
	// Start is called before the first frame update
	void Start() {
		reachedDestination = false;
		pathTraveled.Add(target);
		chooseEdge().addPheremone(pheremoneDeposit);
		pathTraveled.Add(target);
	}

	// Update is called once per frame
	void Update() {
		if (!reachedTarget()) {
			Vector3 direction = (target.transform.position - transform.position).normalized;
			transform.Translate(direction * speed * Time.deltaTime);
		} else {
			if (target.getIsDestination()) {
				reachedDestination = true;
				target = pathTraveled[pathTraveled.Count - 1];
				pathTraveled.RemoveAt(pathTraveled.Count - 1);
			} else if (target.getIsSource() && reachedDestination) {
				Destroy(gameObject);
				FindObjectOfType<AntGenerator>().generate();
			} else {
				if (reachedDestination) {
					Vertex prevTarget = target;
					target = pathTraveled[pathTraveled.Count - 1];
					pathTraveled.RemoveAt(pathTraveled.Count - 1);
					FindObjectOfType<Graph>().findEdge(prevTarget, target).addPheremone(pheremoneDeposit);
				} else {
					Edge chosenEdge = chooseEdge();
					chosenEdge.addPheremone(pheremoneDeposit);
					pathTraveled.Add(target);
					chosenEdges.Add(chosenEdge);
				}
			}
		}
	}

	bool reachedTarget() {
		float distance = Vector3.Distance(transform.position, target.transform.position);
		return distance < 0.05f;
	}

	Edge chooseEdge() {
		List<Edge> edges = target.getEdges().Except(chosenEdges).ToList();
		Edge chosenEdge = null;
		float upperLimit = 0;
		foreach (Edge edge in edges) {
			upperLimit += edge.getPheremone();
		}

		float chosenRange = Random.Range(0f, upperLimit);
		float probSum = 0;
		for (int i = 0; i < edges.Count; i++) {
			float edgeProbability = edges[i].getPheremone();
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
		chosenEdges.Clear();
		return chosenEdge;
	}

	public void setTarget(Vertex target) {
		this.target = target;
	}
}
