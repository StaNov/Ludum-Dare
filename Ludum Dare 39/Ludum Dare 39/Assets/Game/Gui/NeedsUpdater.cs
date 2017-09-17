using GameOfLife.GameState;
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
		
		// TODO display values push down?
		MyMaxEnergy.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.MyMaxEnergy), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.MyMaxEnergy)), effect.MyMaxEnergy);
		MyEnergy.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.MyEnergy), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.MyEnergy)), effect.MyEnergy, State.State.GetStateItemValue<float>(StateItemType.MyMaxEnergy));
		MyFood.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.MyFood), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.MyFood)), effect.MyFood);
		MyHappiness.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.MyHappiness), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.MyHappiness)), effect.MyHappiness);
		MyHealth.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.MyHealth), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.MyHealth)), effect.MyHealth);
		FamilyFood.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.FamilyFood), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.FamilyFood)), effect.FamilyFood);
		FamilyHappiness.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.FamilyHappiness), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.FamilyHappiness)), effect.FamilyHappiness);
		FamilyHealth.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.FamilyHealth), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.FamilyHealth)), effect.FamilyHealth);
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
