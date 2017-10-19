namespace GameOfLife.GameLogic.GameStateItem
{
    using GameOfLife.GameLogic.GameStateAction;

    public interface StateItem
	{
        string GetName();
		T GetValue<T>();
		bool UpdateIfFamilyNotActive();
		bool IsGameOverBecauseOfThis();
		bool DifferenceHasZeroEffect(StatsDifference difference);
		void ApplyDifferenceByTime(float deltaTime);
		void ApplyDifferenceByAction(StatsDifference difference, StateAction action, float multiplier = 1);
	}
}
