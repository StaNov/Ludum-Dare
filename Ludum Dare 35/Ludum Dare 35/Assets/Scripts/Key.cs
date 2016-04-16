using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public int doorId = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col) {
		Door door = col.collider.GetComponent<Door> ();

		if (door == null) {
			return;
		}

		if (door.keyId == doorId) {
			door.Open ();
			GetComponent<ShapeShiftController> ().ReleaseGhost ();
			Destroy (gameObject);
		}
	}
}
