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
    public float Money = 100;
    public int MoneyPerWorkshift = 20;
    public int MoneyPerPartnersWorkshift = 20;
    public int FoodSupplies = 5;

	public CurrentAction CurrentPlayerAction;
	public CurrentAction CurrentPartnerAction;

	public GameplayConstants Constants;
	
	public bool IsGameOver { get {return MyEnergy <= 0 || MyFood <= 0 || MyHappiness <= 0 || MyHealth <= 0 || FamilyFood <= 0 || FamilyHappiness <= 0 || FamilyHealth <= 0 || Money < 0 || FoodSupplies < 0;}}
	
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
		if (IsGameOver)
		{
			return;
		}
		
		CurrentPlayerAction = UpdateByAction(CurrentPlayerAction, DeltaTimeByDurationPlayer);
		CurrentPartnerAction = UpdateByAction(CurrentPartnerAction, DeltaTimeByDurationPartner);

		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.MyEnergy < float.Epsilon)
			&& (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.MyEnergy < float.Epsilon))
			MyEnergy += Constants.ChangePerMinute.MyEnergy * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.MyMaxEnergy < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.MyMaxEnergy < float.Epsilon))
			MyMaxEnergy += Constants.ChangePerMinute.MyMaxEnergy * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.MyFood < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.MyFood < float.Epsilon))
			MyFood += Constants.ChangePerMinute.MyFood * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.MyHappiness < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.MyHappiness < float.Epsilon))
			MyHappiness += Constants.ChangePerMinute.MyHappiness * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.MyHealth < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.MyHealth < float.Epsilon))
			MyHealth += Constants.ChangePerMinute.MyHealth * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.FamilyFood < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.FamilyFood < float.Epsilon))
			FamilyFood += Constants.ChangePerMinute.FamilyFood * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.FamilyHappiness < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.FamilyHappiness < float.Epsilon))
			FamilyHappiness += Constants.ChangePerMinute.FamilyHappiness * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.EffectDuring.FamilyHealth < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.EffectDuring.FamilyHealth < float.Epsilon))
			FamilyHealth += Constants.ChangePerMinute.FamilyHealth * DeltaTimeInMinutes;
		
		Age += Constants.ChangePerMinute.Age * DeltaTimeInMinutes;
		
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
			FamilyFood += action.Action.EffectDuring.FamilyFood * deltaTimeByDuration;
			FamilyHappiness += action.Action.EffectDuring.FamilyHappiness * deltaTimeByDuration;
			FamilyHealth += action.Action.EffectDuring.FamilyHealth * deltaTimeByDuration;
			Money += action.Action.EffectDuring.Money * deltaTimeByDuration;

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
		
		if (! isPartnersAction && CurrentPlayerAction != null && CurrentPlayerAction.Action.Type != PlayerActionType.None
		    || isPartnersAction && CurrentPartnerAction != null && CurrentPartnerAction.Action.Type != PlayerActionType.None
		    || isPartnersAction && CurrentPlayerAction != null && CurrentPlayerAction.Action.Type.IsForBoth()
		    || ! isPartnersAction && CurrentPartnerAction != null && action.Type.IsForBoth())
		{
			return;
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
				MoneyPerWorkshift = Mathf.RoundToInt(MoneyPerWorkshift * Constants.MoneyPerShiftIncreaseCoefficient);
				break;
			case PlayerActionType.PartnerGoesToWork:
				Money += MoneyPerPartnersWorkshift;
				MoneyPerPartnersWorkshift = Mathf.RoundToInt(MoneyPerPartnersWorkshift * Constants.MoneyPerShiftIncreaseCoefficient);
				break;
			case PlayerActionType.LearnNewStuffForWork:
				MoneyPerWorkshift = Mathf.RoundToInt(MoneyPerWorkshift * Constants.MoneyPerShiftIncreaseByLearningCoefficient);
				break;
			case PlayerActionType.LearnNewStuffForWorkPartner:
				MoneyPerPartnersWorkshift = Mathf.RoundToInt(MoneyPerPartnersWorkshift * Constants.MoneyPerShiftIncreaseByLearningCoefficient);
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
		FamilyFood += difference.FamilyFood;
		FamilyHappiness += difference.FamilyHappiness;
		FamilyHealth += difference.FamilyHealth;
		Money += difference.Money;
		FoodSupplies += difference.FoodSupplies;
	}

	[Serializable]
	public class CurrentAction
	{
		public PlayerAction Action;
		public float RemainingTime;
	}
}
