using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedsUpdater : MonoBehaviour
{

	public StateIndicator MyEnergy;
	public StateIndicator MyMaxEnergy;
	public StateIndicator MyFood;
	public StateIndicator MyHappiness;
	public StateIndicator MyHealth;
	public StateIndicator FamilyFood;
	public StateIndicator FamilyHappiness;
	public StateIndicator FamilyHealth;

	public GameState State;
	public HoveredActionEffect Effect;

	private void Update ()
	{
		StatsDifference effect = Effect.Effect;
		
		MyMaxEnergy.UpdateValue(State.MyMaxEnergy, Mathf.CeilToInt(State.MyMaxEnergy), effect.MyMaxEnergy);
		MyEnergy.UpdateValue(State.MyEnergy, Mathf.CeilToInt(State.MyEnergy), effect.MyEnergy, State.MyMaxEnergy);
		MyFood.UpdateValue(State.MyFood, Mathf.CeilToInt(State.MyFood), effect.MyFood);
		MyHappiness.UpdateValue(State.MyHappiness, Mathf.CeilToInt(State.MyHappiness), effect.MyHappiness);
		MyHealth.UpdateValue(State.MyHealth, Mathf.CeilToInt(State.MyHealth), effect.MyHealth);
		FamilyFood.UpdateValue(State.FamilyFood, Mathf.CeilToInt(State.FamilyFood), effect.FamilyFood);
		FamilyHappiness.UpdateValue(State.FamilyHappiness, Mathf.CeilToInt(State.FamilyHappiness), effect.FamilyHappiness);
		FamilyHealth.UpdateValue(State.FamilyHealth, Mathf.CeilToInt(State.FamilyHealth), effect.FamilyHealth);
	}
}

public abstract class StateIndicator : MonoBehaviour
{
	public abstract void UpdateValue(float value, int displayValue, int effect, float maxValue=100.0f);
}

public static class EffectSuffixCreator
{
	public static string Create(int differenceValue)
	{
		int difference = Mathf.RoundToInt(differenceValue);
		
		if (difference > 0)
			#if UNITY_WEBGL && !UNITY_EDITOR
				return " <color=green>+" + difference + "</color>";
			#else
				return " <color=green><b>+" + difference + "</b></color>";
			#endif
		if (difference < 0)
			#if UNITY_WEBGL && !UNITY_EDITOR
				return " <color=red>+" + difference + "</color>";
			#else
				return " <color=red><b>" + difference + "</b></color>";
			#endif

		return "";
	}
}