using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour {

	public AntHill antHill;
	public GameObject innerPanel;
	public Text gameOverText;

	private bool gameOver = false;

	void Awake() {
		innerPanel.SetActive(false);
	}

	void Update()
	{
		if (antHill.currentFoodSupply < - float.Epsilon)
		{
			OnGameOver();
		}

		if (gameOver && Input.GetKeyDown(KeyCode.Return))
		{
			SceneManager.LoadScene(0);
		}
	}

	private void OnGameOver()
	{
		gameOver = true;
		gameOverText.text = gameOverText.text.Replace("XXX", antHill.level.ToString()).Replace("YYY", Medal());
		innerPanel.SetActive(true);
		GameManager.OnGameOver();
	}

	private string Medal()
	{
		if (antHill.level >= 40)
		{
			PlayerPrefs.SetString("medal", "gold");
			return "GOLD";
		}

		if (antHill.level >= 30)
		{
			if (PlayerPrefs.GetString("medal", "") != "gold")
			{
				PlayerPrefs.SetString("medal", "silver");
			}
			
			return "SILVER";
		}

		if (antHill.level >= 20)
		{
			if (PlayerPrefs.GetString("medal", "") != "gold" || PlayerPrefs.GetString("medal", "") != "silver")
			{
				PlayerPrefs.SetString("medal", "bronze");
			}

			return "BRONZE";
		}

		return "no";
	}
}
