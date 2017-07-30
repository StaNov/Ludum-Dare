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

	public GameState State;

	private void Update ()
	{
		MyEnergy.text = "My energy: " + Mathf.CeilToInt(State.MyEnergy) + " / " + Mathf.CeilToInt(State.MyMaxEnergy);
		MyFood.text = "My food: " + Mathf.CeilToInt(State.MyFood);
		MyHappiness.text = "My happiness: " + Mathf.CeilToInt(State.MyHappiness);
		MyHealth.text = "My health: " + Mathf.CeilToInt(State.MyHealth);
		FamilyFood.text = "Family food: " + Mathf.CeilToInt(State.FamilyFood);
		FamilyHappiness.text = "Family happiness: " + Mathf.CeilToInt(State.FamilyHappiness);
		FamilyHealth.text = "Family health: " + Mathf.CeilToInt(State.FamilyHealth);
		Age.text = "Age\n\n" + Mathf.FloorToInt(State.Age);
	}
	
	// TODO private void Append
}
