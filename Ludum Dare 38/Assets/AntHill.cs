using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntHill : MonoBehaviour {

	public AntSpawner spawner;

	private float currentFoodSupply = 0;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Food food = col.GetComponent<Food>();

		if (food == null)
		{
			return;
		}

		currentFoodSupply += food.quantity;

		AntController[] ants = food.ReleaseAntsAndDestroy();

		spawner.AcceptAntsToSpawn(ants);
	}
}
