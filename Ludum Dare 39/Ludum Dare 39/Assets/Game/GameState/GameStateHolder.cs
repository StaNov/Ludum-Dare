using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

public class GameStateHolder : MonoBehaviour
{
	private GameState _state;

	public GameplayConstants Constants;

	// TODO return only the values we need to expose
	public GameState State { get { return _state; } }

	// TODO move to button logic
	public void RunAction()
	{
		string actionName = EventSystem.current.currentSelectedGameObject.name;
		_state.RunAction(actionName);
	}

	private void Start()
	{
		_state = new GameState(Constants);
	}

	private void FixedUpdate()
	{
		_state.ApplyTime(Time.deltaTime);
	}
}

public class GameState
{ 
	private Dictionary<StateItemType, StateItem> _items;
	private GameplayConstants _constants;

	public float GetStateItemValue(StateItemType type)
	{
		return _items[type].Value;
	}

	public float Age { get { return _items[StateItemType.Age].Value; } }
	public float MyEnergy { get { return _items[StateItemType.MyEnergy].Value; } }
    public float MyMaxEnergy { get { return _items[StateItemType.MyMaxEnergy].Value; } }
	public float MyFood { get { return _items[StateItemType.MyFood].Value; } }
	public float MyHappiness { get { return _items[StateItemType.MyHappiness].Value; } }
	public float MyHealth { get { return _items[StateItemType.MyHealth].Value; } }
	public float FamilyFood { get { return _items[StateItemType.FamilyFood].Value; } }
	public float FamilyHappiness { get { return _items[StateItemType.FamilyHappiness].Value; } }
	public float FamilyHealth { get { return _items[StateItemType.FamilyHealth].Value; } }
	public int Money { get; private set; }
	public int MoneyPerWorkshift { get; private set; }
	public int MoneyPerPartnersWorkshift { get; private set; }
	public int FoodSupplies { get; private set; }
	public bool IsFamilyActive { get; private set; }

	public CurrentAction CurrentPlayerAction { get; private set; }
	public CurrentAction CurrentPartnerAction { get; private set; }
	
	public StateItemType? GameOver
	{
		get
		{
			foreach (var item in _items)
				if (item.Value.IsGameOverBecauseOfThis())
					return item.Key;

			// TODO delete
			if (Money < 0)
				return StateItemType.Money;
			if (FoodSupplies < 0)
				return StateItemType.FoodSupplies;
			
			return null;
		}
	}

	public void StartFamily()
	{
		IsFamilyActive = true;
	}
	
	public GameState(GameplayConstants constants)
	{
		CurrentPlayerAction = null;
		CurrentPartnerAction = null;

		_constants = constants;
		_items = new Dictionary<StateItemType, StateItem>();

		_items.Add(StateItemType.Age, new StateItem(0, 99999, constants.InitialValues.Age, constants.ChangePerMinute.Age, (d) => true, () => true));
		_items.Add(StateItemType.MyMaxEnergy, new StateItem(0, 100, constants.InitialValues.MyMaxEnergy, constants.ChangePerMinute.MyMaxEnergy, (d) => d.MyMaxEnergy == 0, () => true));
		_items.Add(StateItemType.MyEnergy, new StateItem(0, () => MyMaxEnergy, constants.InitialValues.MyEnergy, constants.ChangePerMinute.MyEnergy, (d) => d.MyEnergy == 0, () => true));
		_items.Add(StateItemType.MyFood, new StateItem(0, 100, constants.InitialValues.MyFood, constants.ChangePerMinute.MyFood, (d) => d.MyFood == 0, () => true));
		_items.Add(StateItemType.MyHappiness, new StateItem(0, 100, constants.InitialValues.MyHappiness, constants.ChangePerMinute.MyHappiness, (d) => d.MyHappiness == 0, () => true));
		_items.Add(StateItemType.MyHealth, new StateItem(0, 100, constants.InitialValues.MyHealth, constants.ChangePerMinute.MyHealth, (d) => d.MyHealth == 0, () => true));
		_items.Add(StateItemType.FamilyFood, new StateItem(0, 100, constants.InitialValues.FamilyFood, constants.ChangePerMinute.FamilyFood, (d) => d.FamilyFood == 0, () => IsFamilyActive));
		_items.Add(StateItemType.FamilyHappiness, new StateItem(0, 100, constants.InitialValues.FamilyHappiness, constants.ChangePerMinute.FamilyHappiness, (d) => d.FamilyHappiness == 0, () => IsFamilyActive));
		_items.Add(StateItemType.FamilyHealth, new StateItem(0, 100, constants.InitialValues.FamilyHealth, constants.ChangePerMinute.FamilyHealth, (d) => d.FamilyHealth == 0, () => IsFamilyActive));
		Money = constants.InitialValues.Money;
		MoneyPerWorkshift = constants.InitialValues.MoneyPerWorkshift;
		MoneyPerPartnersWorkshift = constants.InitialValues.MoneyPerPartnersWorkshift;
	}

	public void ApplyTime(float deltaTime)
	{
		if (GameOver != null)
		{
			return;
		}

		CurrentPlayerAction = UpdateByAction(CurrentPlayerAction, deltaTime);
		CurrentPartnerAction = UpdateByAction(CurrentPartnerAction, deltaTime);

		UpdateByTime(deltaTime);
	}

	private void UpdateByTime(float deltaTime)
	{
		float deltaTimeInMinutes = deltaTime / 60;

		foreach (var item in _items)
			if ((CurrentPlayerAction == null || item.Value.DifferenceHasZeroEffect(CurrentPlayerAction.Action.EffectDuring))
				&& (CurrentPartnerAction == null || item.Value.DifferenceHasZeroEffect(CurrentPartnerAction.Action.EffectDuring)))
				item.Value.ApplyChange(item.Value.ChangePerMinute * deltaTimeInMinutes);
	}

	private CurrentAction UpdateByAction(CurrentAction action, float deltaTime)
	{
		float deltaTimeByDuration = action == null ? 0 : deltaTime / action.Action.DurationInSeconds;

		if (action != null && action.Action.Type != PlayerActionType.None)
		{
			action.RemainingTime -= Time.deltaTime;

			// TODO if remaining time is less than Time.deltaTime, apply to only the remaining part
			// ... but it happens in FixedUpdate, so it doesn't matter that much - Time.deltaTime is still the same 0.02
			if (action.RemainingTime < 0)
			{
				UpdateAfterAction(action.Action);
				// TODO action may be "set or unset" internally, not "null or non-null" by itself
				return null;
			}

			foreach (var item in _items)
			{
				item.Value.ApplyChange(action.Action.EffectDuring.GetStat(item.Key) * deltaTimeByDuration);
			}
		}

		return action;
	}

	public void RunAction(PlayerActionType type)
	{
		RunAction(type.ToString());
	}

	public void RunAction(string actionName)
	{
		PlayerAction action = _constants.GetPlayerAction(actionName);
		RunAction(action);
	}

	public void RunAction(PlayerAction action)
	{
		bool isPartnersAction = action.Type.IsPartnersAction();

		if (action.Type.IsForBoth() && CurrentPartnerAction != null && CurrentPartnerAction.Action.Type != PlayerActionType.None)
		{
			CurrentPartnerAction = null;
		}

		UpdateBeforeAction(action);

		var currentAction = new CurrentAction
		{
			Action = action,
			RemainingTime = action.DurationInSeconds
		};

		if (!isPartnersAction)
			CurrentPlayerAction = currentAction;
		else
			CurrentPartnerAction = currentAction;
	}

	private void UpdateBeforeAction(PlayerAction action)
	{
		UpdateStatsOneTime(action.EffectBefore);
	}

	private void UpdateAfterAction(PlayerAction action)
	{
		UpdateStatsOneTime(action.EffectAfter);
		
		switch (action.Type)
		{
			case PlayerActionType.GoToWork:
				Money += MoneyPerWorkshift;
				break;
			case PlayerActionType.PartnerGoesToWork:
				Money += MoneyPerPartnersWorkshift;
				break;
		}
	}
	
	private void UpdateStatsOneTime(StatsDifference difference)
	{
		foreach (var item in _items)
			item.Value.ApplyChange(difference.GetStat(item.Key));
		
		Money += difference.Money;
		FoodSupplies += difference.FoodSupplies;
		MoneyPerWorkshift += difference.MoneyPerWorkshift;
		MoneyPerPartnersWorkshift += difference.MoneyPerPartnersWorkshift;
	}

	[Serializable]
	public class CurrentAction
	{
		public PlayerAction Action;
		public float RemainingTime;
	}
}
