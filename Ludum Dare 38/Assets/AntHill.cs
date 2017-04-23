using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntHill : MonoBehaviour {

	public AntSpawner spawner;

	public float currentFoodSupply { get { return m_CurrentFoodSupply; } }
	public int level { get { return m_Level; } }

	private float m_CurrentFoodSupply = 0;
	private int m_Level = 0;
	
	void Update () {
		m_CurrentFoodSupply -= 1 * level * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Collectible collectible = col.GetComponent<Collectible>();

		if (collectible == null)
		{
			return;
		}

		switch (collectible.type)
		{
			case CollectibleType.FOOD:
				m_CurrentFoodSupply += collectible.quantity;
				break;
			case CollectibleType.MATERIAL:
				m_Level += collectible.quantity;
				break;
		}

		AntController[] ants = collectible.ReleaseAntsAndDestroy();

		spawner.AcceptAntsToSpawn(ants);
	}
}
