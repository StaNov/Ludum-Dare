using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PernicekController : MonoBehaviour {

	public float JumpForce = 100;
	public float MoveForce = 100;
	public float MaxSpeed = 10;

	private Rigidbody2D rb;
	
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * MoveForce, 0));

		if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0) {
			rb.AddForce(new Vector2(0, JumpForce));
		}

		rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -MaxSpeed, MaxSpeed), rb.velocity.y);
	}
}
