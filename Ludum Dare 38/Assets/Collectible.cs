using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
	public int quantity = 30;
	public CollectibleType type;

	private bool waitingForAnts = true;
	
	void FixedUpdate () {
		if (waitingForAnts)
		{
			return;
		}

		transform.Translate(
			Input.GetAxisRaw("Horizontal") * Time.deltaTime * 10,
			Input.GetAxisRaw("Vertical") * Time.deltaTime * 10,
			0
		);
	}

	public AntController[] ReleaseAntsAndDestroy()
	{
		AntController[] ants = GetComponentsInChildren<AntController>();

		foreach (AntController ant in ants)
		{
			ant.transform.parent = null;
		}

		Destroy(gameObject);
		return ants;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (! waitingForAnts)
		{
			return;
		}

		if (! collision.collider.GetComponent<AntController>().isLeader)
		{
			return;
		}
		
		foreach (AntController ant in AntsManager.GetActiveAnts())
		{
			ant.enabled = false;
			ant.transform.parent = transform;
			SetAntPositionAndRotationAfterHit(ant);
			ant.GetComponent<Collider2D>().enabled = false;
		}
		
		waitingForAnts = false;
	}

	private void SetAntPositionAndRotationAfterHit(AntController ant)
	{
		float xPos = Random.Range(-10f, 10f);
		float yPos = Random.Range(-10f, 10f);
		Vector3 position = new Vector3(xPos, yPos, 0);
		position.Normalize();
		position *= (GetComponent<CircleCollider2D>().radius + (ant.GetComponent<BoxCollider2D>().size.y / 2) / transform.localScale.x);

		ant.transform.localPosition = position;
		ant.transform.localRotation = Quaternion.LookRotation(Vector3.forward, Vector3.zero - ant.transform.localPosition);
	}
}

public enum CollectibleType
{
	FOOD, MATERIAL
}
