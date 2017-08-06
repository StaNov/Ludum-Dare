﻿using UnityEngine;

public class LeaderboardSaver : MonoBehaviour
{
	public GameState State;

	private bool _recordSaved;
	
	void Update () {
		if (!_recordSaved && State.GameOver != GameOverReason.StillPlaying)
		{
			StartCoroutine(ReusableLeaderboardManager.SaveNewRecord(
				"http://games.stanov.cz/ludum-dare-39/callLeaderboardSave.php",
				PlayerNameManager.PlayerName,
				Mathf.FloorToInt(State.Age)));
			
			_recordSaved = true;
		}
	}
}
