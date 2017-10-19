namespace GameOfLife.GameLogic.GameState
{
    using GameOfLife.GameLogic.GameStateAction;
    using System.Collections.Generic;

    public interface IGameState
	{
		string GameOver { get; }
        KeyValuePair<string, float>? GetCurrentPlayerAction();
        KeyValuePair<string, float>? GetCurrentPartnerAction();

		void RunAction(string actionName);
		void ApplyTime(float deltaTime);
		T GetStateItemValue<T>(string type);
		void StartFamily();
	}

	public class CurrentAction
	{
		public StateAction Action;
		public float RemainingTime;
	}
}
