using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {

	public Transform slot;
	public Collectible current = null;

	public bool HasCollectible { get { return current != null; } }

	void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag(Tags.COLLECTIBLE) && current == null && (col.transform.parent == null || ! col.transform.parent.CompareTag(Tags.DESTINATION))) {
			current = col.transform.GetComponent<Collectible>();

			current.transform.SetParent(slot);
			current.transform.localPosition = Vector3.zero;
			current.transform.localRotation = Quaternion.identity;
		}

		Destination dest = col.GetComponent<Destination>();
		if (dest != null && current != null && dest.Type == current.Type) {
			current.transform.parent = col.transform;
			current.transform.localPosition = Vector3.zero;
			current.transform.localRotation = Quaternion.identity;

			current.DisableTrigger();

			current = null;
		}
	}

	public void ReturnCollectible() {
		if (current == null) {
			return;
		}

		current.ReturnToOrigin();
		current = null;
	}
}
