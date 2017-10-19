using GameOfLife.GameLogic;
using GameOfLife.GameLogic.GameStateAction;
// TODO move to factory
using GameOfLife.GameLogic.GameStateAction.Internal;
using System;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "GameplayConstants", menuName = "Create GameplayConstants", order = 1)]
public class GameplayConstants : ScriptableObject
{
	public StatsDifference InitialValues;
	public StatsDifference ChangePerMinute;
	public PlayerAction[] PlayerActions;

    // TODO move to StateAction factory
	public List<StateAction> GetPlayerActions()
	{
        var result = new List<StateAction>();

        foreach(var action in PlayerActions)
        {
            result.Add(new StateActionImpl
            {
                Name = action.GetName(),
                DurationInSeconds = action.DurationInSeconds,
                EffectBefore = action.EffectBefore,
                EffectDuring = action.EffectDuring,
                EffectAfter = action.EffectAfter,
                ForBoth = action.Type.IsForBoth(),
                PartnersAction = action.Type.IsPartnersAction(),
                WorkAction = action.Type.IsWorkAction()
            });
        }

        return result;
	}

	public static GameplayConstants CreateEmptyConstants()
	{
		var result = ScriptableObject.CreateInstance<GameplayConstants>();

		result.InitialValues = new StatsDifference();
		result.ChangePerMinute = new StatsDifference();
		result.PlayerActions = new PlayerAction[0];

		return result;
	}

    public int GetInitialValue(string itemType)
    {
        return InitialValues.GetStat(itemType);
    }

    public int GetChangePerMinute(string itemType)
    {
        return ChangePerMinute.GetStat(itemType);
    }
}

[Serializable]
public class StatsDifference : ICloneable
{
    [SerializeField]
	private int MyEnergy;
    [SerializeField]
    private int MyMaxEnergy;
    [SerializeField]
    private int MyFood;
    [SerializeField]
    private int MyHappiness;
    [SerializeField]
    private int MyHealth;
    [SerializeField]
    private int FamilyFood;
    [SerializeField]
    private int FamilyHappiness;
    [SerializeField]
    private int FamilyHealth;
    [SerializeField]
    private int Age;
    [SerializeField]
    private int Money;
    [SerializeField]
    private int MoneyPerWorkshift;
    [SerializeField]
    private int MoneyPerPartnersWorkshift;
    [SerializeField]
    private int FoodSupplies;
	
	public object Clone()
	{
		return MemberwiseClone();
	}

    public int GetStat(string type)
    {
        StateItemType typeEnum = (StateItemType) Enum.Parse(typeof(StateItemType), type);
        return GetStat(typeEnum);
    }
    
	private int GetStat(StateItemType type)
	{
		switch (type)
		{
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
            case StateItemType.Age:
                return Age;
            case StateItemType.Money:
                return Money;
            case StateItemType.MySalary:
                return MoneyPerWorkshift;
            case StateItemType.PartnerSalary:
                return MoneyPerPartnersWorkshift;
            case StateItemType.FoodSupplies:
                return FoodSupplies;
            default:
				throw new Exception("This type is not implemented in StatsDifference: " + type.ToString());
		}
	}

    public void Plus(StatsDifference difference)
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
        MoneyPerWorkshift += difference.MoneyPerWorkshift;
        MoneyPerPartnersWorkshift += difference.MoneyPerPartnersWorkshift;
    }

    // TODO redesign so this method doesn't have to exist
    public void SetMoney(int money)
    {
        Money = money;
    }

    // TODO redesign so this method doesn't have to exist
    public void SetMyFood(int food)
    {
        MyFood = food;
    }
}

// TODO redesign to interface somewhere
[Serializable]
public class PlayerAction
{
	public PlayerActionType Type;
	public int DurationInSeconds;
	public StatsDifference EffectBefore;
	public StatsDifference EffectDuring;
	public StatsDifference EffectAfter;

    public string GetName()
    {
        return Type.ToString();
    }
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
