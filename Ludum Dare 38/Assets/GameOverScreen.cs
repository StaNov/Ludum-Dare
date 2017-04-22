using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour {

	public AntHill antHill;
	public GameObject innerPanel;

	void Awake() {
		innerPanel.SetActive(false);
	}

	void Update()
	{
		if (antHill.currentFoodSupply < 0)
		{
			innerPanel.SetActive(true);
			GameManager.OnGameOver();
		}
	}
}
