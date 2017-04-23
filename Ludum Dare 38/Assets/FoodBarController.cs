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
	}
	
	void Update () {
		text.text = "Food: " + antHill.currentFoodSupply.ToString("0") + "/" + antHill.maxFoodSupply;
		Vector2 size = bar.sizeDelta;
		size.x = (antHill.currentFoodSupply / antHill.maxFoodSupply) * fullWidth;
		bar.DOSizeDelta(size, 1f);
	}
}
