using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	void Start () {
		Time.timeScale = 1;
		Cursor.visible = false;
	}

	public static void OnGameOver()
	{
		Cursor.visible = true;
		Time.timeScale = 0;
	}
}
