using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonStart : MonoBehaviour {

	public void StartGame() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}
}
