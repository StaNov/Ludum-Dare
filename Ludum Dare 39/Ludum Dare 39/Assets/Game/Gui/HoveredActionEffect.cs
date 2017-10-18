using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using GameOfLife.GameLogic;

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
            result.Plus(difference);
		}

		if (action.Type == PlayerActionType.GoToWork)
		{
			result.SetMoney(State.State.GetStateItemValue<int>(StateItemType.MySalary.ToString()));
		}
		
		if (action.Type == PlayerActionType.PartnerGoesToWork)
		{
			result.SetMoney(State.State.GetStateItemValue<int>(StateItemType.PartnerSalary.ToString()));
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
