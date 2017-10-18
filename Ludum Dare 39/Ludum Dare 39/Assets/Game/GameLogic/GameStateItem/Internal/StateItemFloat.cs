namespace GameOfLife.GameLogic.GameStateItem.Internal
{
	using System;
	using UnityEngine;

	public class StateItemFloat : StateItemGeneric<float>
	{
		private float _minValue = float.MinValue;
		private Func<float> _maxValue = () => float.MaxValue;
		private float _changePerMinute;

		public StateItemFloat(string name, float minValue, float maxValue, float initialValue, float changePerMinute, bool updateIfFamilyNotActive) :
			this(name, minValue, () => maxValue, initialValue, changePerMinute, updateIfFamilyNotActive)
		{ }

		// TODO create builder
		// TODO redesign the arguments needed in constructor
		public StateItemFloat(string name, float minValue, Func<float> getMaxValue, float initialValue, float changePerMinute, bool updateIfFamilyNotActive) :
			base(name, initialValue, updateIfFamilyNotActive)
		{
			_minValue = minValue;
			_maxValue = getMaxValue;
			_changePerMinute = changePerMinute;
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

        protected override float GetDifferenceValue(StatsDifference difference)
        {
            return difference.GetStat(GetName());
        }

        // TODO original value may be deleted i guess
        protected override float OnSetValue(float originalValue, float newValue)
		{
			return Mathf.Clamp(newValue, _minValue, _maxValue());
		}
    }
}
