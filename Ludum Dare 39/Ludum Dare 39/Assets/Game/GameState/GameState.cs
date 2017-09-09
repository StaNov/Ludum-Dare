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
	private Func<bool> _shouldBeUpdated;
	private float _changePerMinute;

	// TODO create builder
	public StateItem(float minValue, Func<float> getMaxValue, float initialValue, float changePerMinute, Func<StatsDifference, bool> differenceHasZeroEffect, Func<bool> shouldBeUpdated)
	{
		_shouldBeUpdated = () => true; // initial values can be initialized
		_minValue = minValue;
		_maxValue = getMaxValue;
		_changePerMinute = changePerMinute;
		_differenceHasZeroEffect = differenceHasZeroEffect;
		Value = initialValue;
		_shouldBeUpdated = shouldBeUpdated;
	}

	public StateItem(float minValue, float maxValue, float initialValue, float changePerMinute, Func<StatsDifference, bool> differenceHasZeroEffect, Func<bool> shouldBeUpdated)
	{
		_shouldBeUpdated = () => true; // initial values can be initialized
		_minValue = minValue;
		_maxValue = () => maxValue;
		_changePerMinute = changePerMinute;
		_differenceHasZeroEffect = differenceHasZeroEffect;
		Value = initialValue;
		_shouldBeUpdated = shouldBeUpdated;
	}

	public float ChangePerMinute { get { return _changePerMinute; } }

	public bool IsGameOverBecauseOfThis()
	{
		return _value <= 0;
	}

	public bool DifferenceHasZeroEffect(StatsDifference difference)
	{
		return _differenceHasZeroEffect(difference);
	}

	// TODO redesign so set may be private
	public float Value
	{
		get
		{
			return _value;
		}
		set
		{
			if (_shouldBeUpdated())
			{
				_value = Mathf.Clamp(value, _minValue, _maxValue());
			}
		}
	}
}

public class GameState : MonoBehaviour
{
	private Dictionary<StateItemType, StateItem> _items;

	public float GetStateItemValue(StateItemType type)
	{
		return _items[type].Value;
	}

	public float Age { get { return _items[StateItemType.Age].Value; } private set { _items[StateItemType.Age].Value = value; } }
	public float MyEnergy { get { return _items[StateItemType.MyEnergy].Value; } private set { _items[StateItemType.MyEnergy].Value = value; } }
    public float MyMaxEnergy { get { return _items[StateItemType.MyMaxEnergy].Value; } private set { _items[StateItemType.MyMaxEnergy].Value = value; } }
	public float MyFood { get { return _items[StateItemType.MyFood].Value; } private set { _items[StateItemType.MyFood].Value = value; } }
	public float MyHappiness { get { return _items[StateItemType.MyHappiness].Value; } private set { _items[StateItemType.MyHappiness].Value = value; } }
	public float MyHealth { get { return _items[StateItemType.MyHealth].Value; } private set { _items[StateItemType.MyHealth].Value = value; } }
	public float FamilyFood { get { return _items[StateItemType.FamilyFood].Value; } private set { _items[StateItemType.FamilyFood].Value = value; } }
	public float FamilyHappiness { get { return _items[StateItemType.FamilyHappiness].Value; } private set { _items[StateItemType.FamilyHappiness].Value = value; } }
	public float FamilyHealth { get { return _items[StateItemType.FamilyHealth].Value; } private set { _items[StateItemType.FamilyHealth].Value = value; } }
	public int Money { get; private set; }
	public int MoneyPerWorkshift { get; private set; }
	public int MoneyPerPartnersWorkshift { get; private set; }
	public int FoodSupplies { get; private set; }
	public bool IsFamilyActive { get; private set; }

	public CurrentAction CurrentPlayerAction { get; private set; }
	public CurrentAction CurrentPartnerAction { get; private set; }

	public GameplayConstants Constants;
	
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
	
	private static float DeltaTimeInMinutes { get { return Time.deltaTime / 60; }}
	private float DeltaTimeByDurationPlayer { get { return CurrentPlayerAction == null ? 0 : Time.deltaTime / CurrentPlayerAction.Action.DurationInSeconds; }}
	private float DeltaTimeByDurationPartner { get { return CurrentPartnerAction == null ? 0 : Time.deltaTime / CurrentPartnerAction.Action.DurationInSeconds; }}

	public void StartFamily()
	{
		IsFamilyActive = true;
	}

	private void Start ()
	{
		CurrentPlayerAction = null;
		CurrentPartnerAction = null;

		_items = new Dictionary<StateItemType, StateItem>();

		_items.Add(StateItemType.Age, new StateItem(0, 99999, Constants.InitialValues.Age, Constants.ChangePerMinute.Age, (d) => true, () => true));
		_items.Add(StateItemType.MyMaxEnergy, new StateItem(0, 100, Constants.InitialValues.MyMaxEnergy, Constants.ChangePerMinute.MyMaxEnergy, (d) => d.MyMaxEnergy == 0, () => true));
		_items.Add(StateItemType.MyEnergy, new StateItem(0, () => MyMaxEnergy, Constants.InitialValues.MyEnergy, Constants.ChangePerMinute.MyEnergy, (d) => d.MyEnergy == 0, () => true));
		_items.Add(StateItemType.MyFood, new StateItem(0, 100, Constants.InitialValues.MyFood, Constants.ChangePerMinute.MyFood, (d) => d.MyFood == 0, () => true));
		_items.Add(StateItemType.MyHappiness, new StateItem(0, 100, Constants.InitialValues.MyHappiness, Constants.ChangePerMinute.MyHappiness, (d) => d.MyHappiness == 0, () => true));
		_items.Add(StateItemType.MyHealth, new StateItem(0, 100, Constants.InitialValues.MyHealth, Constants.ChangePerMinute.MyHealth, (d) => d.MyHealth == 0, () => true));
		_items.Add(StateItemType.FamilyFood, new StateItem(0, 100, Constants.InitialValues.FamilyFood, Constants.ChangePerMinute.FamilyFood, (d) => d.FamilyFood == 0, () => IsFamilyActive));
		_items.Add(StateItemType.FamilyHappiness, new StateItem(0, 100, Constants.InitialValues.FamilyHappiness, Constants.ChangePerMinute.FamilyHappiness, (d) => d.FamilyHappiness == 0, () => IsFamilyActive));
		_items.Add(StateItemType.FamilyHealth, new StateItem(0, 100, Constants.InitialValues.FamilyHealth, Constants.ChangePerMinute.FamilyHealth, (d) => d.FamilyHealth == 0, () => IsFamilyActive));
		Money = Constants.InitialValues.Money;
		MoneyPerWorkshift = Constants.InitialValues.MoneyPerWorkshift;
		MoneyPerPartnersWorkshift = Constants.InitialValues.MoneyPerPartnersWorkshift;
	}
	
	private void FixedUpdate ()
	{
		if (GameOver != null)
		{
			return;
		}
		
		CurrentPlayerAction = UpdateByAction(CurrentPlayerAction, DeltaTimeByDurationPlayer);
		CurrentPartnerAction = UpdateByAction(CurrentPartnerAction, DeltaTimeByDurationPartner);

		UpdateByTime();
	}

	private void UpdateByTime()
	{
		foreach (var item in _items)
			if ((CurrentPlayerAction == null || item.Value.DifferenceHasZeroEffect(CurrentPlayerAction.Action.EffectDuring))
				&& (CurrentPartnerAction == null || item.Value.DifferenceHasZeroEffect(CurrentPartnerAction.Action.EffectDuring)))
				item.Value.Value += item.Value.ChangePerMinute * DeltaTimeInMinutes;
	}

	private CurrentAction UpdateByAction(CurrentAction action, float deltaTimeByDuration)
	{
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
				item.Value.Value += action.Action.EffectDuring.GetStat(item.Key) * deltaTimeByDuration;
			}
		}

		return action;
	}

	// TODO move to button logic
	public void RunAction()
	{
		string actionName = EventSystem.current.currentSelectedGameObject.name;
		RunAction(actionName);
	}

	public void RunAction(PlayerActionType type)
	{
		RunAction(type.ToString());
	}

	public void RunAction(string actionName)
	{
		PlayerAction action = Constants.GetPlayerAction(actionName);
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
			item.Value.Value += difference.GetStat(item.Key);
		
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
