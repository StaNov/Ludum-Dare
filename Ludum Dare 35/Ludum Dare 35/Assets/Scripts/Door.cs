using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public void Open() {
		transform.localScale = new Vector3 (0.1f, transform.localScale.y, transform.localScale.z);

		GetComponent<Collider2D> ().enabled = false;
	}
}
