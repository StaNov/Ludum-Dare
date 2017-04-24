using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour {
	
	void Awake () {
		Time.timeScale = 1;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
		{
			SceneManager.LoadScene(1);
		}
	}
}
