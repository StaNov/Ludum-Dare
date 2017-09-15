using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConstants", menuName = "Create GameplayConstants", order = 1)]
public class GameplayConstants : ScriptableObject
{
	public StatsDifference InitialValues;
	public StatsDifference ChangePerMinute;
	public PlayerAction[] PlayerActions;

	public PlayerAction GetPlayerAction(string typeString)
	{
		PlayerActionType type = (PlayerActionType) Enum.Parse(typeof(PlayerActionType), typeString);
		return GetPlayerAction(type);
	}
	
	public PlayerAction GetPlayerAction(PlayerActionType type)
	{
		foreach (var action in PlayerActions)
		{
			if (type == action.Type)
				return action;
		}

		throw new Exception("NO ACTION FOUND!");
	}

	public static GameplayConstants CreateEmptyConstants()
	{
		var result = ScriptableObject.CreateInstance<GameplayConstants>();

		result.InitialValues = new StatsDifference();
		result.ChangePerMinute = new StatsDifference();
		result.PlayerActions = new PlayerAction[0];

		return result;
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

	// TODO redesign, load dynamically
	public int GetStat(StateItemType type)
	{
		switch (type)
		{
			case StateItemType.Age:
				return Age;
			case StateItemType.MyMaxEnergy:
				return MyMaxEnergy;
			case StateItemType.MyEnergy:
				return MyEnergy;
			case StateItemType.MyFood:
				return MyFood;
			case StateItemType.MyHappiness:
				return MyHappiness;
			case StateItemType.MyHealth:
				return MyHealth;
			case StateItemType.FamilyFood:
				return FamilyFood;
			case StateItemType.FamilyHappiness:
				return FamilyHappiness;
			case StateItemType.FamilyHealth:
				return FamilyHealth;
			default:
				throw new Exception("This type is not implemented in StatsDifference: " + type.ToString());
		}
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

	public static bool IsWorkAction(this PlayerActionType type)
	{
		switch (type)
		{
			case PlayerActionType.GoToWork:
			case PlayerActionType.PartnerGoesToWork:
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
