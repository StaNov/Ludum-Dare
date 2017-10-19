namespace GameOfLife.GameLogic.GameStateItem.Internal
{
    using GameStateAction;
    using System;

	public abstract class StateItemGeneric<T> : StateItem
	{
        private string _name;
		private T _value;
		private bool _updateIfFamilyNotActive;

		public StateItemGeneric(string name, T initialValue, bool updateIfFamilyNotActive)
		{
            _name = name;
			Value = initialValue;
			_updateIfFamilyNotActive = updateIfFamilyNotActive;
		}

		// TODO make private maybe?
		protected T Value
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

        public string GetName()
        {
            return _name;
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

		public abstract void ApplyDifferenceByAction(StatsDifference difference, StateAction action, float multiplier = 1);

		public abstract bool DifferenceHasZeroEffect(StatsDifference difference);

        protected abstract T GetDifferenceValue(StatsDifference difference);

        public virtual void ApplyDifferenceByTime(float deltaTime) { }

		public virtual bool IsGameOverBecauseOfThis() { return false; }
	}
}
