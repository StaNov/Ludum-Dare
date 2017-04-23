using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour {

	public AntHill antHill;
	public GameObject innerPanel;

	void Awake() {
		innerPanel.SetActive(false);
	}

	void Update()
	{
		if (antHill.currentFoodSupply < - float.Epsilon)
		{
			OnGameOver();
		}
	}

	private void OnGameOver()
	{
		innerPanel.SetActive(true);
		GameManager.OnGameOver();
	}
}
