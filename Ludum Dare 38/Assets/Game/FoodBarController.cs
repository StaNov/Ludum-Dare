using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FoodBarController : MonoBehaviour {

	public AntHill antHill;
	public RectTransform bar;

	private float fullWidth;

	private Text text;

	
	void Awake () {
		text = GetComponentInChildren<Text>();
		fullWidth = bar.sizeDelta.x;

		Vector2 size = bar.sizeDelta;
		size.x = 0;
		bar.sizeDelta = size;
	}
	
	void Update () {
		float currentFood = Mathf.Max(0, antHill.currentFoodSupply);
		text.text = "Food: " + currentFood.ToString("0") + "/" + antHill.maxFoodSupply;
		Vector2 size = bar.sizeDelta;
		size.x = (currentFood / antHill.maxFoodSupply) * fullWidth;
		bar.DOSizeDelta(size, 1f);
	}
}
