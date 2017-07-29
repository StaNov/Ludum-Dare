using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentActionUpdater : MonoBehaviour
{
	public GameState State;
	public Text ActionText;
	public bool IsPartner;
	
	void Update ()
	{
		GameState.CurrentAction action = IsPartner ? State.CurrentPartnerAction : State.CurrentPlayerAction;
		
		ActionText.text = action == null ? "" : action.Action.Type.ToString();
	}
}
