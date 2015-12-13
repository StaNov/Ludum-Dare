using UnityEngine;
using System.Collections;

public class CapturedBee : MonoBehaviour {

	private Transform beesParent;
	private GameObject audioPlayer;
	private AudioSource audioSource;

	public GameObject beePrefab;

	void Start () {
		beesParent = GameObject.Find("bees").transform;
		audioPlayer = transform.GetChild(0).gameObject;
		audioSource = audioPlayer.GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (!col.CompareTag("Player")) {
			return;
		}

		GameObject bee = (GameObject) Instantiate(beePrefab, transform.position, Quaternion.identity);
		bee.transform.parent = beesParent;
		BeeCountUpdater.GetInstance().UpdateBeeCount();

		audioSource.Play();
		audioPlayer.transform.parent = null;
		Destroy(audioPlayer, audioSource.clip.length);
		Destroy(gameObject);
	}
}
