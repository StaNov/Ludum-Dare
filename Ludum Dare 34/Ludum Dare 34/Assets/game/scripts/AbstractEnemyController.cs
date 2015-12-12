using UnityEngine;
using System.Collections;

public abstract class AbstractEnemyController : MonoBehaviour {

	public int enemySpeed = 5;

	private bool shouldMove;

	protected Transform player;

	void Start() {
		player = GameObject.FindWithTag("Player").transform;
		shouldMove = false;
	}

	void FixedUpdate () {
		if (! shouldMove) {
			return;
		}

		MoveEnemy();
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (!shouldMove && col.collider.CompareTag("Bee")) {
			shouldMove = true;
		}
	}

	abstract protected void MoveEnemy();
}
