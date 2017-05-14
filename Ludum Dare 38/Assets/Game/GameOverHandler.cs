using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameOverHandler : MonoBehaviour {

	public AntHill antHill;
	public GameObject innerPanel;
	public Text gameOverText;

	private bool gameOver = false;
	private bool leaderboardSent = false;

	void Awake() {
		innerPanel.SetActive(false);
	}

	void Update()
	{
		if (antHill.currentFoodSupply < - float.Epsilon)
		{
			StartCoroutine(OnGameOver());
		}

		if (leaderboardSent && Input.GetKeyDown(KeyCode.Return))
		{
			SceneManager.LoadScene(0);
		}
	}

	private IEnumerator OnGameOver()
	{
		gameOver = true;
		gameOverText.text = gameOverText.text.Replace("XXX", antHill.level.ToString()).Replace("YYY", Medal());
		innerPanel.SetActive(true);
		GameManager.OnGameOver();

		WWW www = LeaderBoardConnector.Save(NameManager.Name, antHill.level);
		while (! www.isDone)
		{
			yield return null;
		}

		leaderboardSent = true;
	}

	private string Medal()
	{
		if (antHill.level >= 70)
		{
			PlayerPrefs.SetString("medal-competitive", "gold");
			return "GOLD";
		}

		if (antHill.level >= 50)
		{
			if (PlayerPrefs.GetString("medal-competitive", "") != "gold")
			{
				PlayerPrefs.SetString("medal-competitive", "silver");
			}
			
			return "SILVER";
		}

		if (antHill.level >= 30)
		{
			if (PlayerPrefs.GetString("medal-competitive", "") != "gold" || PlayerPrefs.GetString("medal-competitive", "") != "silver")
			{
				PlayerPrefs.SetString("medal-competitive", "bronze");
			}

			return "BRONZE";
		}

		return "no";
	}
}
