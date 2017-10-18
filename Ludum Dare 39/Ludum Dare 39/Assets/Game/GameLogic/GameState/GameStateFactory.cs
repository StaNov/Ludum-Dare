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

            StateItemFloat myMaxEnergy = new StateItemFloat(StateItemType.MyMaxEnergy.ToString(), 0, 100, InitValue(constants, StateItemType.MyMaxEnergy), PerMinute(constants, StateItemType.MyMaxEnergy), true);
            StateItemInt mySalary = new StateItemInt(StateItemType.MySalary.ToString(), InitValue(constants, StateItemType.MySalary));
            StateItemInt partnerSalary = new StateItemInt(StateItemType.PartnerSalary.ToString(), InitValue(constants, StateItemType.PartnerSalary));

            items.Add(new StateItemFloat(StateItemType.Age.ToString(), 0, 99999, InitValue(constants, StateItemType.Age), PerMinute(constants, StateItemType.Age), true));
            items.Add(myMaxEnergy);
            items.Add(new StateItemFloat(StateItemType.MyEnergy.ToString(), 0, () => myMaxEnergy.GetValue<float>(), InitValue(constants, StateItemType.MyEnergy), PerMinute(constants, StateItemType.MyEnergy), true));
            items.Add(new StateItemFloat(StateItemType.MyFood.ToString(), 0, 100, InitValue(constants, StateItemType.MyFood), PerMinute(constants, StateItemType.MyFood), true));
            items.Add(new StateItemFloat(StateItemType.MyHappiness.ToString(), 0, 100, InitValue(constants, StateItemType.MyHappiness), PerMinute(constants, StateItemType.MyHappiness), true));
            items.Add(new StateItemFloat(StateItemType.MyHealth.ToString(), 0, 100, InitValue(constants, StateItemType.MyHealth), PerMinute(constants, StateItemType.MyHealth), true));
            items.Add(new StateItemFloat(StateItemType.FamilyFood.ToString(), 0, 100, InitValue(constants, StateItemType.FamilyFood), PerMinute(constants, StateItemType.FamilyFood), false));
            items.Add(new StateItemFloat(StateItemType.FamilyHappiness.ToString(), 0, 100, InitValue(constants, StateItemType.FamilyHappiness), PerMinute(constants, StateItemType.FamilyHappiness), false));
            items.Add(new StateItemFloat(StateItemType.FamilyHealth.ToString(), 0, 100, InitValue(constants, StateItemType.FamilyHealth), PerMinute(constants, StateItemType.FamilyHealth), false));
            items.Add(new StateItemMoney(StateItemType.Money.ToString(), InitValue(constants, StateItemType.Money), () => mySalary.GetValue<int>(), () => partnerSalary.GetValue<int>()));
            items.Add(mySalary);
            items.Add(partnerSalary);
            items.Add(new StateItemInt(StateItemType.FoodSupplies.ToString(), InitValue(constants, StateItemType.FoodSupplies)));

            List<PlayerAction> actions = constants.GetPlayerActions();

            return new GameStateImpl(items, actions);
		}

        private static int InitValue(GameplayConstants constants, StateItemType type)
        {
            return constants.InitialValues.GetStat(type.ToString());
        }

        private static int PerMinute(GameplayConstants constants, StateItemType type)
        {
            return constants.ChangePerMinute.GetStat(type.ToString());
        }
    }
}
