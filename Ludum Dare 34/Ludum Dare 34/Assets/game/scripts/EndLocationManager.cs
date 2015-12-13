using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndLocationManager : MonoBehaviour {

	private AudioSource audioSource;

	void Start() {
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (!col.CompareTag("Player")) {
			return;
		}

		BeeCountStore.GetInstance().beesAtStartOfLevel = GameObject.Find("bees").transform.childCount;

		audioSource.Play();

		StartCoroutine(WaitAndLoadNextScene());
	}

	private IEnumerator WaitAndLoadNextScene() {
		yield return new WaitForSeconds(audioSource.clip.length);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
