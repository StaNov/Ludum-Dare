using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour {

	private static AntSpawner instance;

	public Transform leaderSpawnPoint;
	public GameObject antLeaderPrefab;
	public GameObject antPrefab;
	public AntHill antHill;

	private AntController antLeaderToSpawn;
	private List<AntController> remainingAntsToSpawn;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		remainingAntsToSpawn = new List<AntController>();
		antLeaderToSpawn = Instantiate(antLeaderPrefab).GetComponent<AntController>();
		SpawnAntLeader();
	}

	public void AcceptAntsToSpawn(AntController[] ants) {
		foreach (AntController ant in ants)
		{
			ant.transform.parent = transform;
			ant.gameObject.SetActive(false);
			if (ant.isLeader) {
				antLeaderToSpawn = ant;
			} else {
				remainingAntsToSpawn.Add(ant);
			}
		}

		while (remainingAntsToSpawn.Count < antHill.level) {
			AntController newAnt = Instantiate(antPrefab).GetComponent<AntController>();
			newAnt.gameObject.SetActive(false);
			remainingAntsToSpawn.Add(newAnt);
		}

		SpawnAntLeader();
		SpawnAntIfAvailable();
	}

	private void SpawnAntLeader()
	{
		antLeaderToSpawn.transform.position = leaderSpawnPoint.position;
		antLeaderToSpawn.transform.rotation = leaderSpawnPoint.rotation;
		antLeaderToSpawn.GetComponent<Collider2D>().enabled = true;
		antLeaderToSpawn.gameObject.SetActive(true);
		AntsManager.ReturnAntToActives(antLeaderToSpawn);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		SpawnAntIfAvailable();
	}

	private void SpawnAntIfAvailable()
	{
		if (remainingAntsToSpawn.Count > 0)
		{
			SpawnAnt();
		}
	}

	private void SpawnAnt()
	{
		AntController ant = remainingAntsToSpawn[0];
		ant.GetComponent<Collider2D>().enabled = true;
		ant.transform.position = transform.position;
		ant.gameObject.SetActive(true);
		AntsManager.ReturnAntToActives(ant);
		remainingAntsToSpawn.RemoveAt(0);
	}

	public static void ForceSpawnOfAllAnts()
	{
		while (instance.remainingAntsToSpawn.Count > 0)
		{
			instance.SpawnAnt();
		}
	}
}
