namespace GameOfLife.GameState.Internal
{
	using System;

	public abstract class StateItemGeneric<T> : StateItem
	{
		private T _value;
		private bool _updateIfFamilyNotActive;
		protected Func<StatsDifference, T> GetDifferenceValue;

		public StateItemGeneric(GameplayConstants constants, Func<StatsDifference, T> getDifferenceValue, bool updateIfFamilyNotActive)
		{
			GetDifferenceValue = getDifferenceValue;
			Value = GetDifferenceValue(constants.InitialValues);
			_updateIfFamilyNotActive = updateIfFamilyNotActive;
		}

		// TODO make protected for sure, private maybe?
		public T Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = OnSetValue(_value, value);
			}
		}

		public V GetValue<V>()
		{
			if (typeof(V) != typeof(T))
			{
				throw new Exception("You are trying to get value of type " + typeof(V) + ", but this StateItem is of type " + typeof(T) + ".");
			}

			return (V)Convert.ChangeType(Value, typeof(V));
		}

		public bool UpdateIfFamilyNotActive()
		{
			return _updateIfFamilyNotActive;
		}

		protected virtual T OnSetValue(T originalValue, T newValue)
		{
			return newValue;
		}

		public abstract void ApplyDifferenceByAction(StatsDifference difference, PlayerAction action, float multiplier = 1);

		public abstract bool DifferenceHasZeroEffect(StatsDifference difference);

		public virtual void ApplyDifferenceByTime(float deltaTime) { }

		public virtual bool IsGameOverBecauseOfThis() { return false; }
	}
}
