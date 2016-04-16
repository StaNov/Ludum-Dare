using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RestartLevel : MonoBehaviour {

	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
