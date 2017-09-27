namespace GameOfLife.GameLogic.GameState
{
    // TODO remove internal dependency, redo creating items to factory
    using GameOfLife.GameLogic.GameStateItem.Internal;
    using GameStateItem;
    using Internal;
    using System.Collections.Generic;

    public static class GameStateFactory
	{
		public static IGameState CreateGameState(GameplayConstants constants)
        {
            Dictionary<string, StateItem> items = new Dictionary<string, StateItem>();

            StateItemFloat myMaxEnergy = new StateItemFloat(0, 100, constants, (d) => d.MyMaxEnergy, true);
            StateItemInt mySalary = new StateItemInt(constants, (d) => d.MoneyPerWorkshift);
            StateItemInt partnerSalary = new StateItemInt(constants, (d) => d.MoneyPerPartnersWorkshift);

            items.Add(StateItemType.Age.ToString(), new StateItemFloat(0, 99999, constants, (d) => d.Age, true));
            items.Add(StateItemType.MyMaxEnergy.ToString(), myMaxEnergy);
            items.Add(StateItemType.MyEnergy.ToString(), new StateItemFloat(0, () => myMaxEnergy.GetValue<float>(), constants, (d) => d.MyEnergy, true));
            items.Add(StateItemType.MyFood.ToString(), new StateItemFloat(0, 100, constants, (d) => d.MyFood, true));
            items.Add(StateItemType.MyHappiness.ToString(), new StateItemFloat(0, 100, constants, (d) => d.MyHappiness, true));
            items.Add(StateItemType.MyHealth.ToString(), new StateItemFloat(0, 100, constants, (d) => d.MyHealth, true));
            items.Add(StateItemType.FamilyFood.ToString(), new StateItemFloat(0, 100, constants, (d) => d.FamilyFood, false));
            items.Add(StateItemType.FamilyHappiness.ToString(), new StateItemFloat(0, 100, constants, (d) => d.FamilyHappiness, false));
            items.Add(StateItemType.FamilyHealth.ToString(), new StateItemFloat(0, 100, constants, (d) => d.FamilyHealth, false));
            items.Add(StateItemType.Money.ToString(), new StateItemMoney(constants, (d) => d.Money, () => mySalary.GetValue<int>(), () => partnerSalary.GetValue<int>()));
            items.Add(StateItemType.MySalary.ToString(), mySalary);
            items.Add(StateItemType.PartnerSalary.ToString(), partnerSalary);
            items.Add(StateItemType.FoodSupplies.ToString(), new StateItemInt(constants, (d) => d.FoodSupplies));

            Dictionary<string, PlayerAction> actions = constants.GetPlayerActions();

            return new GameStateImpl(items, actions);
		}
	}
}
