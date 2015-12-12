using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStarter : MonoBehaviour {

	public void StartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
