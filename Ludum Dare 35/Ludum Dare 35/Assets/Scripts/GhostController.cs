using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostController : MonoBehaviour {

	public float ghostSpeed = 3;

	private Rigidbody2D rb;
	private List<ShapeShiftController> currentCollidingShiftables;

	public static GhostController instance;

	private void Awake() {
		instance = this;
	}

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		currentCollidingShiftables = new List<ShapeShiftController> ();
	}

	void Update () {
		if (currentCollidingShiftables.Count > 0 && Input.GetKeyDown (KeyCode.Return)) {
			gameObject.SetActive (false);
			currentCollidingShiftables[0].GetComponent<ShapeShiftController>().enabled = true;
			currentCollidingShiftables.Clear ();
		}

		rb.AddForce (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * ghostSpeed);
	}

	void OnTriggerEnter2D (Collider2D col) {
		ShapeShiftController shapeShiftable = col.GetComponent<ShapeShiftController> ();

		if (shapeShiftable == null) {
			return;
		}

		currentCollidingShiftables.Add(shapeShiftable);
	}

	void OnTriggerExit2D (Collider2D col) {
		currentCollidingShiftables.Remove(col.GetComponent<ShapeShiftController>());
	}
}
