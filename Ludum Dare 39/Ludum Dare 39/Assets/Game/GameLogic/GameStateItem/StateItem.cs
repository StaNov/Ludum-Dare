namespace GameOfLife.GameLogic.GameStateItem
{
	public interface StateItem
	{
        string GetName();
		T GetValue<T>();
		bool UpdateIfFamilyNotActive();
		bool IsGameOverBecauseOfThis();
		bool DifferenceHasZeroEffect(StatsDifference difference);
		void ApplyDifferenceByTime(float deltaTime);
		void ApplyDifferenceByAction(StatsDifference difference, PlayerAction action, float multiplier = 1);
	}
}
