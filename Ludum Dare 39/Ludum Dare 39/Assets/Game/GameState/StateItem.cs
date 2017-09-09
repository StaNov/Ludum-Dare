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
	private Func<StatsDifference, float> _getDifferenceValue;
	private Func<bool> _shouldBeUpdated = () => true; // initial values can be initialized
	private float _changePerMinute;

	public StateItem(float minValue, float maxValue, GameplayConstants constants, Func<StatsDifference, float> getDifferenceValue, Func<bool> shouldBeUpdated) :
		this(minValue, () => maxValue, constants, getDifferenceValue, shouldBeUpdated)
	{ }

	// TODO create builder
	// TODO redesign the arguments needed in constructor
	// TODO pass gamestate to differenceHasZeroEffect or shouldBeUpdated?
	public StateItem(float minValue, Func<float> getMaxValue, GameplayConstants constants, Func<StatsDifference, float> getDifferenceValue, Func<bool> shouldBeUpdated)
	{
		_minValue = minValue;
		_maxValue = getMaxValue;
		_getDifferenceValue = getDifferenceValue;
		_changePerMinute = _getDifferenceValue(constants.ChangePerMinute);
		Value = _getDifferenceValue(constants.InitialValues);
		_shouldBeUpdated = shouldBeUpdated;
	}

	public float ChangePerMinute { get { return _changePerMinute; } }

	public void ApplyDifferenceByTime(float deltaTime)
	{
		float deltaTimeInMinutes = deltaTime / 60;

		Value += _changePerMinute * deltaTimeInMinutes;
	}

	public void ApplyDifference(StatsDifference difference, float multiplier = 1)
	{
		Value += _getDifferenceValue(difference) * multiplier;
	}

	public bool IsGameOverBecauseOfThis()
	{
		return _value <= 0;
	}

	public bool DifferenceHasZeroEffect(StatsDifference difference)
	{
		return _getDifferenceValue(difference) == 0;
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
