using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConstants", menuName = "Create GameplayConstants", order = 1)]
public class GameplayConstants : ScriptableObject
{
	public float MoneyPerShiftIncreaseCoefficient = 1.2f;
	public float MoneyPerShiftIncreaseByLearningCoefficient = 1.5f;
	
	public StatsDifference ChangePerMinute = new StatsDifference
	{
		MyEnergy = -15,
		MyMaxEnergy = -10,
		MyFood = -25,
		MyHappiness = -25,
		MyHealth = -25,
		FamilyFood = -25,
		FamilyHappiness = -25,
		FamilyHealth = -25,
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
			Type = PlayerActionType.FeedFamily,
			DurationInSeconds = 7,
			Effect = new StatsDifference
			{
				FamilyFood = 30,
				FoodSupplies = -2,
				MyEnergy = -20
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
			DurationInSeconds = 10,
			Effect = new StatsDifference
			{
				MyFood = -10,
				MyEnergy = -20,
				FoodSupplies = 5,
				Money = -40
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.SpendTimeWithFamily,
			DurationInSeconds = 10,
			Effect = new StatsDifference
			{
				FamilyHappiness = 40,
				MyHappiness = 40,
				MyEnergy = -20
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.TakeCareOfFamily,
			DurationInSeconds = 10,
			Effect = new StatsDifference
			{
				FamilyHealth = 40,
				MyEnergy = -20
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.LearnNewStuffForWork,
			DurationInSeconds = 10,
			Effect = new StatsDifference
			{
				MyEnergy = -20,
				MyHappiness = -20,
				FamilyHappiness = -5
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.LearnNewStuffForWorkPartner,
			DurationInSeconds = 10,
			Effect = new StatsDifference
			{
				FamilyHappiness = -5
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.PartnerTakesCareOfFamily,
			DurationInSeconds = 10,
			Effect = new StatsDifference
			{
				FamilyHealth = 40,
				FamilyHappiness = -5
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.PartnerGoesShopping,
			DurationInSeconds = 10,
			Effect = new StatsDifference
			{
				FoodSupplies = 5,
				FamilyFood = -10,
				Money = -40
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.PartnerGoesToWork,
			DurationInSeconds = 15,
			Effect = new StatsDifference
			{
				FamilyHealth = -10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.HireSomeoneToTakeCareOfFamily,
			DurationInSeconds = 1,
			Effect = new StatsDifference
			{
				FamilyHealth = 50,
				Money = -50,
				FamilyHappiness = -10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.OrderFoodSupplies,
			DurationInSeconds = 1,
			Effect = new StatsDifference
			{
				FoodSupplies = 5,
				Money = -80,
				FamilyHappiness = -10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.OrderPizza,
			DurationInSeconds = 1,
			Effect = new StatsDifference
			{
				FamilyHealth = -20,
				Money = -80,
				FamilyFood = 30,
				FamilyHappiness = 10
			}
		}
	};

	public PlayerAction GetPlayerAction(string typeString)
	{
		PlayerActionType type = (PlayerActionType) Enum.Parse(typeof(PlayerActionType), typeString);
		
		foreach (var action in PlayerActions)
		{
			if (type == action.Type)
				return action;
		}

		throw new Exception("NO ACTION FOUND!");
	}
}

[Serializable]
public class StatsDifference : ICloneable
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
	
	public object Clone()
	{
		return MemberwiseClone();
	}
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
	GoShopping,
	FeedFamily,
	SpendTimeWithFamily,
	TakeCareOfFamily,
	LearnNewStuffForWork,
	LearnNewStuffForWorkPartner,
	PartnerTakesCareOfFamily,
	PartnerGoesShopping,
	PartnerGoesToWork,
	HireSomeoneToTakeCareOfFamily,
	OrderFoodSupplies,
	OrderPizza
}

public static class ExtensionMethods
{
	public static bool IsPartnersAction(this PlayerActionType type)
	{
		switch(type)
		{
			case PlayerActionType.PartnerFeedsFamily:
			case PlayerActionType.LearnNewStuffForWorkPartner:
			case PlayerActionType.PartnerTakesCareOfFamily:
			case PlayerActionType.PartnerGoesShopping:
			case PlayerActionType.PartnerGoesToWork:
				return true;
				
			default:
				return false;
		}
	}
}