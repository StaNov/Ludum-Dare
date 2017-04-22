using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {

	public float moveSpeed = 1;
	public float rotateSpeed = 1;
	public AntController followingAnt = null;

	private const float minimumDistance = 5;

	private Rigidbody2D rb;
	
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		if (followingAnt == null) {
			ControlLeader();
		} else {
			ControlFollower();
		}
	}

	private void ControlLeader() {
		if (Input.GetAxisRaw("Vertical") > float.Epsilon) {
			rb.MovePosition(transform.position + transform.up * moveSpeed * Time.deltaTime);
		}

		transform.Rotate(new Vector3(0, 0, -Input.GetAxisRaw("Horizontal") * rotateSpeed * 30 * Time.deltaTime));
	}

	private void ControlFollower() {

		var lookPos = followingAnt.transform.position - transform.position;
		var newRotation = Quaternion.LookRotation(lookPos, Vector3.back);
		newRotation.y = 0;
		newRotation.x = 0;
		transform.rotation = newRotation;

		if (Vector3.Distance(followingAnt.transform.position, transform.position) < minimumDistance) {
			return;
		}
		

		rb.MovePosition(transform.position + transform.up * moveSpeed * Time.deltaTime);
	}
}
