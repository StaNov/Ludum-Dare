using UnityEngine;
using System.Collections;

public class BeeCreator : MonoBehaviour {

	public GameObject beePrefab;

	void Start () {
		BeeCountStore store = BeeCountStore.GetInstance();
		Transform beesParent = GameObject.Find("bees").transform;

		for (int i = 0; i < store.beesAtStartOfLevel; i++) {
			Vector3 beePosition = new Vector3(transform.position.x + (Random.value*2-1), transform.position.y + (Random.value*2-1), transform.position.z);
			GameObject bee = (GameObject) Instantiate(beePrefab, beePosition, Quaternion.identity);
			bee.transform.parent = beesParent;
		}

		BeeCountUpdater.GetInstance().UpdateBeeCount();
	}
}
