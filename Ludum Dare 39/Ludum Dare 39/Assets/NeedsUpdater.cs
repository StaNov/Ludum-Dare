using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedsUpdater : MonoBehaviour
{

	public Text MyEnergy;
	public Text MyFood;
	public Text MyHappiness;
	public Text MyHealth;
	public Text FamilyFood;
	public Text FamilyHappiness;
	public Text FamilyHealth;
	public Text Age;

	private void Update ()
	{
		MyEnergy.text = "My energy: " + Mathf.CeilToInt(GameState.Instance.MyEnergy) + " / " + Mathf.CeilToInt(GameState.Instance.MyMaxEnergy);
		MyFood.text = "My food: " + Mathf.CeilToInt(GameState.Instance.MyFood);
		MyHappiness.text = "My happiness: " + Mathf.CeilToInt(GameState.Instance.MyHappiness);
		MyHealth.text = "My health: " + Mathf.CeilToInt(GameState.Instance.MyHealth);
		FamilyFood.text = "Family food: " + Mathf.CeilToInt(GameState.Instance.FamilyFood);
		FamilyHappiness.text = "Family happiness: " + Mathf.CeilToInt(GameState.Instance.FamilyHappiness);
		FamilyHealth.text = "Family health: " + Mathf.CeilToInt(GameState.Instance.FamilyHealth);
		Age.text = "Age\n\n" + Mathf.FloorToInt(GameState.Instance.Age);
	}
}
