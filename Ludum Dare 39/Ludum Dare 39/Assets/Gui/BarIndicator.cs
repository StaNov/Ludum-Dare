using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarIndicator : StateIndicator
{
	public Text Value;
	public RectTransform GreenBar;
	
	public override void UpdateValue(float value, int displayValue, int effect, float maxValue=100.0f)
	{
		Value.text = displayValue + EffectSuffixCreator.Create(effect);
		float fullWidth = GetComponentInParent<RectTransform>().sizeDelta.x;

		var temp = GreenBar.sizeDelta;
		temp.x = fullWidth * (value / maxValue);
		GreenBar.sizeDelta = temp;
	}
}