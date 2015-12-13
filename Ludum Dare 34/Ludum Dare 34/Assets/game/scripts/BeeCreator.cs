using UnityEngine;
using System.Collections;

public class BeeCreator : MonoBehaviour {

	public GameObject beePrefab;
	public AudioClip[] beeHitSounds;

	void Start () {
		BeeCountStore store = BeeCountStore.GetInstance();
		Transform beesParent = GameObject.Find("bees").transform;

		for (int i = 0; i < store.beesAtStartOfLevel; i++) {
			Vector3 beePosition = new Vector3(transform.position.x + (Random.value*2-1), transform.position.y + (Random.value*2-1), 0f);
			GameObject bee = (GameObject) Instantiate(beePrefab, beePosition, Quaternion.identity);
			bee.transform.parent = beesParent;
			bee.GetComponent<AudioSource>().clip = beeHitSounds[Random.Range(0, beeHitSounds.Length)];
		}

		BeeCountUpdater.GetInstance().UpdateBeeCount();
	}
}
