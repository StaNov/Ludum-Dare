using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	public Transform antsParent;

	public float quantity = 3;

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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (! waitingForAnts)
		{
			return;
		}
		
		foreach (AntController ant in AntsManager.GetActiveAnts())
		{
			ant.enabled = false;
			ant.transform.parent = transform;
			ant.transform.localPosition = Vector3.zero;
			ant.transform.localRotation = Quaternion.identity;
		}
		
		waitingForAnts = false;
	}
}
