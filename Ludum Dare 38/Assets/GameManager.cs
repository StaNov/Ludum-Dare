using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	void Start () {
		Time.timeScale = 1;
	}

	public static void OnGameOver()
	{
		Time.timeScale = 0;
	}
}
