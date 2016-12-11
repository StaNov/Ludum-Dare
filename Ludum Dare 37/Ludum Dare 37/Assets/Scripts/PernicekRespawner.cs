using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PernicekRespawner : MonoBehaviour {

	public CameraMover Mover;

	private Vector3 m_StartPosition;
	private Rigidbody2D rb;
	private Collector Collector;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
		Collector = GetComponent<Collector>();
	}

	void Start() {
		m_StartPosition = transform.position;
	}

	public void Respawn() {
		transform.position = m_StartPosition;
		rb.velocity = Vector2.zero;
		Collector.ReturnCollectible();
		Mover.SetFloorFirst(true);
	}
}
