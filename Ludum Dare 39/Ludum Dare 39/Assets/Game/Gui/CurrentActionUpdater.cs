using GameOfLife.GameLogic;
using GameOfLife.GameLogic.GameState;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentActionUpdater : MonoBehaviour
{
	public GameStateHolder State;
	public Text ActionText;
	public bool IsPartner;
	public BarIndicator Indicator;
	public AudioSource Source;

	private bool lastFrameActionPerforming;
	
	void Update ()
	{
		KeyValuePair<string, float>? action = IsPartner ? State.State.GetCurrentPartnerAction() : State.State.GetCurrentPlayerAction();

        // TODO move logic to GameState
		if (IsPartner && State.State.GetCurrentPlayerAction() != null &&
		    (State.State.GetCurrentPlayerAction().Value.Key == PlayerActionType.GoToAquaWorld.ToString()
		     || State.State.GetCurrentPlayerAction().Value.Key == PlayerActionType.SpendTimeWithFamily.ToString()))
		{
			action = State.State.GetCurrentPlayerAction();
		}
		
		ActionText.text = action == null ? "" : ((PlayerActionType) Enum.Parse(typeof(PlayerActionType), action.Value.Key)).Localized();
		
		Indicator.UpdateValue(action == null || action.Value.Key == PlayerActionType.None.ToString() ? 0 : action.Value.Value, 0, 0, 1);

		if (lastFrameActionPerforming && action == null)
		{
			Source.Play();
		}

		lastFrameActionPerforming = action != null;
	}
}
