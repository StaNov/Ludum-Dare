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
		MyEnergy.text = "My energy: " + GameState.Instance.MyEnergy + " / " + GameState.Instance.MyMaxEnergy;
		MyFood.text = "My food: " + GameState.Instance.MyFood;
		MyHappiness.text = "My happiness: " + GameState.Instance.MyHappiness;
		MyHealth.text = "My health: " + GameState.Instance.MyHealth;
		FamilyFood.text = "Family food: " + GameState.Instance.FamilyFood;
		FamilyHappiness.text = "Family happiness: " + GameState.Instance.FamilyHappiness;
		FamilyHealth.text = "Family health: " + GameState.Instance.FamilyHealth;
	}
}
