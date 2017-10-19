using UnityEngine;
using GameOfLife.GameLogic;
using GameOfLife.GameLogic.GameStateAction;

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

	public void SetEffect(StateAction action)
	{
		m_Effect = CreateSummaryEffect(action);
		m_TimeToFinish = action.GetDurationInSeconds();
	}

	private StatsDifference CreateSummaryEffect(StateAction action)
	{
		StatsDifference result = new StatsDifference();

		var differences = new[] {action.GetEffectBefore(), action.GetEffectDuring(), action.GetEffectAfter()};
		
		foreach (StatsDifference difference in differences)
		{
            result.Plus(difference);
		}

		if (action.GetName() == PlayerActionType.GoToWork.ToString())
		{
			result.SetMoney(State.State.GetStateItemValue<int>(StateItemType.MySalary.ToString()));
		}
		
		if (action.GetName() == PlayerActionType.PartnerGoesToWork.ToString())
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
