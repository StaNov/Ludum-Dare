using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public CurrentAction CurrentPlayerAction;
	public CurrentAction CurrentPartnerAction;

	public GameplayConstants Constants;

	public static GameState Instance;
	
	public bool IsGameOver { get {return MyEnergy <= 0 || MyFood <= 0 || MyHappiness <= 0 || MyHealth <= 0 || FamilyFood <= 0 || FamilyHappiness <= 0 || FamilyHealth <= 0;}}
	
	private static float DeltaTimeInMinutes { get { return Time.deltaTime * (1.0f / 60.0f); }}
	private float DeltaTimeByDurationPlayer { get { return Time.deltaTime * (1.0f / CurrentPlayerAction.Action.DurationInSeconds); }}
	private float DeltaTimeByDurationPartner { get { return Time.deltaTime * (1.0f / CurrentPartnerAction.Action.DurationInSeconds); }}

	private void Awake ()
	{
		Instance = this;
		CurrentPlayerAction = null;
	}
	
	private void FixedUpdate ()
	{
		if (IsGameOver)
		{
			return;
		}
		
		if (CurrentPlayerAction != null && CurrentPlayerAction.Action.Type != PlayerActionType.None)
		{
			MyEnergy += CurrentPlayerAction.Action.Effect.MyEnergy * DeltaTimeByDurationPlayer;
			MyMaxEnergy += CurrentPlayerAction.Action.Effect.MyMaxEnergy * DeltaTimeByDurationPlayer;
			MyFood += CurrentPlayerAction.Action.Effect.MyFood * DeltaTimeByDurationPlayer;
			MyHappiness += CurrentPlayerAction.Action.Effect.MyHappiness * DeltaTimeByDurationPlayer;
			MyHealth += CurrentPlayerAction.Action.Effect.MyHealth * DeltaTimeByDurationPlayer;
			FamilyFood += CurrentPlayerAction.Action.Effect.FamilyFood * DeltaTimeByDurationPlayer;
			FamilyHappiness += CurrentPlayerAction.Action.Effect.FamilyHappiness * DeltaTimeByDurationPlayer;
			FamilyHealth += CurrentPlayerAction.Action.Effect.FamilyHealth * DeltaTimeByDurationPlayer;

			CurrentPlayerAction.RemainingTime -= Time.deltaTime;

			if (CurrentPlayerAction.RemainingTime < 0)
			{
				CurrentPlayerAction = null;
			}
		}

		MyEnergy += Constants.ChangePerMinute.MyEnergy * DeltaTimeInMinutes;
		MyMaxEnergy += Constants.ChangePerMinute.MyMaxEnergy * DeltaTimeInMinutes;
		MyFood += Constants.ChangePerMinute.MyFood * DeltaTimeInMinutes;
		MyHappiness += Constants.ChangePerMinute.MyHappiness * DeltaTimeInMinutes;
		MyHealth += Constants.ChangePerMinute.MyHealth * DeltaTimeInMinutes;
		FamilyFood += Constants.ChangePerMinute.FamilyFood * DeltaTimeInMinutes;
		FamilyHappiness += Constants.ChangePerMinute.FamilyHappiness * DeltaTimeInMinutes;
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

	public void RunPlayerAction(string type)
	{
		if (CurrentPlayerAction != null && CurrentPlayerAction.Action.Type != PlayerActionType.None)
		{
			return;
		}
		
		PlayerAction action = Constants.GetPlayerAction((PlayerActionType) Enum.Parse(typeof(PlayerActionType), type));
		
		CurrentPlayerAction = new CurrentAction
		{
			Action = action,
			RemainingTime = action.DurationInSeconds
		};
	}

	[Serializable]
	public class CurrentAction
	{
		public PlayerAction Action;
		public float RemainingTime;
	}
}
