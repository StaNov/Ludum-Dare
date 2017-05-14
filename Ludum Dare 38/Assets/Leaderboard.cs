using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Leaderboard : MonoBehaviour {

	public GameObject leaderboardLinePrefab;
	public GameObject leaderboardLinePrefab1;
	public GameObject leaderboardLinePrefab2;
	public GameObject leaderboardLinePrefab3;
	public GameObject loading;

	IEnumerator Start ()
	{
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}

		loading.SetActive(true);

		WWW www = LeaderBoardConnector.FetchResultsAll(10);

		while (! www.isDone)
		{
			yield return null;
		}

		if (www.error != null)
		{
			yield break;
		}

		var lines = LeaderBoardConnector.ConvertStringToResult(www.text);

		if (!string.IsNullOrEmpty(NameManager.Name) && !lines.Any(r => r.name == NameManager.Name))
		{
			www = LeaderBoardConnector.FetchResultsPlayer(NameManager.Name);

			while (!www.isDone)
			{
				yield return null;
			}

			if (www.error != null)
			{
				yield break;
			}

			lines.AddRange(LeaderBoardConnector.ConvertStringToResult(www.text));
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

		loading.SetActive(false);
	}
}
