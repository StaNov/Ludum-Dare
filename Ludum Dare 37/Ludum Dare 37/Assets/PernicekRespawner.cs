using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PernicekRespawner : MonoBehaviour {

	private Vector3 m_StartPosition;
	private Rigidbody2D rb;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	void Start() {
		m_StartPosition = transform.position;
	}

	public void Respawn() {
		transform.position = m_StartPosition;
		rb.velocity = Vector2.zero;
	}
}
