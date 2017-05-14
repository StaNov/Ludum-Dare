using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour {

	public GameObject leaderboardLinePrefab;
	
	void Start () {
		var lines = LeaderBoardConnector.Load(10, NameManager.Name);

		foreach(Transform child in transform)
		{
			Destroy(child.gameObject);
		}

		foreach(var line in lines)
		{
			GameObject newLine = Instantiate(leaderboardLinePrefab);

			newLine.transform.SetParent(transform, false);

			var lineComponent = newLine.GetComponent<LeaderboardLine>();
			lineComponent.place = line.place;
			lineComponent.playerName = line.name;
			lineComponent.score = line.score;
		}
	}
}
