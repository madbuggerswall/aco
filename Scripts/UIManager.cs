using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField] InputField evaporationRate;
	[SerializeField] InputField antSpeed;
	[SerializeField] InputField pheromoneDeposit;
	[SerializeField] InputField maxPheromone;
	[SerializeField] InputField solutionMultiplier;
	[SerializeField] Button set;
	[SerializeField] Button kill;
	[SerializeField] Button defaultValues;

	void Start() {
		updateFields();
	}

	public void onSetPressed() {
		Edge.evaporationRate = float.Parse(evaporationRate.text);
		Edge.maxPheromone = float.Parse(maxPheromone.text);
		Ant.speed = float.Parse(antSpeed.text);
		Ant.pheromoneDeposit = float.Parse(pheromoneDeposit.text);
		Ant.solutionMultiplier = float.Parse(solutionMultiplier.text);
	}

	public void onKillPressed() {
		Ant[] ants = FindObjectsOfType<Ant>();
		foreach (Ant ant in ants) {
			Destroy(ant.gameObject);
		}
	}

	public void onDefaultPressed() {
		Edge.evaporationRate = 2f;
		Edge.maxPheromone = 20f;
		Ant.speed = 4f;
		Ant.pheromoneDeposit = 1f;
		Ant.solutionMultiplier = 1f;
		updateFields();
	}

	void updateFields() {
		evaporationRate.text = Edge.evaporationRate.ToString();
		antSpeed.text = Ant.speed.ToString();
		pheromoneDeposit.text = Ant.pheromoneDeposit.ToString();
		maxPheromone.text = Edge.maxPheromone.ToString();
		solutionMultiplier.text = Ant.solutionMultiplier.ToString();
	}
}
