using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PernicekController : MonoBehaviour {

	private Rigidbody2D rb;
	
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * 100, 0));

		if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0) {
			rb.AddForce(new Vector2(0, 1000));
		}
	}
}