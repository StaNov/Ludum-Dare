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

        foreach (var indicator in new KeyValuePair<StateIndicator, StateItemType>[] {
            new KeyValuePair<StateIndicator, StateItemType> (MyEnergy, StateItemType.MyEnergy),
            new KeyValuePair<StateIndicator, StateItemType> (MyMaxEnergy, StateItemType.MyMaxEnergy),
            new KeyValuePair<StateIndicator, StateItemType> (MyFood, StateItemType.MyFood),
            new KeyValuePair<StateIndicator, StateItemType> (MyHappiness, StateItemType.MyHappiness),
            new KeyValuePair<StateIndicator, StateItemType> (MyHealth, StateItemType.MyHealth),
            new KeyValuePair<StateIndicator, StateItemType> (FamilyFood, StateItemType.FamilyFood),
            new KeyValuePair<StateIndicator, StateItemType> (FamilyHappiness, StateItemType.FamilyHappiness),
            new KeyValuePair<StateIndicator, StateItemType> (FamilyHealth, StateItemType.FamilyHealth)
        })
        {
            // TODO display values push down?
            indicator.Key.UpdateValue(
                State.State.GetStateItemValue<float>(indicator.Value.ToString()), 
                Mathf.CeilToInt(State.State.GetStateItemValue<float>(indicator.Value.ToString())), 
                effect.GetStat(indicator.Value.ToString()));

            // TODO special indicator for MyEnergy
            MyEnergy.UpdateValue(State.State.GetStateItemValue<float>(StateItemType.MyEnergy.ToString()), Mathf.CeilToInt(State.State.GetStateItemValue<float>(StateItemType.MyEnergy.ToString())), effect.GetStat(StateItemType.MyEnergy.ToString()), State.State.GetStateItemValue<float>(StateItemType.MyMaxEnergy.ToString()));
        }
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
