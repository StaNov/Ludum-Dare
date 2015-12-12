using UnityEngine;
using System.Collections;

public class DummyEnemyController : AbstractEnemyController {

	protected override void MoveEnemy() {
		Vector3 moveVector = (transform.position - player.position);
		moveVector.Normalize();
		transform.Translate(moveVector * enemySpeed * Time.deltaTime);
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
	}

	protected override void OnPlayerTouch() {
		// nothing
	}

}
