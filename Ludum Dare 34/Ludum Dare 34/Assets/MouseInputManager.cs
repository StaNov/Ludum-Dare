using UnityEngine;
using System.Collections;

public class MouseInputManager : MonoBehaviour {

	private Transform playerTarget;
	private Transform beesTarget;
	private Transform player;

	void Start () {
		player = GameObject.FindWithTag("Player").transform;
		playerTarget = GameObject.FindWithTag("PlayerTarget").transform;
		beesTarget = GameObject.FindWithTag("BeesTarget").transform;
	}

	void Update () {
		if (Input.GetMouseButton(0)) {
			Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 mousePointPosition = new Vector3(mousePoint.x, mousePoint.y, transform.position.z);
			bool enemyClicked = checkEnemyClicked(mousePointPosition);

			playerTarget.position = enemyClicked ? Vector3.zero : mousePointPosition;
			beesTarget.position = enemyClicked ? mousePointPosition : player.position;
		} else {
			playerTarget.position = Vector3.zero;
			beesTarget.position = player.position;
		}
	}

	private bool checkEnemyClicked(Vector3 mousePointPosition) {
		Collider2D col = Physics2D.OverlapPoint(new Vector2(mousePointPosition.x, mousePointPosition.y));

		return col != null && col.gameObject.CompareTag("Enemy");
	}
}
