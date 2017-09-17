namespace GameOfLife.GameState
{
	public interface StateItem
	{
		T GetValue<T>();
		bool IsGameOverBecauseOfThis();
		bool DifferenceHasZeroEffect(StatsDifference difference);
		void ApplyDifferenceByTime(float deltaTime);
		void ApplyDifferenceByAction(StatsDifference difference, PlayerAction action, float multiplier = 1);
	}
}
