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

	public GameStateHolder State;
	public HoveredActionEffect Effect;

	private void Update ()
	{
		StatsDifference effect = Effect.Effect;
		
		MyMaxEnergy.UpdateValue(State.State.MyMaxEnergy, Mathf.CeilToInt(State.State.MyMaxEnergy), effect.MyMaxEnergy);
		MyEnergy.UpdateValue(State.State.MyEnergy, Mathf.CeilToInt(State.State.MyEnergy), effect.MyEnergy, State.State.MyMaxEnergy);
		MyFood.UpdateValue(State.State.MyFood, Mathf.CeilToInt(State.State.MyFood), effect.MyFood);
		MyHappiness.UpdateValue(State.State.MyHappiness, Mathf.CeilToInt(State.State.MyHappiness), effect.MyHappiness);
		MyHealth.UpdateValue(State.State.MyHealth, Mathf.CeilToInt(State.State.MyHealth), effect.MyHealth);
		FamilyFood.UpdateValue(State.State.FamilyFood, Mathf.CeilToInt(State.State.FamilyFood), effect.FamilyFood);
		FamilyHappiness.UpdateValue(State.State.FamilyHappiness, Mathf.CeilToInt(State.State.FamilyHappiness), effect.FamilyHappiness);
		FamilyHealth.UpdateValue(State.State.FamilyHealth, Mathf.CeilToInt(State.State.FamilyHealth), effect.FamilyHealth);
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
				return " <color=maroon>" + difference + "</color>";
			#else
				return " <color=maroon><b>" + difference + "</b></color>";
			#endif

		return "";
	}
}
