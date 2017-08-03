using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameState : MonoBehaviour {

    public float MyEnergy = 100;
    public float MyMaxEnergy = 100;
    public float MyFood = 100;
    public float MyHappiness = 100;
    public float MyHealth = 100;
    public float FamilyFood = 100;
    public float FamilyHappiness = 100;
    public float FamilyHealth = 100;
    public float Age = 25;
    public float Money = 0;
    public int MoneyPerWorkshift = 20;
    public int MoneyPerPartnersWorkshift = 20;
    public int FoodSupplies = 5;
	public bool IsFamilyActive;

	public CurrentAction CurrentPlayerAction;
	public CurrentAction CurrentPartnerAction;

	public GameplayConstants Constants;

	public GameOverReason GameOver
	{
		get
		{
			if (MyEnergy <= 0)
				return GameOverReason.Energy;
			if (MyFood <= 0)
				return GameOverReason.Food;
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
			
			return GameOverReason.StillPlaying;
		}
	}
	
	private static float DeltaTimeInMinutes { get { return Time.deltaTime * (1.0f / 60.0f); }}
	private float DeltaTimeByDurationPlayer { get { return CurrentPlayerAction == null ? 0 : Time.deltaTime * (1.0f / CurrentPlayerAction.Action.DurationInSeconds); }}
	private float DeltaTimeByDurationPartner { get { return CurrentPartnerAction == null ? 0 : Time.deltaTime * (1.0f / CurrentPartnerAction.Action.DurationInSeconds); }}

	private void Awake ()
	{
		CurrentPlayerAction = null;
		CurrentPartnerAction = null;
	}
	
	private void FixedUpdate ()
	{
		if (GameOver != GameOverReason.StillPlaying)
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
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.MyEnergy == 0)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.MyEnergy == 0))
			MyEnergy += Constants.ChangePerMinute.MyEnergy * DeltaTimeInMinutes;

		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.MyMaxEnergy == 0)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.MyMaxEnergy == 0))
			MyMaxEnergy += Constants.ChangePerMinute.MyMaxEnergy * DeltaTimeInMinutes;

		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.MyFood == 0)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.MyFood == 0))
			MyFood += Constants.ChangePerMinute.MyFood * DeltaTimeInMinutes;

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
		MyEnergy = Mathf.Clamp(MyEnergy, 0, MyMaxEnergy);
		MyMaxEnergy = Mathf.Clamp(MyMaxEnergy, 0, 100);
		MyFood = Mathf.Clamp(MyFood, 0, 100);
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
			MyEnergy += action.Action.EffectDuring.MyEnergy * deltaTimeByDuration;
			MyMaxEnergy += action.Action.EffectDuring.MyMaxEnergy * deltaTimeByDuration;
			MyFood += action.Action.EffectDuring.MyFood * deltaTimeByDuration;
			MyHappiness += action.Action.EffectDuring.MyHappiness * deltaTimeByDuration;
			MyHealth += action.Action.EffectDuring.MyHealth * deltaTimeByDuration;
			Money += action.Action.EffectDuring.Money * deltaTimeByDuration;

			if (IsFamilyActive)
			{
				FamilyFood += action.Action.EffectDuring.FamilyFood * deltaTimeByDuration;
				FamilyHappiness += action.Action.EffectDuring.FamilyHappiness * deltaTimeByDuration;
				FamilyHealth += action.Action.EffectDuring.FamilyHealth * deltaTimeByDuration;
			}

			action.RemainingTime -= Time.deltaTime;

			if (action.RemainingTime < 0)
			{
				UpdateAfterAction(action.Action);
				action = null;
			}
		}

		return action;
	}

	public void RunAction()
	{
		string type = EventSystem.current.currentSelectedGameObject.name;
		PlayerAction action = Constants.GetPlayerAction(type);
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
	StillPlaying, Energy, Food, Health, Happiness, FFood, FHealth, FHappiness, Money, FoodSupplies 
}
