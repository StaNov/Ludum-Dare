using System;

public abstract class StateItemGeneric<T> : StateItem
{
	private T _value;
	protected Func<StatsDifference, T> GetDifferenceValue;

	public StateItemGeneric(GameplayConstants constants, Func<StatsDifference, T> getDifferenceValue)
	{
		GetDifferenceValue = getDifferenceValue;
		Value = GetDifferenceValue(constants.InitialValues);
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

	protected virtual T OnSetValue(T originalValue, T newValue)
	{
		return newValue;
	}

	public abstract void ApplyDifference(StatsDifference difference, float multiplier = 1);

	public abstract bool DifferenceHasZeroEffect(StatsDifference difference);

	public virtual void ApplyDifferenceByTime(float deltaTime) { }

	public virtual bool IsGameOverBecauseOfThis() { return false; }
}
