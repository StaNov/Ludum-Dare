namespace GameOfLife.GameLogic.GameStateItem.Internal
{
	using System;

	public class StateItemInt : StateItemGeneric<int>
	{
		public StateItemInt(string name, GameplayConstants constants, Func<StatsDifference, int> getDifferenceValue)
			: base(name, constants, getDifferenceValue, true) { }

		public override void ApplyDifferenceByAction(StatsDifference difference, PlayerAction action, float multiplier = 1)
		{
			Value += GetDifferenceValue(difference);
		}

		public override bool DifferenceHasZeroEffect(StatsDifference difference)
		{
			return GetDifferenceValue(difference) == 0;
		}
	}
}
