using UnityEngine;
using System.Collections;

public abstract class AbstractEnemyController : MonoBehaviour {

	public int enemySpeed = 5;

	private bool shouldMove = false;
	private AudioSource audioSource;

	protected Transform player;

	void Start() {
		player = GameObject.FindWithTag("Player").transform;
		audioSource = GetComponent<AudioSource>();
	}

	void FixedUpdate () {
		if (! shouldMove || player == null) {
			return;
		}

		MoveEnemy();
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (!shouldMove && col.collider.CompareTag("Bee")) {
			shouldMove = true;
			audioSource.Play();
		}
	}

	abstract protected void MoveEnemy();
}
