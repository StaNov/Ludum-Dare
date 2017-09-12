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

		_items.Add(StateItemType.Age, new StateItemFloat(0, 99999, constants, (d) => d.Age, () => true));
		_items.Add(StateItemType.MyMaxEnergy, new StateItemFloat(0, 100, constants, (d) => d.MyMaxEnergy, () => true));
		_items.Add(StateItemType.MyEnergy, new StateItemFloat(0, () => GetStateItemValue(StateItemType.MyMaxEnergy), constants, (d) => d.MyEnergy, () => true));
		_items.Add(StateItemType.MyFood, new StateItemFloat(0, 100, constants, (d) => d.MyFood, () => true));
		_items.Add(StateItemType.MyHappiness, new StateItemFloat(0, 100, constants, (d) => d.MyHappiness, () => true));
		_items.Add(StateItemType.MyHealth, new StateItemFloat(0, 100, constants, (d) => d.MyHealth, () => true));
		_items.Add(StateItemType.FamilyFood, new StateItemFloat(0, 100, constants, (d) => d.FamilyFood, () => IsFamilyActive));
		_items.Add(StateItemType.FamilyHappiness, new StateItemFloat(0, 100, constants, (d) => d.FamilyHappiness, () => IsFamilyActive));
		_items.Add(StateItemType.FamilyHealth, new StateItemFloat(0, 100, constants, (d) => d.FamilyHealth, () => IsFamilyActive));
		Money = constants.InitialValues.Money;
		MoneyPerWorkshift = constants.InitialValues.MoneyPerWorkshift;
		MoneyPerPartnersWorkshift = constants.InitialValues.MoneyPerPartnersWorkshift;
	}

	// TODO public T GetStateItemValue<T>(StateItemType type)
	public float GetStateItemValue(StateItemType type)
	{
		return _items[type].GetValue<float>();
	}
	
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
				item.Value.ApplyDifference(action.Action.EffectDuring, deltaTimeByDuration);
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
			item.Value.ApplyDifference(difference);

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
