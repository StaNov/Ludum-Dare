using UnityEngine;
using System.Collections;

public class BeeController : MonoBehaviour {

	public int beeSpeed = 5;

	private Transform beesTarget;
	private Rigidbody2D rb;

	void Start () {
		beesTarget = GameObject.FindGameObjectWithTag("BeesTarget").transform;
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		Vector2 myPosition = new Vector2(transform.position.x, transform.position.y);
		Vector2 playerPosition = new Vector2(beesTarget.position.x, beesTarget.position.y);
		Vector2 forceVector = playerPosition - myPosition;
		forceVector.Normalize();
		rb.AddForce(forceVector * beeSpeed * rb.mass);
	}
}
