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
    public float MoneyPerWorkshift = 20;
    public float MoneyPerPartnersWorkshift = 20;
    public float FoodSupplies = 5;

	public CurrentAction CurrentPlayerAction;
	public CurrentAction CurrentPartnerAction;

	public GameplayConstants Constants;

	public static GameState Instance;
	
	public bool IsGameOver { get {return MyEnergy <= 0 || MyFood <= 0 || MyHappiness <= 0 || MyHealth <= 0 || FamilyFood <= 0 || FamilyHappiness <= 0 || FamilyHealth <= 0 || Money < 0 || FoodSupplies < 0;}}
	
	private static float DeltaTimeInMinutes { get { return Time.deltaTime * (1.0f / 60.0f); }}
	private float DeltaTimeByDurationPlayer { get { return CurrentPlayerAction == null ? 0 : Time.deltaTime * (1.0f / CurrentPlayerAction.Action.DurationInSeconds); }}
	private float DeltaTimeByDurationPartner { get { return CurrentPartnerAction == null ? 0 : Time.deltaTime * (1.0f / CurrentPartnerAction.Action.DurationInSeconds); }}

	private void Awake ()
	{
		Instance = this;
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

		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.Effect.MyEnergy < float.Epsilon)
			&& (CurrentPartnerAction == null || CurrentPartnerAction.Action.Effect.MyEnergy < float.Epsilon))
			MyEnergy += Constants.ChangePerMinute.MyEnergy * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.Effect.MyMaxEnergy < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.Effect.MyMaxEnergy < float.Epsilon))
			MyMaxEnergy += Constants.ChangePerMinute.MyMaxEnergy * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.Effect.MyFood < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.Effect.MyFood < float.Epsilon))
			MyFood += Constants.ChangePerMinute.MyFood * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.Effect.MyHappiness < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.Effect.MyHappiness < float.Epsilon))
			MyHappiness += Constants.ChangePerMinute.MyHappiness * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.Effect.MyHealth < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.Effect.MyHealth < float.Epsilon))
			MyHealth += Constants.ChangePerMinute.MyHealth * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.Effect.FamilyFood < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.Effect.FamilyFood < float.Epsilon))
			FamilyFood += Constants.ChangePerMinute.FamilyFood * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.Effect.FamilyHappiness < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.Effect.FamilyHappiness < float.Epsilon))
			FamilyHappiness += Constants.ChangePerMinute.FamilyHappiness * DeltaTimeInMinutes;
		
		if ((CurrentPlayerAction == null || CurrentPlayerAction.Action.Effect.FamilyHealth < float.Epsilon)
		    && (CurrentPartnerAction == null || CurrentPartnerAction.Action.Effect.FamilyHealth < float.Epsilon))
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
			MyEnergy += action.Action.Effect.MyEnergy * deltaTimeByDuration;
			MyMaxEnergy += action.Action.Effect.MyMaxEnergy * deltaTimeByDuration;
			MyFood += action.Action.Effect.MyFood * deltaTimeByDuration;
			MyHappiness += action.Action.Effect.MyHappiness * deltaTimeByDuration;
			MyHealth += action.Action.Effect.MyHealth * deltaTimeByDuration;
			FamilyFood += action.Action.Effect.FamilyFood * deltaTimeByDuration;
			FamilyHappiness += action.Action.Effect.FamilyHappiness * deltaTimeByDuration;
			FamilyHealth += action.Action.Effect.FamilyHealth * deltaTimeByDuration;
			Money += action.Action.Effect.Money * deltaTimeByDuration;
			FoodSupplies += action.Action.Effect.FoodSupplies * deltaTimeByDuration;

			action.RemainingTime -= Time.deltaTime;

			if (action.RemainingTime < 0)
			{
				switch (action.Action.Type)
				{
					case PlayerActionType.GoToWork:
						Money += MoneyPerWorkshift;
						MoneyPerWorkshift *= Constants.MoneyPerShiftIncreaseCoefficient;
						break;
					case PlayerActionType.LearnNewStuffForWork:
						MoneyPerWorkshift *= Constants.MoneyPerShiftIncreaseByLearningCoefficient;
						break;
					case PlayerActionType.LearnNewStuffForWorkPartner:
						MoneyPerPartnersWorkshift *= Constants.MoneyPerShiftIncreaseByLearningCoefficient;
						break;
				}

				action = null;
			}
		}

		return action;
	}

	public void RunAction()
	{
		string type = EventSystem.current.currentSelectedGameObject.name;
		PlayerAction action = Constants.GetPlayerAction((PlayerActionType) Enum.Parse(typeof(PlayerActionType), type));
		bool isPartnersAction = action.Type.IsPartnersAction();
		
		if (! isPartnersAction && CurrentPlayerAction != null && CurrentPlayerAction.Action.Type != PlayerActionType.None
		    || isPartnersAction && CurrentPartnerAction != null && CurrentPartnerAction.Action.Type != PlayerActionType.None)
		{
			return;
		}

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

	[Serializable]
	public class CurrentAction
	{
		public PlayerAction Action;
		public float RemainingTime;
	}
}
