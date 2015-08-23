using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private bool movingRight;
	private bool movingLeft;
	private Animator animator;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
		movingRight = false;
		movingLeft = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			movingLeft = false;
			movingRight = true;
			animator.SetTrigger("walkRight");
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			movingRight = false;
			//animator.ResetTrigger("walkRight"); // TODO je potřeba?
			animator.SetTrigger("stop");
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			movingRight = false;
			movingLeft = true;
			animator.SetTrigger("walkLeft");
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			movingLeft = false;
			animator.SetTrigger("stop");
		}

		if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0) {
			rb.AddForce(Vector2.up * 11, ForceMode2D.Impulse);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (movingRight) {
			rb.AddForce(Vector2.right * 10);
		}
		if (movingLeft) {
			rb.AddForce(Vector2.left * 10);
		}
	}
}
