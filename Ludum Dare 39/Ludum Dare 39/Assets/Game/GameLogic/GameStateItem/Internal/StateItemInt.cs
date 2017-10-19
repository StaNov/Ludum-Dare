namespace GameOfLife.GameLogic.GameStateItem.Internal
{
    using GameOfLife.GameLogic.GameStateAction;

    public class StateItemInt : StateItemGeneric<int>
	{
		public StateItemInt(string name, int initialValue)
			: base(name, initialValue, true) { }

		public override void ApplyDifferenceByAction(StatsDifference difference, StateAction action, float multiplier = 1)
		{
			Value += GetDifferenceValue(difference);
		}

		public override bool DifferenceHasZeroEffect(StatsDifference difference)
		{
			return GetDifferenceValue(difference) == 0;
		}
        
        protected override int GetDifferenceValue(StatsDifference difference)
        {
            return difference.GetStat(GetName());
        }
    }
}
