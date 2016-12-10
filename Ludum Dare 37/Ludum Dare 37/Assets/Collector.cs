using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {

	public Transform slot;
	public Transform current = null;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag(Tags.COLLECTIBLE) && current == null && (col.transform.parent == null || ! col.transform.parent.CompareTag(Tags.DESTINATION))) {
			current = col.transform;

			current.SetParent(slot);
			current.localPosition = Vector3.zero;
			current.localRotation = Quaternion.identity;
		}

		if (col.CompareTag(Tags.DESTINATION) && current != null) {
			current.parent = col.transform;
			current.localPosition = Vector3.zero;
			current.localRotation = Quaternion.identity;

			current = null;
		}
	}
}
