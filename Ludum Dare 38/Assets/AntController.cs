using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {

	public float moveSpeed = 1;
	public float rotateSpeed = 1;
	public bool isLeader = false;

	public Transform front;

	private const float minimumDistance = 5;

	private AntPosition currentPos;
	private static AntPosition leaderCurrentPos;

	private Rigidbody2D rb;
	
	void Awake () {
		rb = GetComponent<Rigidbody2D>();

		if (isLeader) {
			AntPosition pos = new AntPosition() { position = transform.position, rotation = transform.rotation };
			currentPos = pos;
			leaderCurrentPos = pos;
		}
	}
	
	void FixedUpdate ()
	{
		if (isLeader) {
			ControlLeader();
		} else {
			ControlFollower();
		}
	}

	private void ControlLeader() {
		if (Input.GetAxisRaw("Vertical") > float.Epsilon) {
			AntPosition pos = new AntPosition() { position = transform.position, rotation = transform.rotation };
			currentPos.next = pos;
			currentPos = pos;
			leaderCurrentPos = pos;

			rb.MovePosition(transform.position + transform.up * moveSpeed * Time.deltaTime);

			if (IsSomethingInFront())
			{
				Debug.Log("GAME OVER");
				RenderSettings.ambientLight = Color.black; // TODO
			}
		}

		transform.Rotate(new Vector3(0, 0, -Input.GetAxisRaw("Horizontal") * rotateSpeed * 30 * Time.deltaTime));
	}

	private void ControlFollower() {

		if (currentPos == null) {
			currentPos = leaderCurrentPos;
		}

		if (IsSomethingInFront())
		{
			return;
		}

		if ((transform.position - currentPos.position).magnitude < 0.01f && currentPos.next != null) {
			currentPos = currentPos.next;
		}

		Vector3 direction = currentPos.position - transform.position;
		float magnitude = Mathf.Min(direction.magnitude, moveSpeed * Time.deltaTime);
		direction = direction.normalized * magnitude;

		rb.MovePosition(transform.position + direction);
		transform.rotation = currentPos.rotation;
	}

	private bool IsSomethingInFront()
	{
		return Physics2D.Raycast(front.position, front.up, 1f);
	}

	private class AntPosition {
		public Vector3 position;
		public Quaternion rotation;
		public AntPosition next;
	}
}
