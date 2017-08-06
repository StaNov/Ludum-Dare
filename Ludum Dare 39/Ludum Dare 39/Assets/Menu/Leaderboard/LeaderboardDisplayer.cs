using System.Collections;
using UnityEngine;

public class LeaderboardDisplayer : MonoBehaviour
{
	public GameObject LinePrefab;

	IEnumerator Start ()
	{
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}
		
		yield return ReusableLeaderboardManager.GetLeaderboard(
			"59875abbb0b05d1ad4be0123", 
			PlayerNameManager.PlayerName,
			InstantiateLine);
	}

	private void InstantiateLine(int place, string playerName, int score)
	{
		var leaderBoardLine = Instantiate(LinePrefab).GetComponent<LeaderBoardLine>();
		leaderBoardLine.SetValues(place, playerName, score);
		leaderBoardLine.transform.SetParent(transform, false);
		
		if (playerName.Equals(PlayerNameManager.PlayerName))
			leaderBoardLine.SetCurrentPlayer();
	}
}
