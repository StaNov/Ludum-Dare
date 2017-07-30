using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class HoveredActionEffect : MonoBehaviour
{
	public GameState State;
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
		}

		if (action.Type == PlayerActionType.GoToWork)
		{
			result.Money = State.MoneyPerWorkshift;
			result.MoneyPerWorkshift = Mathf.RoundToInt(State.MoneyPerWorkshift * (Constants.MoneyPerShiftIncreaseCoefficient - 1));
		}
		
		if (action.Type == PlayerActionType.PartnerGoesToWork)
		{
			result.Money = State.MoneyPerPartnersWorkshift;
			result.MoneyPerPartnersWorkshift = Mathf.RoundToInt(State.MoneyPerPartnersWorkshift * (Constants.MoneyPerShiftIncreaseCoefficient - 1));
		}
		
		if (action.Type == PlayerActionType.LearnNewStuffForWork)
		{
			result.MoneyPerWorkshift = Mathf.RoundToInt(State.MoneyPerWorkshift * (Constants.MoneyPerShiftIncreaseByLearningCoefficient - 1));
		}
		
		if (action.Type == PlayerActionType.LearnNewStuffForWorkPartner)
		{
			result.MoneyPerPartnersWorkshift = Mathf.RoundToInt(State.MoneyPerPartnersWorkshift * (Constants.MoneyPerShiftIncreaseByLearningCoefficient - 1));
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
