using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostController : MonoBehaviour {

	public float ghostSpeed = 3;
	public float maxSpeed = 3;
	public LayerMask onlyShiftables;

	private Rigidbody2D rb;
	private ShapeShiftController currentCollidingShiftable;
	private List<ShapeShiftController> currentCollidingShiftables;

	public static GhostController instance;

	private void Awake() {
		instance = this;
	}

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		currentCollidingShiftables = new List<ShapeShiftController>();
	}

	void Update () {
		if (Time.timeScale == 0) {
			return;
		}

		SetCurrentCollidingShiftable ();

		if (currentCollidingShiftable != null && ( Input.GetKeyDown (KeyCode.Return)  || Input.GetKeyDown(KeyCode.JoystickButton0))) {
			currentCollidingShiftable.GetComponent<ShapeShiftController> ().CaptureGhost ();
			currentCollidingShiftable = null;
			currentCollidingShiftables.Clear ();
		}

		if (rb.velocity.magnitude < maxSpeed) {
			rb.AddForce (new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")) * ghostSpeed);
		} else {
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		ShapeShiftController controller = col.GetComponent<ShapeShiftController> ();

		if (controller != null) {
			currentCollidingShiftables.Add (col.GetComponent<ShapeShiftController> ());
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		currentCollidingShiftables.Remove(col.GetComponent<ShapeShiftController>());
	}

	private void SetCurrentCollidingShiftable() {
		if (currentCollidingShiftable == null && currentCollidingShiftables.Count == 0) {
			return;
		}

		if (currentCollidingShiftables.Count != 0 && currentCollidingShiftable == currentCollidingShiftables [0]) {
			return;
		}

		if (currentCollidingShiftable != null) {
			currentCollidingShiftable.Unhighlight ();
			currentCollidingShiftable = null;
		}

		if (currentCollidingShiftables.Count != 0) {
			currentCollidingShiftable = currentCollidingShiftables[0];
			currentCollidingShiftable.Highlight ();
		}
	}
}
