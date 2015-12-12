using UnityEngine;
using System.Collections;

public abstract class AbstractEnemyController : MonoBehaviour {

	public int enemySpeed = 5;

	private bool isVisible;
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

		if (col.collider.CompareTag("Player")) {
			OnPlayerTouch();
		}
	}

	void OnBecameInvisible() {
		isVisible = false;
	}

	void OnBecameVisible() {
		isVisible = true;
	}

	public bool IsVisible() {
		return isVisible;
	}

	abstract protected void MoveEnemy();
	abstract protected void OnPlayerTouch();
}
