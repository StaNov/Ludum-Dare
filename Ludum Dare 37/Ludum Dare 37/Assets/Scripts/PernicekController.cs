using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PernicekController : MonoBehaviour {

	public float JumpForce = 100;
	public float MoveForce = 100;
	public float MaxSpeed = 10;

	public Transform LeftLeg;
	public Transform RightLeg;

	public LayerMask GroundMask;

	private Rigidbody2D rb;
	private bool m_DesireJump;
	
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		m_DesireJump = false;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			m_DesireJump = true;
		}
	}
	
	void FixedUpdate () {
		rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * MoveForce, 0));

		if (m_DesireJump) {
			m_DesireJump = false;
			var leftHit = Physics2D.Raycast(LeftLeg.position, Vector2.down, 0.1f, GroundMask);
			var rightHit = Physics2D.Raycast(RightLeg.position, Vector2.down, 0.1f, GroundMask);
			if (leftHit.collider != null || rightHit.collider != null) {
				rb.AddForce(new Vector2(0, JumpForce));
			}
		}

		rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -MaxSpeed, MaxSpeed), rb.velocity.y);
	}
}