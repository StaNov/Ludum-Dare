using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConstants", menuName = "Create GameplayConstants", order = 1)]
public class GameplayConstants : ScriptableObject
{
	public StatsDifference ChangePerMinute = new StatsDifference
	{
		MyEnergy = -10,
		MyMaxEnergy = -1,
		MyFood = -5,
		MyHappiness = -5,
		MyHealth = -5,
		FamilyFood = -5,
		FamilyHappiness = -5,
		FamilyHealth = -5,
		Age = 2
	};

	public PlayerAction[] PlayerActions = {
		new PlayerAction
		{
			Type = PlayerActionType.Sleep,
			DurationInSeconds = 15,
			Effect = new StatsDifference
			{
				MyEnergy = 50
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.DrinkCoffee,
			DurationInSeconds = 2,
			Effect = new StatsDifference
			{
				MyEnergy = 10,
				MyHealth = -10
			}
		}
	};

	public PlayerAction GetPlayerAction(PlayerActionType type)
	{
		foreach (var action in PlayerActions)
		{
			if (type == action.Type)
				return action;
		}

		throw new Exception("NO ACTION FOUND!");
	}
}

[Serializable]
public class StatsDifference
{
	public float MyEnergy;
	public float MyMaxEnergy;
	public float MyFood;
	public float MyHappiness;
	public float MyHealth;
	public float FamilyFood;
	public float FamilyHappiness;
	public float FamilyHealth;
	public float Age;
}

[Serializable]
public class PlayerAction
{
	public PlayerActionType Type;
	public float DurationInSeconds;
	public StatsDifference Effect;
}

public enum PlayerActionType
{
	None, Sleep, DrinkCoffee
}