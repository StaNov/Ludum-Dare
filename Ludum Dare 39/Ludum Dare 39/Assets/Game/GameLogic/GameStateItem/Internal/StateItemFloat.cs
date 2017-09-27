namespace GameOfLife.GameLogic.GameStateItem.Internal
{
	using System;
	using UnityEngine;

	public class StateItemFloat : StateItemGeneric<float>
	{
		private float _minValue = float.MinValue;
		private Func<float> _maxValue = () => float.MaxValue;
		private float _changePerMinute;

		public StateItemFloat(string name, float minValue, float maxValue, GameplayConstants constants, Func<StatsDifference, float> getDifferenceValue, bool updateIfFamilyNotActive) :
			this(name, minValue, () => maxValue, constants, getDifferenceValue, updateIfFamilyNotActive)
		{ }

		// TODO create builder
		// TODO redesign the arguments needed in constructor
		public StateItemFloat(string name, float minValue, Func<float> getMaxValue, GameplayConstants constants, Func<StatsDifference, float> getDifferenceValue, bool updateIfFamilyNotActive) :
			base(name, constants, getDifferenceValue, updateIfFamilyNotActive)
		{
			_minValue = minValue;
			_maxValue = getMaxValue;
			_changePerMinute = GetDifferenceValue(constants.ChangePerMinute);
			Value = Value; // to do the clamping
		}

		public override void ApplyDifferenceByTime(float deltaTime)
		{
			float deltaTimeInMinutes = deltaTime / 60;

			Value += _changePerMinute * deltaTimeInMinutes;
		}

		public override void ApplyDifferenceByAction(StatsDifference difference, PlayerAction action, float multiplier = 1)
		{
			Value += GetDifferenceValue(difference) * multiplier;
		}

		public override bool IsGameOverBecauseOfThis()
		{
			return Value <= 0;
		}

		public override bool DifferenceHasZeroEffect(StatsDifference difference)
		{
			return GetDifferenceValue(difference) == 0;
		}

		// TODO original value may be deleted i guess
		protected override float OnSetValue(float originalValue, float newValue)
		{
			return Mathf.Clamp(newValue, _minValue, _maxValue());
		}
	}
}
