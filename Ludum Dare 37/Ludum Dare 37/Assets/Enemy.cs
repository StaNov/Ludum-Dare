using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		OnCollision(col.collider);
	}

	void OnTriggerEnter2D(Collider2D col) {
		OnCollision(col);
	}


	private void OnCollision (Collider2D col) {
		PernicekRespawner respawner = col.GetComponent<PernicekRespawner>();

		if (respawner != null) {
			respawner.Respawn();
		}
	}
}
