using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

	public float ghostSpeed = 3;

	private Rigidbody2D rb;
	private ShapeShiftController currentCollidingShapeShiftable;

	public static GhostController instance;

	private void Awake() {
		instance = this;
	}

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (currentCollidingShapeShiftable && Input.GetKeyDown (KeyCode.Return)) {
			gameObject.SetActive (false);
			currentCollidingShapeShiftable.GetComponent<ShapeShiftController>().enabled = true;
		}

		rb.AddForce (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * ghostSpeed);
	}

	void OnTriggerEnter2D (Collider2D col) {
		ShapeShiftController shapeShiftable = col.GetComponent<ShapeShiftController> ();

		if (shapeShiftable == null) {
			return;
		}

		currentCollidingShapeShiftable = shapeShiftable;
	}

	void OnTriggerExit2D (Collider2D col) {
		currentCollidingShapeShiftable = null;
	}
}
