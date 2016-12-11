using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingDirController : MonoBehaviour {
	
	private Rigidbody2D rb;
	private Animator animator;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	void Update () {
		animator.SetFloat("VelocityY", rb.velocity.x);
	}
}
