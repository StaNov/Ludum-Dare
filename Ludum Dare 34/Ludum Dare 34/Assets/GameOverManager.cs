using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public void RestartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void BackToMenu() {
		SceneManager.LoadScene(0);
	}
}
