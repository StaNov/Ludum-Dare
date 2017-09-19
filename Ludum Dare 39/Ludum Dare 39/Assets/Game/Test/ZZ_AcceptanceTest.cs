namespace GameOfLife.GameState
{
	using UnityEngine;
	using UnityEngine.TestTools;
	using System.Collections;
	using UnityEngine.SceneManagement;

	public class ZZ_AcceptanceTest
	{
		private GameState _gameState;

		[UnityTest]
		public IEnumerator AcceptanceTest()
		{
			SceneManager.LoadScene("Game");
			yield return null;
			_gameState = Object.FindObjectOfType<GameStateHolder>().State;
			yield break;
		}
	}
}
