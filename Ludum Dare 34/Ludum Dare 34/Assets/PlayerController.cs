using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform targetPosition;

	public int playerSpeed = 10;

	void Start() {
		targetPosition = GameObject.FindWithTag("PlayerTarget").transform;
	}

	void FixedUpdate() {
		if (targetPosition.position == Vector3.zero) {
			return;
		}

		Vector3 moveDirection = targetPosition.position - transform.position;
		moveDirection.Normalize();

		transform.Translate(moveDirection * playerSpeed * Time.deltaTime);
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
	}
}
