using UnityEngine;
using System.Collections;

public class CapturedBee : MonoBehaviour {

	private Transform beesParent;
	public GameObject beePrefab;

	void Start () {
		beesParent = GameObject.Find("bees").transform;
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (!col.CompareTag("Player")) {
			return;
		}

		GameObject bee = (GameObject) Instantiate(beePrefab, transform.position, Quaternion.identity);
		bee.transform.parent = beesParent;
		Destroy(gameObject);
		BeeCountUpdater.GetInstance().UpdateBeeCount();
	}
}
