using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum StateItemType
{
	MyEnergy, MyMaxEnergy, MyFood
}

public class StateItem
{
	private float _value;
	private float _minValue;
	private Func<float> _maxValue;
	private Func<StatsDifference, bool> _differenceHasZeroEffect;
	private float _changePerMinute;

	// TODO create builder
	public StateItem(float minValue, Func<float> getMaxValue, float initialValue, float changePerMinute, Func<StatsDifference, bool> differenceHasZeroEffect)
	{
		_minValue = minValue;
		_maxValue = getMaxValue;
		_changePerMinute = changePerMinute;
		_differenceHasZeroEffect = differenceHasZeroEffect;
		Value = initialValue;
	}

	public StateItem(float minValue, float maxValue, float initialValue, float changePerMinute, Func<StatsDifference, bool> differenceHasZeroEffect)
	{
		_minValue = minValue;
		_maxValue = () => maxValue;
		_changePerMinute = changePerMinute;
		_differenceHasZeroEffect = differenceHasZeroEffect;
		Value = initialValue;
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
	public float Value { get { return _value; } set { _value = Mathf.Clamp(value, _minValue, _maxValue()); } }
}

public class GameState : MonoBehaviour
{
	private Dictionary<StateItemType, StateItem> _items;

	public float GetStateItemValue(StateItemType type)
	{
		return _items[type].Value;
	}

    public float MyEnergy { get { return _items[StateItemType.MyEnergy].Value; } private set { _items[StateItemType.MyEnergy].Value = value; } }
    public float MyMaxEnergy { get { return _items[StateItemType.MyMaxEnergy].Value; } private set { _items[StateItemType.MyMaxEnergy].Value = value; } }
	public float MyFood { get { return _items[StateItemType.MyFood].Value; } private set { _items[StateItemType.MyFood].Value = value; } }
	public float MyHappiness { get; private set; }
	public float MyHealth { get; private set; }
	public float FamilyFood { get; private set; }
	public float FamilyHappiness { get; private set; }
	public float FamilyHealth { get; private set; }
	public float Age { get; private set; }
	public int Money { get; private set; }
	public int MoneyPerWorkshift { get; private set; }
	public int MoneyPerPartnersWorkshift { get; private set; }
	public int FoodSupplies { get; private set; }
	public bool IsFamilyActive { get; private set; }

	public CurrentAction CurrentPlayerAction { get; private set; }
	public CurrentAction CurrentPartnerAction { get; private set; }

	public GameplayConstants Constants;

	// TODO type to "StateItemType?"
	public GameOverReason? GameOver
	{
		get
		{
			foreach (var item in _items)
				if (item.Value.IsGameOverBecauseOfThis())
					// TODO item.Key
					return GameOverReason.Energy;

			if (MyHappiness <= 0)
				return GameOverReason.Happiness;
			if (MyHealth <= 0)
				return GameOverReason.Health;
			if (FamilyFood <= 0)
				return GameOverReason.FFood;
			if (FamilyHappiness <= 0)
				return GameOverReason.FHappiness;
			if (FamilyHealth <= 0)
				return GameOverReason.FHealth;
			if (Money < 0)
				return GameOverReason.Money;
			if (FoodSupplies < 0)
				return GameOverReason.FoodSupplies;
			
			return null;
		}
	}
	
	private static float DeltaTimeInMinutes { get { return Time.deltaTime * (1.0f / 60.0f); }}
	private float DeltaTimeByDurationPlayer { get { return CurrentPlayerAction == null ? 0 : Time.deltaTime * (1.0f / CurrentPlayerAction.Action.DurationInSeconds); }}
	private float DeltaTimeByDurationPartner { get { return CurrentPartnerAction == null ? 0 : Time.deltaTime * (1.0f / CurrentPartnerAction.Action.DurationInSeconds); }}

	public void StartFamily()
	{
		IsFamilyActive = true;
	}

	private void Start ()
	{
		CurrentPlayerAction = null;
		CurrentPartnerAction = null;

		_items = new Dictionary<StateItemType, StateItem>();

		_items.Add(StateItemType.MyMaxEnergy, new StateItem(0, 100, Constants.InitialValues.MyMaxEnergy, Constants.ChangePerMinute.MyMaxEnergy, (d) => d.MyMaxEnergy == 0));
		_items.Add(StateItemType.MyEnergy, new StateItem(0, () => MyMaxEnergy, Constants.InitialValues.MyEnergy, Constants.ChangePerMinute.MyEnergy, (d) => d.MyEnergy == 0));
		_items.Add(StateItemType.MyFood, new StateItem(0, 100, Constants.InitialValues.MyFood, Constants.ChangePerMinute.MyFood, (d) => d.MyFood == 0));
		MyHappiness = Constants.InitialValues.MyHappiness;
		MyHealth = Constants.InitialValues.MyHealth;
		FamilyFood = Constants.InitialValues.FamilyFood;
		FamilyHappiness = Constants.InitialValues.FamilyHappiness;
		FamilyHealth = Constants.InitialValues.FamilyHealth;
		Age = Constants.InitialValues.Age;
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
		ClampStateValues();
	}

	private void UpdateByTime()
	{
		foreach (var item in _items)
			if ((CurrentPlayerAction == null || item.Value.DifferenceHasZeroEffect(CurrentPlayerAction.Action.EffectDuring))
				&& (CurrentPartnerAction == null || item.Value.DifferenceHasZeroEffect(CurrentPartnerAction.Action.EffectDuring)))
				item.Value.Value += item.Value.ChangePerMinute * DeltaTimeInMinutes;

		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.MyHappiness == 0)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.MyHappiness == 0))
			MyHappiness += Constants.ChangePerMinute.MyHappiness * DeltaTimeInMinutes;

		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.MyHealth == 0)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.MyHealth == 0))
			MyHealth += Constants.ChangePerMinute.MyHealth * DeltaTimeInMinutes;

		if (IsFamilyActive
		    && (CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.FamilyFood == 0)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.FamilyFood == 0))
			FamilyFood += Constants.ChangePerMinute.FamilyFood * DeltaTimeInMinutes;

		if (IsFamilyActive
		    && (CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.FamilyHappiness == 0)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.FamilyHappiness == 0))
			FamilyHappiness += Constants.ChangePerMinute.FamilyHappiness * DeltaTimeInMinutes;

		if (IsFamilyActive
		    && (CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.FamilyHealth == 0)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.FamilyHealth == 0))
			FamilyHealth += Constants.ChangePerMinute.FamilyHealth * DeltaTimeInMinutes;

		Age += Constants.ChangePerMinute.Age * DeltaTimeInMinutes;
	}

	private void ClampStateValues()
	{
		MyHappiness = Mathf.Clamp(MyHappiness, 0, 100);
		MyHealth = Mathf.Clamp(MyHealth, 0, 100);
		FamilyFood = Mathf.Clamp(FamilyFood, 0, 100);
		FamilyHappiness = Mathf.Clamp(FamilyHappiness, 0, 100);
		FamilyHealth = Mathf.Clamp(FamilyHealth, 0, 100);
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

			MyMaxEnergy += action.Action.EffectDuring.MyMaxEnergy * deltaTimeByDuration;
			MyEnergy += action.Action.EffectDuring.MyEnergy * deltaTimeByDuration;
			MyFood += action.Action.EffectDuring.MyFood * deltaTimeByDuration;
			MyHappiness += action.Action.EffectDuring.MyHappiness * deltaTimeByDuration;
			MyHealth += action.Action.EffectDuring.MyHealth * deltaTimeByDuration;

			if (IsFamilyActive)
			{
				FamilyFood += action.Action.EffectDuring.FamilyFood * deltaTimeByDuration;
				FamilyHappiness += action.Action.EffectDuring.FamilyHappiness * deltaTimeByDuration;
				FamilyHealth += action.Action.EffectDuring.FamilyHealth * deltaTimeByDuration;
			}
		}

		return action;
	}

	public void RunAction()
	{
		string actionName = EventSystem.current.currentSelectedGameObject.name;
		RunAction(actionName);
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
		MyEnergy += difference.MyEnergy;
		MyMaxEnergy += difference.MyMaxEnergy;
		MyFood += difference.MyFood;
		MyHappiness += difference.MyHappiness;
		MyHealth += difference.MyHealth;
		Money += difference.Money;
		FoodSupplies += difference.FoodSupplies;
		MoneyPerWorkshift += difference.MoneyPerWorkshift;
		MoneyPerPartnersWorkshift += difference.MoneyPerPartnersWorkshift;

		// TODO this if condition doesnt need to be here - it gets clamped later anyways
		if (IsFamilyActive)
		{
			FamilyFood += difference.FamilyFood;
			FamilyHappiness += difference.FamilyHappiness;
			FamilyHealth += difference.FamilyHealth;
		}
	}

	[Serializable]
	public class CurrentAction
	{
		public PlayerAction Action;
		public float RemainingTime;
	}
}

public enum GameOverReason
{
	Energy, Food, Health, Happiness, FFood, FHealth, FHappiness, Money, FoodSupplies 
}
