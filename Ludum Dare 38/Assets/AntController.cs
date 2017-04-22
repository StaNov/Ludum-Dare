using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {

	public float moveSpeed = 1;
	public float rotateSpeed = 1;
	public bool isLeader = false;

	public Transform[] bumpers;

	private const float minimumDistance = 5;

	private AntPosition currentPos;
	private static AntPosition leaderCurrentPos;

	private Rigidbody2D rb;
	
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}

	void OnEnable()
	{
		if (isLeader)
		{
			AntPosition pos = new AntPosition() { position = transform.position };
			currentPos = pos;
			leaderCurrentPos = pos;
		}

		ResetCurrentPos();
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
			AntPosition pos = new AntPosition() { position = transform.position };
			currentPos.next = pos;
			currentPos = pos;
			leaderCurrentPos = pos;

			rb.MovePosition(transform.position + transform.up * moveSpeed * Time.deltaTime);

			if (GetBlockingObject())
			{
				Debug.Log("GAME OVER");
				RenderSettings.ambientLight = Color.black; // TODO
			}
		}

		transform.Rotate(new Vector3(0, 0, -Input.GetAxisRaw("Horizontal") * rotateSpeed * 30 * Time.deltaTime));
	}

	private void ControlFollower() {

		// TODO mozna se da smazat, udela se v OnEnable
		if (currentPos == null) {
			ResetCurrentPos();
		}

		var blockingObject = GetBlockingObject();
		if (blockingObject)
		{
			if (blockingObject.GetBlockingObject() == this)
			{
				ResetCurrentPos();
				transform.rotation = Quaternion.identity;
				blockingObject.transform.rotation = Quaternion.identity;
			}

			return;
		}

		if ((transform.position - currentPos.position).magnitude < 0.01f && currentPos.next != null) {
			currentPos = currentPos.next;
		}

		Vector3 direction = currentPos.position - transform.position;
		float magnitude = Mathf.Min(direction.magnitude, moveSpeed * Time.deltaTime);
		direction = direction.normalized * magnitude;

		rb.MovePosition(transform.position + direction);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
	}

	private AntController GetBlockingObject()
	{
		foreach (Transform bumper in bumpers)
		{
			RaycastHit2D hit = Physics2D.Raycast(bumper.position, bumper.up, 1f);
			if (hit)
			{
				return hit.collider.GetComponent<AntController>();
			}
		}

		return null;
	}

	private void ResetCurrentPos()
	{
		currentPos = leaderCurrentPos;
	}

	private class AntPosition {
		public Vector3 position;
		public AntPosition next;
	}
}
