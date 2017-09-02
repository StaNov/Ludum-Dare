using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analytics : MonoBehaviour
{
	public GameState State;
	private bool _eventSent;
	private const string PlayTimeTotalKey = "PlayTimeTotalGameOfLife";
	private const string PlayerIdKey = "PlayerIdGameOfLife";
	private const string PlayedGamesKey = "PlayedGamesGameOfLife";

	private int PlayTimeTotal
	{
		get { return PlayerPrefs.GetInt(PlayTimeTotalKey, 0); }
		set { PlayerPrefs.SetInt(PlayTimeTotalKey, value); }
	}

	private int PlayedGames
	{
		get { return PlayerPrefs.GetInt(PlayedGamesKey, 0); }
		set { PlayerPrefs.SetInt(PlayedGamesKey, value); }
	}

	private int PlayerId
	{
		get
		{
			int id = PlayerPrefs.GetInt(PlayerIdKey, 0);
			if (id == 0)
			{
				id = Random.Range(10000000, 100000000);
				PlayerPrefs.SetInt(PlayerIdKey, id);
			}

			return id;
		}
	}

	void Update () {
		if (!_eventSent && State.GameOver != null)
		{
			PlayTimeTotal += Mathf.FloorToInt(Time.timeSinceLevelLoad);
			PlayedGames++;

			UnityEngine.Analytics.Analytics.CustomEvent(
				"GameOver",
				new Dictionary<string, object>
				{
					{"Score", Mathf.FloorToInt(State.Age)},
					{"GameOverReason", State.GameOver.ToString()},
					{"PlayingTimeOneGame", Mathf.FloorToInt(Time.timeSinceLevelLoad)},
					{"PlayingTimeTotal", PlayTimeTotal},
					{"Name", PlayerNameManager.PlayerName},
					{"PlayerId", PlayerId},
				});

			_eventSent = true;
		}
	}
}
