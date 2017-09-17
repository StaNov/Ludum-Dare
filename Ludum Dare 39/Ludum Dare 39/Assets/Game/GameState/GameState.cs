namespace GameOfLife.GameState
{
	public interface GameState
	{
		StateItemType? GameOver { get; }
		CurrentAction CurrentPartnerAction { get; }
		CurrentAction CurrentPlayerAction { get; }

		// TODO delete those int types
		int Money { get; }
		int MoneyPerWorkshift { get; }
		int MoneyPerPartnersWorkshift { get; }
		int FoodSupplies { get; }

		void RunAction(string actionName);
		void ApplyTime(float deltaTime);
		float GetStateItemValue(StateItemType age);
		void StartFamily();
	}

	public class CurrentAction
	{
		public PlayerAction Action;
		public float RemainingTime;
	}
}
