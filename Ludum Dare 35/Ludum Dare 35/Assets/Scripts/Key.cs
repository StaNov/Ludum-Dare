using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D col) {
		Door door = col.collider.GetComponent<Door> ();

		if (door == null) {
			return;
		}

		door.Open();
		if (GetComponent<ShapeShiftController> ().enabled) {
			GetComponent<ShapeShiftController> ().ReleaseGhost ();
		}
		Destroy (gameObject);
	}
}
