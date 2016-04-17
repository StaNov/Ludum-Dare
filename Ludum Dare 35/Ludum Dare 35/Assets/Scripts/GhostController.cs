using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostController : MonoBehaviour {

	public float ghostSpeed = 3;

	private Rigidbody2D rb;
	private ShapeShiftController currentCollidingShiftable;

	public static GhostController instance;

	private void Awake() {
		instance = this;
	}

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (Time.timeScale == 0) {
			return;
		}

		SetCurrentCollidingShiftable ();

		if (currentCollidingShiftable != null && Input.GetKeyDown (KeyCode.Return)) {
			currentCollidingShiftable.GetComponent<ShapeShiftController> ().CaptureGhost ();
			currentCollidingShiftable = null;
		}

		rb.AddForce (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * ghostSpeed);
	}

	private void SetCurrentCollidingShiftable() {
		Collider2D[] cols = Physics2D.OverlapPointAll (transform.position);

		currentCollidingShiftable = null;

		foreach (Collider2D col in cols) {
			ShapeShiftController controller = col.GetComponent<ShapeShiftController> ();
			if (controller != null) {
				currentCollidingShiftable = controller;
				break;
			}
		}
	}
}
