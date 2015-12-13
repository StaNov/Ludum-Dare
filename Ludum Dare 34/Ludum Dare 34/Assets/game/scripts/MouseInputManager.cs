using UnityEngine;
using System.Collections;

public class MouseInputManager : MonoBehaviour {

	private Transform playerTarget;
	private Transform beesTarget;
	private Transform player;

	private bool moving = false;
	private bool attacking = false;
	private Transform targetedEnemy = null;

	private bool gameOver = false;

	void Start () {
		player = GameObject.FindWithTag("Player").transform;
		playerTarget = GameObject.FindWithTag("PlayerTarget").transform;
		beesTarget = GameObject.FindWithTag("BeesTarget").transform;
	}

	void Update () {

		if (gameOver) {
			return;
		}

		Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 mousePointPosition = new Vector3(mousePoint.x, mousePoint.y, transform.position.z);
		Transform enemyUnderMouse = GetEnemyUnderMouse(mousePointPosition);

		if (Input.GetMouseButtonDown(0)) {
			targetedEnemy = enemyUnderMouse;

			if (targetedEnemy != null) {
				attacking = true;
			} else {
				moving = true;
			}
		}

		if (Input.GetMouseButtonUp(0) || (!moving && targetedEnemy == null)) {
			attacking = false;
			moving = false;
			targetedEnemy = null;
			playerTarget.position = Vector3.zero;
			beesTarget.position = new Vector3(player.position.x, player.position.y, beesTarget.position.z);
		}

		if (attacking) {
			playerTarget.position = Vector3.zero;
			beesTarget.position = new Vector3(mousePointPosition.x, mousePointPosition.y, beesTarget.position.z);

		}

		if (moving) {
			playerTarget.position = mousePointPosition;
			beesTarget.position = new Vector3(player.position.x, player.position.y, beesTarget.position.z);
		}


	}

	private Transform GetEnemyUnderMouse(Vector3 mousePointPosition) {
		Collider2D col = Physics2D.OverlapPoint(new Vector2(mousePointPosition.x, mousePointPosition.y));

		if (col == null || !col.gameObject.CompareTag("Enemy")) {
			return null;
		}

		return col.transform;
	}
}
