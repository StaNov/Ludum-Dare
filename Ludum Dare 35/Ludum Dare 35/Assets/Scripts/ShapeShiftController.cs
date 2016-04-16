using UnityEngine;
using System.Collections;

public class ShapeShiftController : MonoBehaviour {

	public float speed = 10;
	public float jumpSpeed = 15;

	private Rigidbody2D rb;

	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
		rb.isKinematic = true;
	}

	void OnEnable() {
		rb.isKinematic = false;
	}

	void OnDisable() {
		rb.isKinematic = true;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			ReleaseGhost ();
		}

		if (Input.GetKeyDown (KeyCode.Space) && rb.velocity.y == 0) {
			rb.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);
		}

		rb.AddForce (new Vector2(Input.GetAxisRaw("Horizontal"), 0) * speed);
	}

	public void ReleaseGhost() {
		GhostController.instance.gameObject.SetActive (true);
		GhostController.instance.transform.position = new Vector3 (
			transform.position.x,
			transform.position.y,
			GhostController.instance.transform.position.z);
		enabled = false;
	}
}
