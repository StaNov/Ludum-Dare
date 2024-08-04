using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PernicekController : MonoBehaviour {

	public float JumpForce = 100;
	public float MoveForce = 100;
	public float MaxSpeed = 10;

	public Transform[] Legs;

	public LayerMask GroundMask;

	public bool TurnedOn = false;

	private Rigidbody2D rb;
	private bool m_DesireJump;
	
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		m_DesireJump = false;
	}

	void Update() {
		if (!TurnedOn) {
			return;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			m_DesireJump = true;
		}
	}
	
	void FixedUpdate () {
		if (!TurnedOn) {
			return;
		}

		if (m_DesireJump) {
			m_DesireJump = false;
			foreach (var leg in Legs)
			{
				var legHit = Physics2D.Raycast(leg.position, Vector2.down, 0.1f, GroundMask);

				if (legHit.collider != null)
				{
					rb.AddForce(new Vector2(0, JumpForce));
					break;
				}
			}
		}

		float axis = Input.GetAxisRaw("Horizontal");
		if (rb.velocity.x >= -MaxSpeed && axis < 0 || rb.velocity.x <= MaxSpeed && axis > 0) {
			rb.AddForce(new Vector2(axis * MoveForce, 0));
		}
		
		rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -MaxSpeed, MaxSpeed), rb.velocity.y);
	}
}
