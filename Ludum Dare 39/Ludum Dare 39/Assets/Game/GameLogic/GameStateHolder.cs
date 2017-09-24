namespace GameOfLife.GameLogic
{
	using UnityEngine;
	using UnityEngine.EventSystems;
    using GameState;

	public class GameStateHolder : MonoBehaviour
	{
		private IGameState _state;

		public GameplayConstants Constants;

		// TODO make protected methods that should be executed only inside of package
		public IGameState State { get { return _state; } }

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
