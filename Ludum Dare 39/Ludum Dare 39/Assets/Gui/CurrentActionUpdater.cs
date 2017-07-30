using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentActionUpdater : MonoBehaviour
{
	public GameState State;
	public Text ActionText;
	public bool IsPartner;
	public BarIndicator Indicator;
	
	void Update ()
	{
		GameState.CurrentAction action = IsPartner ? State.CurrentPartnerAction : State.CurrentPlayerAction;

		if (IsPartner && State.CurrentPlayerAction != null &&
		    (State.CurrentPlayerAction.Action.Type == PlayerActionType.GoToAquaWorld
		     || State.CurrentPlayerAction.Action.Type == PlayerActionType.SpendTimeWithFamily))
		{
			action = State.CurrentPlayerAction;
		}
		
		ActionText.text = action == null ? "" : action.Action.Type.ToString();
		
		Indicator.UpdateValue(action == null || action.Action.Type == PlayerActionType.None ? 0 : 1 - action.RemainingTime / action.Action.DurationInSeconds, 0, 0, 1);
	}
}
