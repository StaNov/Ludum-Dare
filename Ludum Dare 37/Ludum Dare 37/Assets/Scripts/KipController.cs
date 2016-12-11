using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KipController : MonoBehaviour {

	public Transform Pernicek;
	public float MoveSpeed = 1;
	public float MaxSpeed = 2;
	public float JumpSpeed = 2;
	public float JumpInterval = 1;

	private Rigidbody2D rb;


	void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	IEnumerator Start() {
		while (true) {
			rb.AddForce(Vector2.up * JumpSpeed);
			yield return new WaitForSeconds(JumpInterval);
		}
	}

	void FixedUpdate () {
		rb.AddForce(new Vector2(Pernicek.position.x - transform.position.x, 0).normalized * MoveSpeed);
		
		rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -MaxSpeed, MaxSpeed), rb.velocity.y);
	}
}
