using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextSceneLoader : MonoBehaviour {

	public void LoadNextScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
