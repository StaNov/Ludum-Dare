using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour {

	public GameObject leaderboardLinePrefab;
	public GameObject leaderboardLinePrefab1;
	public GameObject leaderboardLinePrefab2;
	public GameObject leaderboardLinePrefab3;

	void Start () {
		var lines = LeaderBoardConnector.Load(10, NameManager.Name);

		foreach(Transform child in transform)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < lines.Length; i++)
		{
			var line = lines[i];
			GameObject prefab = i == 0 ? leaderboardLinePrefab1 : i == 1 ? leaderboardLinePrefab2 : i == 2 ? leaderboardLinePrefab3 : leaderboardLinePrefab;
			GameObject newLine = Instantiate(prefab);

			newLine.transform.SetParent(transform, false);

			var lineComponent = newLine.GetComponent<LeaderboardLine>();
			lineComponent.place = line.place;
			lineComponent.playerName = line.name;
			lineComponent.score = line.score;
		}
	}
}
