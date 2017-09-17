using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using GameOfLife.GameState;

public class HoveredActionEffect : MonoBehaviour
{
	public GameStateHolder State;
	public GameplayConstants Constants;
	
	public StatsDifference Effect
	{
		get { return m_Effect; }
	}

	public int TimeToFinish
	{
		get { return m_TimeToFinish; }
	}

	public void SetEffect(PlayerAction action)
	{
		m_Effect = CreateSummaryEffect(action);
		m_TimeToFinish = action.DurationInSeconds;
	}

	private StatsDifference CreateSummaryEffect(PlayerAction action)
	{
		StatsDifference result = new StatsDifference();

		var differences = new[] {action.EffectBefore, action.EffectDuring, action.EffectAfter};
		
		foreach (StatsDifference difference in differences)
		{
			result.MyEnergy += difference.MyEnergy;
			result.MyMaxEnergy += difference.MyMaxEnergy;
			result.MyFood += difference.MyFood;
			result.MyHappiness += difference.MyHappiness;
			result.MyHealth += difference.MyHealth;
			result.FamilyFood += difference.FamilyFood;
			result.FamilyHappiness += difference.FamilyHappiness;
			result.FamilyHealth += difference.FamilyHealth;
			result.Money += difference.Money;
			result.FoodSupplies += difference.FoodSupplies;
			result.MoneyPerWorkshift += difference.MoneyPerWorkshift;
			result.MoneyPerPartnersWorkshift += difference.MoneyPerPartnersWorkshift;
		}

		if (action.Type == PlayerActionType.GoToWork)
		{
			result.Money = State.State.GetStateItemValue<int>(StateItemType.MySalary);
		}
		
		if (action.Type == PlayerActionType.PartnerGoesToWork)
		{
			result.Money = State.State.GetStateItemValue<int>(StateItemType.PartnerSalary);
		}

		return result;
	}

	private StatsDifference m_Effect = new StatsDifference();
	private int m_TimeToFinish;

	public void ResetDifference()
	{
		m_Effect = new StatsDifference();
		m_TimeToFinish = 0;
	}
}
