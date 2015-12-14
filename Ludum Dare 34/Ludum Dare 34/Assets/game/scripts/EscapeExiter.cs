using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EscapeExiter : MonoBehaviour {
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (SceneManager.GetActiveScene().buildIndex == 0) {
				Application.Quit();
			} else {
				SceneManager.LoadScene(0);
			}
		}
	}
}
