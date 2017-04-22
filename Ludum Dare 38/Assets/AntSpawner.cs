using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour {

	public Transform leaderSpawnPoint;

	private AntController antLeaderToSpawn;
	private List<AntController> remainingAntsToSpawn;

	void Awake()
	{
		remainingAntsToSpawn = new List<AntController>();
	}

	public void AcceptAntsToSpawn(AntController[] ants) {
		foreach (AntController ant in ants)
		{
			ant.transform.parent = transform;
			ant.gameObject.SetActive(false);
			if (ant.isLeader) {
				antLeaderToSpawn = ant;
			} else
			{
				remainingAntsToSpawn.Add(ant);
			}
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
			AntController ant = remainingAntsToSpawn[0];
			ant.GetComponent<Collider2D>().enabled = true;
			ant.transform.position = transform.position;
			ant.gameObject.SetActive(true);
			AntsManager.ReturnAntToActives(ant);
			remainingAntsToSpawn.RemoveAt(0);
		}
	}
}
