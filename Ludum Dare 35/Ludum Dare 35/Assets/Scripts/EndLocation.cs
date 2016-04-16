using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndLocation : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}
}
