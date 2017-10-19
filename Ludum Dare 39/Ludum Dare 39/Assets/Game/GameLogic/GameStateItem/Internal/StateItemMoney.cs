namespace GameOfLife.GameLogic.GameStateItem.Internal
{
    using GameStateAction;
    using System;

	public class StateItemMoney : StateItemInt
	{
		private Func<int> _getPlayerSalary;
		private Func<int> _getPartnerSalary;

		public StateItemMoney(string name, int initialValue, Func<int> getPlayerSalary, Func<int> getPartnerSalary)
			: base(name, initialValue)
		{
			_getPlayerSalary = getPlayerSalary;
			_getPartnerSalary = getPartnerSalary;
		}

		public override void ApplyDifferenceByAction(StatsDifference difference, StateAction action, float multiplier = 1)
		{
			base.ApplyDifferenceByAction(difference, action, multiplier);

			// TODO aaaaargh ugly comparison of objects to find out if it's after-action difference!
			// maybe create methods UpdateBeforeAction and UpdateAfterAction in StateItem and make default implementation the same, just override it in money case
			if (action.IsWorkAction() && difference == action.GetEffectAfter())
			{
				Value += action.IsPartnersAction() ? _getPartnerSalary() : _getPlayerSalary();
			}
		}
	}
}
