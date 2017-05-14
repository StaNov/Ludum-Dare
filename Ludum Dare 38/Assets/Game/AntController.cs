using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {

	public float moveSpeed = 1;
	public float rotateSpeed = 1;
	public bool isLeader = false;
	public TutorialCanvas tutorial;

	public Transform[] bumpers;

	private const float minimumDistance = 5;

	private AntPosition currentPos;
	private float leaderDistanceWhileIAmStanding;
	private static AntPosition leaderLastPos;
	private static AntPosition leaderCurrentPos;
	private bool movingAutomatically;

	private Rigidbody2D rb;
	private Animator animator;

	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
	}

	void OnEnable()
	{
		if (isLeader)
		{
			AntPosition pos = new AntPosition() { position = transform.position };
			currentPos = pos;
			leaderCurrentPos = pos;
			leaderLastPos = pos;
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
		leaderLastPos = leaderCurrentPos;
		bool moved = false;

		if (Input.GetAxisRaw("Vertical") > float.Epsilon || movingAutomatically) {
			AntPosition pos = new AntPosition() { position = transform.position };
			currentPos.next = pos;
			currentPos = pos;
			leaderCurrentPos = pos;

			rb.MovePosition(transform.position + transform.up * moveSpeed * Time.deltaTime);
			moved = true;
		}

		if (!movingAutomatically)
		{
			transform.Rotate(new Vector3(0, 0, -Input.GetAxisRaw("Horizontal") * rotateSpeed * 30 * Time.deltaTime));
			//moved |= Mathf.Abs(Input.GetAxisRaw("Horizontal")) > float.Epsilon;
		}
		
		animator.SetBool("IsMoving", moved);

		if (Input.GetAxisRaw("Vertical") > float.Epsilon)
		{
			tutorial.gameObject.SetActive(false);
		}
	}

	private void ControlFollower() {

		var blockingObject = GetBlockingObject();
		if (blockingObject)
		{
			if (leaderDistanceWhileIAmStanding > 5)
			{
				ResetCurrentPos();
				transform.rotation = Quaternion.identity;
			}
			else
			{
				leaderDistanceWhileIAmStanding += Vector3.Distance(leaderCurrentPos.position, leaderLastPos.position);
			}

			animator.SetBool("IsMoving", false);
			return;
		}

		if ((transform.position - currentPos.position).magnitude < 0.01f && currentPos.next != null) {
			currentPos = currentPos.next;
		}

		Vector3 direction = currentPos.position - transform.position;
		float magnitude = Mathf.Min(direction.magnitude, moveSpeed * Time.deltaTime);
		direction = direction.normalized * magnitude;

		rb.MovePosition(transform.position + direction);
		animator.SetBool("IsMoving", true);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
		leaderDistanceWhileIAmStanding = 0;
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

	public void MoveForwardAuto()
	{
		StartCoroutine(MoveAuto());
	}

	public void PlayPickupSound()
	{
		GetComponent<AudioSource>().Play();
	}

	private IEnumerator MoveAuto()
	{
		movingAutomatically = true;
		yield return new WaitForSeconds(0.2f);
		movingAutomatically = false;
	}

	private class AntPosition {
		public Vector3 position;
		public AntPosition next;
	}
}
