using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsText : MonoBehaviour {

	public AntHill antHill;

	private Text text;

	void Awake()
	{
		text = GetComponent<Text>();
	}

	void Update () {
		text.text = "Level: " + antHill.level + ", Food: " + antHill.currentFoodSupply.ToString("0") + "/" + antHill.maxFoodSupply;
	}
}
