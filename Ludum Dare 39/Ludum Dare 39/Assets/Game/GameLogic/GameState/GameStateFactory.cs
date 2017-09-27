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
            List<StateItem> items = new List<StateItem>();

            StateItemFloat myMaxEnergy = new StateItemFloat(StateItemType.MyMaxEnergy.ToString(), 0, 100, constants, (d) => d.MyMaxEnergy, true);
            StateItemInt mySalary = new StateItemInt(StateItemType.MySalary.ToString(), constants, (d) => d.MoneyPerWorkshift);
            StateItemInt partnerSalary = new StateItemInt(StateItemType.PartnerSalary.ToString(), constants, (d) => d.MoneyPerPartnersWorkshift);

            items.Add(new StateItemFloat(StateItemType.Age.ToString(), 0, 99999, constants, (d) => d.Age, true));
            items.Add(myMaxEnergy);
            items.Add(new StateItemFloat(StateItemType.MyEnergy.ToString(), 0, () => myMaxEnergy.GetValue<float>(), constants, (d) => d.MyEnergy, true));
            items.Add(new StateItemFloat(StateItemType.MyFood.ToString(), 0, 100, constants, (d) => d.MyFood, true));
            items.Add(new StateItemFloat(StateItemType.MyHappiness.ToString(), 0, 100, constants, (d) => d.MyHappiness, true));
            items.Add(new StateItemFloat(StateItemType.MyHealth.ToString(), 0, 100, constants, (d) => d.MyHealth, true));
            items.Add(new StateItemFloat(StateItemType.FamilyFood.ToString(), 0, 100, constants, (d) => d.FamilyFood, false));
            items.Add(new StateItemFloat(StateItemType.FamilyHappiness.ToString(), 0, 100, constants, (d) => d.FamilyHappiness, false));
            items.Add(new StateItemFloat(StateItemType.FamilyHealth.ToString(), 0, 100, constants, (d) => d.FamilyHealth, false));
            items.Add(new StateItemMoney(StateItemType.Money.ToString(), constants, (d) => d.Money, () => mySalary.GetValue<int>(), () => partnerSalary.GetValue<int>()));
            items.Add(mySalary);
            items.Add(partnerSalary);
            items.Add(new StateItemInt(StateItemType.FoodSupplies.ToString(), constants, (d) => d.FoodSupplies));

            Dictionary<string, PlayerAction> actions = constants.GetPlayerActions();

            return new GameStateImpl(items, actions);
		}
	}
}
