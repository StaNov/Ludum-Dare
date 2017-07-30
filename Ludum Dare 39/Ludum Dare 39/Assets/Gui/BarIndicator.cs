using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarIndicator : StateIndicator
{
	public Text text;
	
	public override void UpdateValue(int value, int effect)
	{
		text.text = value + EffectSuffixCreator.Create(effect);
	}
}