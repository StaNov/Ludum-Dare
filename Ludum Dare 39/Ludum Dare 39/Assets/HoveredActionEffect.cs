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

	public float /*TODO int*/ TimeToFinish
	{
		get { return m_TimeToFinish; }
	}

	public void SetEffect(PlayerAction action)
	{
		m_Effect = action.Effect.Clone() as StatsDifference;
		m_TimeToFinish = action.DurationInSeconds;

		if (action.Type == PlayerActionType.GoToWork)
		{
			m_Effect.Money = State.MoneyPerWorkshift;
			m_Effect.MoneyPerWorkshift = State.MoneyPerWorkshift * (Constants.MoneyPerShiftIncreaseCoefficient - 1);
		}
		
		if (action.Type == PlayerActionType.PartnerGoesToWork)
		{
			m_Effect.Money = State.MoneyPerPartnersWorkshift;
			m_Effect.MoneyPerPartnersWorkshift = State.MoneyPerPartnersWorkshift * (Constants.MoneyPerShiftIncreaseCoefficient - 1);
		}
		
		if (action.Type == PlayerActionType.LearnNewStuffForWork)
		{
			m_Effect.MoneyPerWorkshift = State.MoneyPerWorkshift * (Constants.MoneyPerShiftIncreaseByLearningCoefficient - 1);
		}
		
		if (action.Type == PlayerActionType.LearnNewStuffForWorkPartner)
		{
			m_Effect.MoneyPerPartnersWorkshift = State.MoneyPerPartnersWorkshift * (Constants.MoneyPerShiftIncreaseByLearningCoefficient - 1);
		}
	}

	private StatsDifference m_Effect = new StatsDifference();
	private float m_TimeToFinish;

	public void ResetDifference()
	{
		m_Effect = new StatsDifference();
		m_TimeToFinish = 0;
	}
}
