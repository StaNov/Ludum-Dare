using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

	public CollectibleType Type;
	public Destination Destination;

	private Vector3 m_OriginalPosition;
	private Transform m_CollectiblesParent;

	void Start() {
		m_OriginalPosition = transform.position;
		m_CollectiblesParent = transform.parent;
	}

	public void ReturnToOrigin() {
		transform.position = m_OriginalPosition;
		transform.rotation = Quaternion.identity;
		transform.parent = m_CollectiblesParent;
	}

	public void DisableTrigger() {
		transform.GetChild(0).GetComponentInChildren<Collider2D>().enabled = false;
	}
}

public enum CollectibleType {
	STAR,
	TREE,
	SNOWFLAKE,
	LAMP,
	CANDLES
}
