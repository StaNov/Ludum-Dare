namespace GameOfLife.GameState
{
	using Internal;

	public static class GameStateFactory
	{
		public static GameState CreateGameState(GameplayConstants constants)
		{
			return new GameStateImpl(constants);
		}
	}
}
