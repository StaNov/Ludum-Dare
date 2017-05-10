using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour {

	public NameField field;
	
	void Awake () {
		Time.timeScale = 1;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
		{
			NameManager.Name = field.fieldText;
			SceneManager.LoadScene(1);
		}
	}
}
