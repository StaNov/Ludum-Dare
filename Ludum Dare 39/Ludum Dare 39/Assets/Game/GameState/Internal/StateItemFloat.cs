namespace GameOfLife.GameState.Internal
{
	using System;
	using UnityEngine;

	public class StateItemFloat : StateItemGeneric<float>
	{
		private float _minValue = float.MinValue;
		private Func<float> _maxValue = () => float.MaxValue;
		private Func<bool> _shouldBeUpdated = () => true; // initial values can be initialized
		private float _changePerMinute;

		public StateItemFloat(float minValue, float maxValue, GameplayConstants constants, Func<StatsDifference, float> getDifferenceValue, Func<bool> shouldBeUpdated) :
			this(minValue, () => maxValue, constants, getDifferenceValue, shouldBeUpdated)
		{ }

		// TODO create builder
		// TODO redesign the arguments needed in constructor
		public StateItemFloat(float minValue, Func<float> getMaxValue, GameplayConstants constants, Func<StatsDifference, float> getDifferenceValue, Func<bool> shouldBeUpdated) :
			base(constants, getDifferenceValue)
		{
			_minValue = minValue;
			_maxValue = getMaxValue;
			_changePerMinute = GetDifferenceValue(constants.ChangePerMinute);
			_shouldBeUpdated = shouldBeUpdated;
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

		protected override float OnSetValue(float originalValue, float newValue)
		{
			if (!_shouldBeUpdated())
				return originalValue;

			return Mathf.Clamp(newValue, _minValue, _maxValue());
		}
	}
}
