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
		gameOverText.text = gameOverText.text.Replace("XXX", antHill.level.ToString()).Replace("YYY", "Gold");
		innerPanel.SetActive(true);
		GameManager.OnGameOver();
	}
}
