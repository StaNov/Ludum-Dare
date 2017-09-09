using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StateItemType
{
	Age,
	MyEnergy,
	MyMaxEnergy,
	MyFood,
	MyHealth,
	MyHappiness,
	FamilyFood,
	FamilyHealth,
	FamilyHappiness,
	Money,
	FoodSupplies,
	MySalary,
	PartnerSalary
}

public class StateItem
{
	private float _value;
	private float _minValue;
	private Func<float> _maxValue;
	private Func<StatsDifference, bool> _differenceHasZeroEffect;
	private Func<bool> _shouldBeUpdated = () => true; // initial values can be initialized
	private float _changePerMinute;

	// TODO create builder
	public StateItem(float minValue, Func<float> getMaxValue, float initialValue, float changePerMinute, Func<StatsDifference, bool> differenceHasZeroEffect, Func<bool> shouldBeUpdated)
	{
		_minValue = minValue;
		_maxValue = getMaxValue;
		_changePerMinute = changePerMinute;
		_differenceHasZeroEffect = differenceHasZeroEffect;
		Value = initialValue;
		_shouldBeUpdated = shouldBeUpdated;
	}

	public StateItem(float minValue, float maxValue, float initialValue, float changePerMinute, Func<StatsDifference, bool> differenceHasZeroEffect, Func<bool> shouldBeUpdated)
	{
		_minValue = minValue;
		_maxValue = () => maxValue;
		_changePerMinute = changePerMinute;
		_differenceHasZeroEffect = differenceHasZeroEffect;
		Value = initialValue;
		_shouldBeUpdated = shouldBeUpdated;
	}

	public float ChangePerMinute { get { return _changePerMinute; } }

	public void ApplyChange(float changeValue)
	{
		Value += changeValue;
	}

	public bool IsGameOverBecauseOfThis()
	{
		return _value <= 0;
	}

	public bool DifferenceHasZeroEffect(StatsDifference difference)
	{
		return _differenceHasZeroEffect(difference);
	}

	public float Value
	{
		get
		{
			return _value;
		}
		private set
		{
			if (_shouldBeUpdated())
			{
				_value = Mathf.Clamp(value, _minValue, _maxValue());
			}
		}
	}
}
