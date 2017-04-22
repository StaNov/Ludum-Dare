using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {

	public float moveSpeed = 1;
	public float rotateSpeed = 1;

	private Rigidbody2D rb;
	
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (Input.GetAxisRaw("Vertical") > float.Epsilon) {
			rb.MovePosition(transform.position + transform.up * moveSpeed * Time.deltaTime);
		}

		transform.Rotate(new Vector3(0, 0, - Input.GetAxisRaw("Horizontal") * rotateSpeed * 30 * Time.deltaTime));
	}
}
