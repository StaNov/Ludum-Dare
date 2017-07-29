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

	private void Update ()
	{
		MyEnergy.text = "My energy: " + (int) GameState.Instance.MyEnergy + " / " + (int) GameState.Instance.MyMaxEnergy;
		MyFood.text = "My food: " + (int) GameState.Instance.MyFood;
		MyHappiness.text = "My happiness: " + (int) GameState.Instance.MyHappiness;
		MyHealth.text = "My health: " + (int) GameState.Instance.MyHealth;
		FamilyFood.text = "Family food: " + (int) GameState.Instance.FamilyFood;
		FamilyHappiness.text = "Family happiness: " + (int) GameState.Instance.FamilyHappiness;
		FamilyHealth.text = "Family health: " + (int) GameState.Instance.FamilyHealth;
	}
}
