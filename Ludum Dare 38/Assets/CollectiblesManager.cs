using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour {

	public GameObject[] foodPrefabs;
	public GameObject[] materialPrefabs;
	public int[] levelToStartGenerateNext;

	public AntHill antHill;

	private int currentPrefabLevel {
		get {
			for (int i = 0; i < levelToStartGenerateNext.Length; i++)
			{
				if (levelToStartGenerateNext[i] > antHill.level)
				{
					return i;
				}
			}
			
			return levelToStartGenerateNext[levelToStartGenerateNext.Length - 1];
		}
	}

	private CollectibleSlot[] slots;

	void Awake()
	{
		slots = GetComponentsInChildren<CollectibleSlot>();
	}
	
	void Start () {
		for (int i = 0; i <= slots.Length / 2; i++)
		{
			PopulateRandomSlot();
		}
	}

	public void PopulateRandomSlot()
	{
		int food;
		int material;
		var freeSlots = GetFreeSlots(out food, out material);
		CollectibleSlot slot = freeSlots[Random.Range(0, freeSlots.Length - 1)];

		GameObject[] prefabs = food <= material ? foodPrefabs : materialPrefabs;
		GameObject prefab = prefabs[currentPrefabLevel];

		Instantiate(prefab, slot.transform);
	}

	private CollectibleSlot[] GetFreeSlots(out int food, out int material)
	{
		List<CollectibleSlot> freeSlots = new List<CollectibleSlot>();
		food = 0;
		material = 0;

		foreach (var slot in slots)
		{
			if (! slot.isPopulated)
			{
				freeSlots.Add(slot);
			}
			else
			{
				int _ = slot.GetComponentInChildren<Collectible>().type == CollectibleType.FOOD ? food++ : material++;
			}
		}

		return freeSlots.ToArray();
	}
}
