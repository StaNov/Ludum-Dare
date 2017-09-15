using System.Collections.Generic;
using UnityEngine;
using System;

public class GameState
{
	private Dictionary<StateItemType, StateItem> _items;
	private GameplayConstants _constants;

	public GameState(GameplayConstants constants)
	{
		CurrentPlayerAction = null;
		CurrentPartnerAction = null;

		_constants = constants;
		_items = new Dictionary<StateItemType, StateItem>();

		// TODO move to State Factory
		_items.Add(StateItemType.Age, new StateItemFloat(0, 99999, constants, (d) => d.Age, () => true));
		_items.Add(StateItemType.MyMaxEnergy, new StateItemFloat(0, 100, constants, (d) => d.MyMaxEnergy, () => true));
		_items.Add(StateItemType.MyEnergy, new StateItemFloat(0, () => GetStateItemValue(StateItemType.MyMaxEnergy), constants, (d) => d.MyEnergy, () => true));
		_items.Add(StateItemType.MyFood, new StateItemFloat(0, 100, constants, (d) => d.MyFood, () => true));
		_items.Add(StateItemType.MyHappiness, new StateItemFloat(0, 100, constants, (d) => d.MyHappiness, () => true));
		_items.Add(StateItemType.MyHealth, new StateItemFloat(0, 100, constants, (d) => d.MyHealth, () => true));
		_items.Add(StateItemType.FamilyFood, new StateItemFloat(0, 100, constants, (d) => d.FamilyFood, () => IsFamilyActive));
		_items.Add(StateItemType.FamilyHappiness, new StateItemFloat(0, 100, constants, (d) => d.FamilyHappiness, () => IsFamilyActive));
		_items.Add(StateItemType.FamilyHealth, new StateItemFloat(0, 100, constants, (d) => d.FamilyHealth, () => IsFamilyActive));
		_items.Add(StateItemType.Money, new StateItemMoney(constants, (d) => d.Money, () => GetStateItemValue<int>(StateItemType.MySalary), () => GetStateItemValue<int>(StateItemType.PartnerSalary)));
		_items.Add(StateItemType.MySalary, new StateItemInt(constants, (d) => d.MoneyPerWorkshift));
		_items.Add(StateItemType.PartnerSalary, new StateItemInt(constants, (d) => d.MoneyPerPartnersWorkshift));
		_items.Add(StateItemType.FoodSupplies, new StateItemInt(constants, (d) => d.FoodSupplies));
	}

	// TODO delete
	public float GetStateItemValue(StateItemType type)
	{
		return GetStateItemValue<float>(type);
	}

	public T GetStateItemValue<T>(StateItemType type)
	{
		return _items[type].GetValue<T>();
	}
	
	public int Money { get { return GetStateItemValue<int>(StateItemType.Money); } private set { ((StateItemInt)_items[StateItemType.Money]).Value = value; } }
	public int MoneyPerWorkshift { get { return GetStateItemValue<int>(StateItemType.MySalary); } private set { ((StateItemInt)_items[StateItemType.MySalary]).Value = value; } }
	public int MoneyPerPartnersWorkshift { get { return GetStateItemValue<int>(StateItemType.PartnerSalary); } private set { ((StateItemInt)_items[StateItemType.PartnerSalary]).Value = value; } }
	public int FoodSupplies { get { return GetStateItemValue<int>(StateItemType.FoodSupplies); } private set { ((StateItemInt)_items[StateItemType.FoodSupplies]).Value = value; } }
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

			// TODO delete when no game over when no money or food
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
		foreach (var item in _items)
			if ((CurrentPlayerAction == null || item.Value.DifferenceHasZeroEffect(CurrentPlayerAction.Action.EffectDuring))
				&& (CurrentPartnerAction == null || item.Value.DifferenceHasZeroEffect(CurrentPartnerAction.Action.EffectDuring)))
				item.Value.ApplyDifferenceByTime(deltaTime);
	}

	private CurrentAction UpdateByAction(CurrentAction action, float deltaTime)
	{
		if (action != null && action.Action.Type != PlayerActionType.None)
		{
			deltaTime = Mathf.Min(deltaTime, action.RemainingTime); // just the remaining time if smaller then deltaTime
			action.RemainingTime -= deltaTime;

			foreach (var item in _items)
			{
				float deltaTimeByDuration = action == null ? 0 : deltaTime / action.Action.DurationInSeconds;
				item.Value.ApplyDifferenceByAction(action.Action.EffectDuring, action.Action, deltaTimeByDuration);
			}

			if (action.RemainingTime <= 0)
			{
				UpdateAfterAction(action.Action);
				// TODO action may be "set or unset" internally, not "null or non-null" by itself
				return null;
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
		UpdateStatsOneTime(action.EffectBefore, action);
	}

	private void UpdateAfterAction(PlayerAction action)
	{
		UpdateStatsOneTime(action.EffectAfter, action);
	}

	private void UpdateStatsOneTime(StatsDifference difference, PlayerAction action)
	{
		foreach (var item in _items)
			item.Value.ApplyDifferenceByAction(difference, action);
	}

	[Serializable]
	public class CurrentAction
	{
		public PlayerAction Action;
		public float RemainingTime;
	}
}
