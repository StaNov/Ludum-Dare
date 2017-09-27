namespace GameOfLife.GameLogic.GameState
{
	public interface IGameState
	{
		StateItemType? GameOver { get; }
		CurrentAction CurrentPartnerAction { get; }
		CurrentAction CurrentPlayerAction { get; }

		void RunAction(string actionName);
		void ApplyTime(float deltaTime);
		T GetStateItemValue<T>(string type);
		void StartFamily();
	}

	public class CurrentAction
	{
		public PlayerAction Action;
		public float RemainingTime;
	}
}
