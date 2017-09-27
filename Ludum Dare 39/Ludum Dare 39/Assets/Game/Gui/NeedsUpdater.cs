using GameOfLife.GameLogic;
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
		MyMaxEnergy.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.MyMaxEnergy.ToString()), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.MyMaxEnergy.ToString())), effect.MyMaxEnergy);
		MyEnergy.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.MyEnergy.ToString()), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.MyEnergy.ToString())), effect.MyEnergy, State.State.GetStateItemValue<float>(StateItemType.MyMaxEnergy.ToString()));
		MyFood.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.MyFood.ToString()), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.MyFood.ToString())), effect.MyFood);
		MyHappiness.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.MyHappiness.ToString()), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.MyHappiness.ToString())), effect.MyHappiness);
		MyHealth.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.MyHealth.ToString()), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.MyHealth.ToString())), effect.MyHealth);
		FamilyFood.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.FamilyFood.ToString()), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.FamilyFood.ToString())), effect.FamilyFood);
		FamilyHappiness.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.FamilyHappiness.ToString()), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.FamilyHappiness.ToString())), effect.FamilyHappiness);
		FamilyHealth.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.FamilyHealth.ToString()), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.FamilyHealth.ToString())), effect.FamilyHealth);
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
