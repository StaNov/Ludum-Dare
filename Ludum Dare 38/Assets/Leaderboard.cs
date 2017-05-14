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

		foreach (var line in lines)
		{
			GameObject prefab = line.place == 1 ? leaderboardLinePrefab1 : line.place == 2 ? leaderboardLinePrefab2 : line.place == 3 ? leaderboardLinePrefab3 : leaderboardLinePrefab;
			GameObject newLine = Instantiate(prefab);

			newLine.transform.SetParent(transform, false);

			var lineComponent = newLine.GetComponent<LeaderboardLine>();
			lineComponent.place = line.place;
			lineComponent.playerName = line.name;
			lineComponent.score = line.score;
			lineComponent.isCurrentPlayer = line.name == NameManager.Name;
		}
	}
}
