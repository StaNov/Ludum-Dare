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
		if (CurrentPlayerAction != null && CurrentPlayerAction.Action.Type != PlayerActionType.None)
		{
			MyEnergy += CurrentPlayerAction.Action.Effect.MyEnergy * DeltaTimeByDurationPlayer;
			MyMaxEnergy += CurrentPlayerAction.Action.Effect.MyMaxEnergy * DeltaTimeInMinutes;
			MyFood += CurrentPlayerAction.Action.Effect.MyFood * DeltaTimeInMinutes;
			MyHappiness += CurrentPlayerAction.Action.Effect.MyHappiness * DeltaTimeInMinutes;
			MyHealth += CurrentPlayerAction.Action.Effect.MyHealth * DeltaTimeInMinutes;
			FamilyFood += CurrentPlayerAction.Action.Effect.FamilyFood * DeltaTimeInMinutes;
			FamilyHappiness += CurrentPlayerAction.Action.Effect.FamilyHappiness * DeltaTimeInMinutes;
			FamilyHealth += CurrentPlayerAction.Action.Effect.FamilyHealth * DeltaTimeInMinutes;

			CurrentPlayerAction.RemainingTime -= Time.deltaTime;

			if (CurrentPlayerAction.RemainingTime < 0)
			{
				CurrentPlayerAction = null;
			}
		}

		MyEnergy -= Constants.ChangePerMinute.MyEnergy * DeltaTimeInMinutes;
		MyMaxEnergy -= Constants.ChangePerMinute.MyMaxEnergy * DeltaTimeInMinutes;
		MyFood -= Constants.ChangePerMinute.MyFood * DeltaTimeInMinutes;
		MyHappiness -= Constants.ChangePerMinute.MyHappiness * DeltaTimeInMinutes;
		MyHealth -= Constants.ChangePerMinute.MyHealth * DeltaTimeInMinutes;
		FamilyFood -= Constants.ChangePerMinute.FamilyFood * DeltaTimeInMinutes;
		FamilyHappiness -= Constants.ChangePerMinute.FamilyHappiness * DeltaTimeInMinutes;
		FamilyHealth -= Constants.ChangePerMinute.FamilyHealth * DeltaTimeInMinutes;
		Age += Constants.ChangePerMinute.Age * DeltaTimeInMinutes;
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
