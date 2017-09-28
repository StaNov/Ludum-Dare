using System.Collections.Generic;

namespace GameOfLife.GameLogic.GameState
{
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
		public PlayerAction Action;
		public float RemainingTime;
	}
}
