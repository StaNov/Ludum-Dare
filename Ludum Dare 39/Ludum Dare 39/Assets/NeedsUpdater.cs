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
	public HoveredActionEffect Effect;

	private void Update ()
	{
		StatsDifference effect = Effect.Effect;
		
		MyEnergy.text = "My energy: " + Mathf.CeilToInt(State.MyEnergy) + EffectSuffixCreator.Create(effect.MyEnergy) + " / " + Mathf.CeilToInt(State.MyMaxEnergy) + EffectSuffixCreator.Create(effect.MyMaxEnergy);
		MyFood.text = "My food: " + Mathf.CeilToInt(State.MyFood) + EffectSuffixCreator.Create(effect.MyFood);
		MyHappiness.text = "My happiness: " + Mathf.CeilToInt(State.MyHappiness) + EffectSuffixCreator.Create(effect.MyHappiness);
		MyHealth.text = "My health: " + Mathf.CeilToInt(State.MyHealth) + EffectSuffixCreator.Create(effect.MyHealth);
		FamilyFood.text = "Family food: " + Mathf.CeilToInt(State.FamilyFood) + EffectSuffixCreator.Create(effect.FamilyFood);
		FamilyHappiness.text = "Family happiness: " + Mathf.CeilToInt(State.FamilyHappiness) + EffectSuffixCreator.Create(effect.FamilyHappiness);
		FamilyHealth.text = "Family health: " + Mathf.CeilToInt(State.FamilyHealth) + EffectSuffixCreator.Create(effect.FamilyHealth);
		Age.text = "Age\n\n" + Mathf.FloorToInt(State.Age);
	}
}

public static class EffectSuffixCreator
{
	public static string Create(int differenceValue)
	{
		int difference = Mathf.RoundToInt(differenceValue);
		
		if (difference > 0)
			return " <color=green><b>+" + difference + "</b></color>";
		if (difference < 0)
			return " <color=red><b>" + difference + "</b></color>";

		return "";
	}
}