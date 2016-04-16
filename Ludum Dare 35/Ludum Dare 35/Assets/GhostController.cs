using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

	public float ghostSpeed = 3;

	private Rigidbody2D rb;
	private ShapeShiftable currentCollidingShiftable;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		rb.AddForce (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * ghostSpeed);
	}

	void OnTriggerEnter2D (Collider2D col) {
		ShapeShiftable shapeShiftable = col.GetComponent<ShapeShiftable> ();

		if (shapeShiftable == null) {
			return;
		}

		currentCollidingShiftable = shapeShiftable;
	}

	void OnTriggerExit2D (Collider2D col) {
		currentCollidingShiftable = null;
	}
}
