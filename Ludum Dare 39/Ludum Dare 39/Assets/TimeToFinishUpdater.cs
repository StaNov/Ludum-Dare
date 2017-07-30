using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeToFinishUpdater : MonoBehaviour
{
	public Text Text;
	public HoveredActionEffect Effect;
	
	void Update ()
	{
		Text.text = Effect.TimeToFinish > float.Epsilon ? Mathf.RoundToInt(Effect.TimeToFinish) + "s" : "";
	}
}
