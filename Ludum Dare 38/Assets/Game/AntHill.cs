using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntHill : MonoBehaviour {

	public AntSpawner spawner;
	public CollectiblesManager collectiblesManager;
	public AudioClip mnamMnam;
	public AudioClip woohoo;

	public float currentFoodSupply { get { return m_CurrentFoodSupply; } }
	public int level { get { return m_Level; } }
	public int maxFoodSupply { get { return (level + 1) * 100; } }

	private float m_CurrentFoodSupply = 0;
	private int m_Level = 0;
	private AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	void Update () {
		m_CurrentFoodSupply -= 1 * level * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Collectible collectible = col.GetComponent<Collectible>();

		if (collectible == null || collectible.destroyed)
		{
			return;
		}

		switch (collectible.type)
		{
			case CollectibleType.FOOD:
				m_CurrentFoodSupply += collectible.quantity;
				m_CurrentFoodSupply = Mathf.Min(m_CurrentFoodSupply, maxFoodSupply);
				audioSource.clip = mnamMnam;
				break;
			case CollectibleType.MATERIAL:
				m_Level += collectible.quantity;
				audioSource.clip = woohoo;
				break;
		}

		audioSource.Play();

		AntController[] ants = collectible.ReleaseAntsAndDestroy();

		spawner.AcceptAntsToSpawn(ants);

		collectiblesManager.PopulateRandomSlot();
	}
}
