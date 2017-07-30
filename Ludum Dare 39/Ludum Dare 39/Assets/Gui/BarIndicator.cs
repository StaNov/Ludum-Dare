using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarIndicator : StateIndicator
{
	public Text Value;
	public RectTransform GreenBar;
	
	public override void UpdateValue(float value, int displayValue, int effect)
	{
		Value.text = displayValue + EffectSuffixCreator.Create(effect);
		float fullWidth = GetComponentInParent<RectTransform>().sizeDelta.x;

		var temp = GreenBar.sizeDelta;
		temp.x = fullWidth * (value / 100.0f);
		GreenBar.sizeDelta = temp;
	}
}