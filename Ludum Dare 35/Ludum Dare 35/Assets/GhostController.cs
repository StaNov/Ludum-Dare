using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

	public float ghostSpeed = 3;

	private Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		rb.AddForce (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * ghostSpeed);
	}
}
