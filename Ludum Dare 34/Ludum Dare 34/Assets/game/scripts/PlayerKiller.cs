using UnityEngine;
using System.Collections;

public class PlayerKiller : MonoBehaviour {

	private GameObject gameOverMenu;

	void Start () {
		gameOverMenu = GameObject.Find("gameOverCanvas").transform.GetChild(0).gameObject;
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (! col.collider.CompareTag("Player")) {
			return;
		}

		gameOverMenu.SetActive(true);

		Camera camera = col.gameObject.GetComponentInChildren<Camera>();
		AudioSource audioSource = col.gameObject.GetComponentInChildren<AudioSource>();
		audioSource.Play();

		if (camera != null) {
			camera.transform.parent = null;
		}

		Destroy(col.gameObject);
	}
}
