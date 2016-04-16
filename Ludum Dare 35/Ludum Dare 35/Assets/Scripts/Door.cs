using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public int keyId = 0;

	public void Open() {
		transform.localScale = new Vector3 (0.1f, transform.localScale.y, transform.localScale.z);

		GetComponent<Collider2D> ().enabled = false;
	}
}
