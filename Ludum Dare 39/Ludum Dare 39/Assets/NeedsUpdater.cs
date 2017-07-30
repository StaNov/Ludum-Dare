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
		
		MyEnergy.text = "My energy: " + Mathf.CeilToInt(State.MyEnergy) + CreateEffectSuffix(effect.MyEnergy) + " / " + Mathf.CeilToInt(State.MyMaxEnergy) + CreateEffectSuffix(effect.MyMaxEnergy);
		MyFood.text = "My food: " + Mathf.CeilToInt(State.MyFood) + CreateEffectSuffix(effect.MyFood);
		MyHappiness.text = "My happiness: " + Mathf.CeilToInt(State.MyHappiness) + CreateEffectSuffix(effect.MyHappiness);
		MyHealth.text = "My health: " + Mathf.CeilToInt(State.MyHealth) + CreateEffectSuffix(effect.MyHealth);
		FamilyFood.text = "Family food: " + Mathf.CeilToInt(State.FamilyFood) + CreateEffectSuffix(effect.FamilyFood);
		FamilyHappiness.text = "Family happiness: " + Mathf.CeilToInt(State.FamilyHappiness) + CreateEffectSuffix(effect.FamilyHappiness);
		FamilyHealth.text = "Family health: " + Mathf.CeilToInt(State.FamilyHealth) + CreateEffectSuffix(effect.FamilyHealth);
		Age.text = "Age\n\n" + Mathf.FloorToInt(State.Age);
	}

	private string CreateEffectSuffix(float /*TODO mozna to budou inty */ differenceValue)
	{
		if (differenceValue > float.Epsilon)
			return " <color=green><b>+" + differenceValue + "</b></color>";
		
		if (differenceValue < -float.Epsilon)
			return " <color=red><b>" + differenceValue + "</b></color>";

		return "";
	}
}
