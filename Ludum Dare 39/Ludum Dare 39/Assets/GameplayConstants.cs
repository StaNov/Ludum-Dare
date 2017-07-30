using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConstants", menuName = "Create GameplayConstants", order = 1)]
public class GameplayConstants : ScriptableObject
{
	public float MoneyPerShiftIncreaseCoefficient = 1.5f;
	public float MoneyPerShiftIncreaseByLearningCoefficient = 2f;
	
	public StatsDifference ChangePerMinute = new StatsDifference
	{
		MyEnergy = -15,
		MyMaxEnergy = -10,
		MyFood = -50,
		MyHappiness = -30,
		MyHealth = -15,
		FamilyFood = -50,
		FamilyHappiness = -30,
		FamilyHealth = -15,
		Age = 13
	};

	public PlayerAction[] PlayerActions = {
		new PlayerAction
		{
			Type = PlayerActionType.Sleep,
			DurationInSeconds = 15,
			EffectDuring = new StatsDifference
			{
				MyEnergy = 50,
				MyFood = -2,
				MyHappiness = -1,
				MyHealth = -1
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.DrinkCoffee,
			DurationInSeconds = 2,
			EffectDuring = new StatsDifference
			{
				MyEnergy = 30,
				MyMaxEnergy = -5,
				MyHealth = -20
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.GoToWork,
			DurationInSeconds = 15,
			EffectDuring = new StatsDifference
			{
				MyEnergy = -30,
				MyHealth = -10,
				MyHappiness = -10,
				FamilyHappiness = -10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.HaveASmoke,
			DurationInSeconds = 2,
			EffectBefore = new StatsDifference
			{
				Money = -10
			},
			EffectDuring = new StatsDifference
			{
				MyMaxEnergy = -10,
				MyHealth = -20,
				FamilyHealth = -10,
				MyHappiness = 30
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.PartnerFeedsFamily,
			DurationInSeconds = 7,
			EffectBefore = new StatsDifference
			{
				FoodSupplies = -3
			},
			EffectDuring = new StatsDifference
			{
				FamilyFood = 30,
				MyFood = 30,
				FamilyHealth = -10,
				MyHealth = -10,
				FamilyHappiness = -10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.FeedFamily,
			DurationInSeconds = 7,
			EffectBefore = new StatsDifference
			{
				FoodSupplies = -3
			},
			EffectDuring = new StatsDifference
			{
				FamilyFood = 30,
				MyFood = 30,
				FamilyHealth = -10,
				MyHealth = -10,
				MyEnergy = -20
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.Eat,
			DurationInSeconds = 4,
			EffectBefore = new StatsDifference
			{
				FoodSupplies = -1,
			},
			EffectDuring = new StatsDifference
			{
				MyFood = 30,
				MyHealth = -10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.DoHobby,
			DurationInSeconds = 10,
			EffectDuring = new StatsDifference
			{
				MyFood = -10,
				MyHealth = -10,
				MyEnergy = -20,
				MyHappiness = 20,
				FamilyHealth = -10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.DoSport,
			DurationInSeconds = 10,
			EffectDuring = new StatsDifference
			{
				MyFood = -15,
				MyEnergy = -20,
				MyHealth = 30
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.GoShopping,
			DurationInSeconds = 10,
			EffectBefore = new StatsDifference
			{
				Money = -40
			},
			EffectDuring = new StatsDifference
			{
				MyFood = -10,
				MyEnergy = -20,
			},
			EffectAfter = new StatsDifference
			{
				FoodSupplies = 5
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.SpendTimeWithFamily,
			DurationInSeconds = 10,
			EffectDuring = new StatsDifference
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
			EffectDuring = new StatsDifference
			{
				FamilyHealth = 40,
				MyEnergy = -20
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.LearnNewStuffForWork,
			DurationInSeconds = 10,
			EffectDuring = new StatsDifference
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
			EffectDuring = new StatsDifference
			{
				FamilyHappiness = -15,
				FamilyFood = -15,
				FamilyHealth = -10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.PartnerTakesCareOfFamily,
			DurationInSeconds = 10,
			EffectDuring = new StatsDifference
			{
				FamilyHealth = 40,
				FamilyHappiness = -10,
				FamilyFood = -15
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.PartnerGoesShopping,
			DurationInSeconds = 10,
			EffectBefore = new StatsDifference
			{
				Money = -40
			},
			EffectDuring = new StatsDifference
			{
				FamilyFood = -10
			},
			EffectAfter = new StatsDifference
			{
				FoodSupplies = 5
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.PartnerGoesToWork,
			DurationInSeconds = 15,
			EffectDuring = new StatsDifference
			{
				FamilyHealth = -20,
				FamilyFood = -10,
				FamilyHappiness = -10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.HireSomeoneToTakeCareOfFamily,
			DurationInSeconds = 1,
			EffectBefore = new StatsDifference
			{
				Money = -50
			},
			EffectDuring = new StatsDifference
			{
				FamilyHealth = 50,
				FamilyHappiness = -10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.OrderFoodSupplies,
			DurationInSeconds = 1,
			EffectBefore = new StatsDifference
			{
				Money = -80
			},
			EffectDuring = new StatsDifference
			{
				FamilyHappiness = -10
			},
			EffectAfter = new StatsDifference
			{
				FoodSupplies = 5
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.OrderPizza,
			DurationInSeconds = 1,
			EffectBefore = new StatsDifference
			{
				Money = -80
			},
			EffectDuring = new StatsDifference
			{
				FamilyHealth = -20,
				FamilyFood = 30,
				MyFood = 30,
				FamilyHappiness = 10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.TakeAPill,
			DurationInSeconds = 1,
			EffectBefore = new StatsDifference
			{
				Money = -50
			},
			EffectDuring = new StatsDifference
			{
				MyHealth = 40,
				FamilyHappiness = -10,
				MyMaxEnergy = -10
			}
		},
		new PlayerAction
		{
			Type = PlayerActionType.GoToAquaWorld,
			DurationInSeconds = 10,
			EffectBefore = new StatsDifference
			{
				Money = -120
			},
			EffectDuring = new StatsDifference
			{
				MyHappiness = 20,
				FamilyHappiness = 40,
				MyEnergy = -10,
				MyFood = 20,
				FamilyFood = 20
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
	public int MyEnergy;
	public int MyMaxEnergy;
	public int MyFood;
	public int MyHappiness;
	public int MyHealth;
	public int FamilyFood;
	public int FamilyHappiness;
	public int FamilyHealth;
	public int Age;
	public int Money;
	public int MoneyPerWorkshift;
	public int MoneyPerPartnersWorkshift;
	public int FoodSupplies;
	
	public object Clone()
	{
		return MemberwiseClone();
	}
}

[Serializable]
public class PlayerAction
{
	public PlayerActionType Type;
	public int DurationInSeconds;
	public StatsDifference EffectBefore;
	public StatsDifference EffectDuring;
	public StatsDifference EffectAfter;
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
	OrderPizza,
	TakeAPill,
	GoToAquaWorld,
	GoToSpa
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
	
	public static bool IsForBoth(this PlayerActionType type)
	{
		switch(type)
		{
			case PlayerActionType.GoToAquaWorld:
			case PlayerActionType.SpendTimeWithFamily:
				return true;
				
			default:
				return false;
		}
	}
	
	public static string Localized(this PlayerActionType type)
	{
		switch(type)
		{
			case PlayerActionType.None:
				return "";
			case PlayerActionType.Sleep:
				return "Sleeping";
			case PlayerActionType.DrinkCoffee:
				return "Drinking coffee";
			case PlayerActionType.GoToWork:
				return "In work";
			case PlayerActionType.HaveASmoke:
				return "Smoking";
			case PlayerActionType.PartnerFeedsFamily:
				return "Feeding family";
			case PlayerActionType.Eat:
				return "Eating";
			case PlayerActionType.DoHobby:
				return "Doing hobby";
			case PlayerActionType.DoSport:
				return "Doing sport";
			case PlayerActionType.GoShopping:
				return "Shopping";
			case PlayerActionType.FeedFamily:
				return "Feeding family";
			case PlayerActionType.SpendTimeWithFamily:
				return "Being together";
			case PlayerActionType.TakeCareOfFamily:
				return "Taking care of family";
			case PlayerActionType.LearnNewStuffForWork:
				return "Learning new skills";
			case PlayerActionType.LearnNewStuffForWorkPartner:
				return "Learning new skills";
			case PlayerActionType.PartnerTakesCareOfFamily:
				return "Taking care of family";
			case PlayerActionType.PartnerGoesShopping:
				return "Shopping";
			case PlayerActionType.PartnerGoesToWork:
				return "In work";
			case PlayerActionType.HireSomeoneToTakeCareOfFamily:
				return "Hiring cleaning someone";
			case PlayerActionType.OrderFoodSupplies:
				return "Ordering meals";
			case PlayerActionType.OrderPizza:
				return "Ordering pizza";
			case PlayerActionType.TakeAPill:
				return "Taking a pill";
			case PlayerActionType.GoToAquaWorld:
				return "In water park";
			case PlayerActionType.GoToSpa:
				return "In spa";
			default:
				throw new ArgumentOutOfRangeException("type", type, null);
		}
	}
}