using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Vector3 targetPosition = Vector3.zero;

	public int playerSpeed = 10;

	void Update () {
		if (Input.GetMouseButton(0)) {
			Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			targetPosition = new Vector3(mousePoint.x, mousePoint.y, transform.position.z);
		} else {
			targetPosition = Vector3.zero;
		}
	}

	void FixedUpdate() {
		if (targetPosition == Vector3.zero) {
			return;
		}

		Vector3 moveDirection = targetPosition - transform.position;
		moveDirection.Normalize();

		transform.Translate(moveDirection * playerSpeed * Time.deltaTime);
	}
}
