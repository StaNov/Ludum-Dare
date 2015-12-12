using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndLocationManager : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (!col.CompareTag("Player")) {
			return;
		}

		BeeCountStore.GetInstance().beesAtStartOfLevel = GameObject.Find("bees").transform.childCount;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
