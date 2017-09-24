namespace GameOfLife.GameLogic.GameState
{
	using Internal;

	public static class GameStateFactory
	{
		public static IGameState CreateGameState(GameplayConstants constants)
		{
			return new GameStateImpl(constants);
		}
	}
}
