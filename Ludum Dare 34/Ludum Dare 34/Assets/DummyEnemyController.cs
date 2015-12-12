using UnityEngine;
using System.Collections;

public class DummyEnemyController : MonoBehaviour {

	public int enemySpeed = 5;

	private bool move = false;

	private Transform player;

	void Start() {
		player = GameObject.FindWithTag("Player").transform;
	}

	void FixedUpdate () {
		if (! move) {
			return;
		}

		Vector3 moveVector = (transform.position - player.position);
		moveVector.Normalize();
		transform.Translate(moveVector * enemySpeed * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.CompareTag("Player") || col.collider.CompareTag("Bee")) {
			move = true;
		}
	}
}
