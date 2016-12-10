using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {

	public Transform slot;
	
	void OnTriggerEnter2D (Collider2D col) {
		col.transform.SetParent(slot);

		col.transform.localPosition = Vector3.zero;
		col.transform.localRotation = Quaternion.identity;
	}
}
