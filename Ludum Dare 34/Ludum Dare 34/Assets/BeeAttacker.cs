using UnityEngine;
using System.Collections;

public class BeeAttacker : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D col) {
		if (col.collider.CompareTag("Enemy")) {
			col.collider.GetComponent<EnemyLife>().Hurt();
		}
	}
}
