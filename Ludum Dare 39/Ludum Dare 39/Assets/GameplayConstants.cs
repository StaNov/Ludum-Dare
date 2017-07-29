using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConstants", menuName = "Create GameplayConstants", order = 1)]
public class GameplayConstants : ScriptableObject
{
	public float MoneyPerShiftIncreaseCoefficient = 1.5f;
	
	public StatsDifference ChangePerMinute = new StatsDifference
	{
		MyEnergy = -30,
		MyMaxEnergy = -10,
		MyFood = -50,
		MyHappiness = -50,
		MyHealth = -50,
		FamilyFood = -50,
		FamilyHappiness = -50,
		FamilyHealth = -50,
		Age = 10
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
				MyEnergy = 30,
				MyHealth = -20
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.GoToWork,
			DurationInSeconds = 15,
			Effect = new StatsDifference
			{
				MyEnergy = -30
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.HaveASmoke,
			DurationInSeconds = 2,
			Effect = new StatsDifference
			{
				MyMaxEnergy = -10,
				Money = -10,
				MyHealth = -10,
				FamilyHealth = -10,
				MyHappiness = 30
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.PartnerFeedsFamily,
			DurationInSeconds = 7,
			Effect = new StatsDifference
			{
				FamilyFood = 30,
				FoodSupplies = -2
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.Eat,
			DurationInSeconds = 4,
			Effect = new StatsDifference
			{
				MyFood = 30,
				FoodSupplies = -1,
				MyHealth = -10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.DoHobby,
			DurationInSeconds = 10,
			Effect = new StatsDifference
			{
				MyFood = -10,
				MyHealth = -10,
				MyEnergy = -20,
				MyHappiness = 20
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.DoSport,
			DurationInSeconds = 10,
			Effect = new StatsDifference
			{
				MyFood = -10,
				MyEnergy = -20,
				MyHealth = 30
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.GoShopping,
			DurationInSeconds = 15,
			Effect = new StatsDifference
			{
				MyFood = -10,
				MyEnergy = -20,
				FoodSupplies = 5,
				Money = -40
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
	public float Money;
	public float MoneyPerWorkshift;
	public float MoneyPerPartnersWorkshift;
	public float FoodSupplies;
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
	None,
	Sleep,
	DrinkCoffee,
	GoToWork,
	HaveASmoke,
	PartnerFeedsFamily,
	Eat,
	DoHobby,
	DoSport,
	GoShopping
}

public static class ExtensionMethods
{
	public static bool IsPartnersAction(this PlayerActionType type)
	{
		switch(type)
		{
			case PlayerActionType.PartnerFeedsFamily:
				return true;
				
			default:
				return false;
		}
	}
}