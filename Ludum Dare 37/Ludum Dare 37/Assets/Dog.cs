using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {
	
	void OnTriggerEnter2D (Collider2D col) {
		PernicekRespawner respawner = col.GetComponent<PernicekRespawner>();

		if (respawner != null) {
			respawner.Respawn();
		}
	}
}
