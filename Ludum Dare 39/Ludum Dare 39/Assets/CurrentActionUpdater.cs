using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentActionUpdater : MonoBehaviour
{
	public GameState State;
	public Text ActionText;
	
	void Update ()
	{
		ActionText.text = State.CurrentPlayerAction == null ? "" : State.CurrentPlayerAction.Action.Type.ToString();
	}
}
