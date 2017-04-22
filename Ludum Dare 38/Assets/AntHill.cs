using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntHill : MonoBehaviour {

	public AntSpawner spawner;
	public float startingFoodSupply = 100; // TODO pak dat nulu, az s prvnim donesenym jidlem naplnit

	public float currentFoodSupply { get { return m_CurrentFoodSupply; } }
	public int level { get { return m_Level; } }

	private float m_CurrentFoodSupply = 0;
	private int m_Level = 0;

	void Start()
	{
		m_CurrentFoodSupply = startingFoodSupply;
	}

	void Update () {
		m_CurrentFoodSupply -= 1 * level * Time.deltaTime;
		m_CurrentFoodSupply -= 1 * AntsManager.GetLiveAntsCount() * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Food food = col.GetComponent<Food>();

		if (food == null)
		{
			return;
		}

		m_CurrentFoodSupply += food.quantity;

		AntController[] ants = food.ReleaseAntsAndDestroy();

		spawner.AcceptAntsToSpawn(ants);
	}
}
