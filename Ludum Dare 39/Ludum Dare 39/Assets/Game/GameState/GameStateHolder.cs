namespace GameOfLife.GameState
{
	using UnityEngine;
	using UnityEngine.EventSystems;

	public class GameStateHolder : MonoBehaviour
	{
		private GameState _state;

		public GameplayConstants Constants;

		// TODO make protected methods that should be executed only inside of package
		public GameState State { get { return _state; } }

		// TODO move to button logic
		public void RunAction()
		{
			string actionName = EventSystem.current.currentSelectedGameObject.name;
			_state.RunAction(actionName);
		}

		private void Start()
		{
			_state = GameStateFactory.CreateGameState(Constants);
		}

		private void FixedUpdate()
		{
			_state.ApplyTime(Time.deltaTime);
		}
	}
}
