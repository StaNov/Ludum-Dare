using UnityEngine;
using System.Collections;

public class ShapeShiftController : MonoBehaviour {

	public float speed = 10;
	public float jumpSpeed = 15;
	public float maxSpeed = 2;
	public Color highlightColor;

	private bool can_jump = true;
	private bool falling = false;


	public bool shapeshiftPermanent = false;

	private Rigidbody2D rb;
	private Animator animator;
	private SpriteRenderer spriteRenderer;

	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		if (Time.timeScale == 0) {
			return;
		}

		if (!can_jump && rb.velocity.y < 0) {
			falling = true;
		}
		if (!can_jump && falling && rb.velocity.y == 0 ) {
			can_jump = true;
			falling = false;
		}

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)) {
			ReleaseGhost ();
		}

		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1) ) && can_jump) {
			can_jump = false;
			rb.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);
			EffectsPlayer.instance.Play (EffectsPlayer.instance.jump);
		}

		rb.AddForce (new Vector2(Input.GetAxisRaw("Horizontal"), 0) * speed);
		rb.velocity = new Vector2 (
			Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed),
			rb.velocity.y
		);
	}

	public void CaptureGhost() {
		animator.SetTrigger ("Shapeshift");
		GhostController.instance.gameObject.SetActive (false);
		enabled = true;
		tag = "Player";
		Unhighlight ();
		EffectsPlayer.instance.Play (EffectsPlayer.instance.shapeshiftIn);
	}

	public void ReleaseGhost() {
		ReleaseGhost (false);
	}

	public void ReleaseGhost(bool releasingByDoor) {
		if (shapeshiftPermanent)
			return;

		animator.SetTrigger ("Shapeshift");
		GhostController.instance.gameObject.SetActive (true);
		GhostController.instance.transform.position = new Vector3 (
			transform.position.x,
			transform.position.y,
			GhostController.instance.transform.position.z);
		enabled = false;
		tag = "Untagged";
		Highlight ();
		EffectsPlayer.instance.Play (
			releasingByDoor ? EffectsPlayer.instance.openDoor : EffectsPlayer.instance.shapeshiftOut
		);
	}

	public void Highlight() {
		spriteRenderer.material.color = highlightColor;
	}

	public void Unhighlight() {
		spriteRenderer.material.color = Color.white;
	}
}
