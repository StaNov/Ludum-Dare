namespace GameOfLife.GameLogic
{
	using UnityEngine;
	using UnityEngine.EventSystems;
    using GameState;

	public class GameStateHolder : MonoBehaviour
    {
        // TODO make protected methods that should be executed only inside of package
        public IGameState State { get; private set; }

        public GameplayConstants Constants;

		private void Start()
		{
			State = GameStateFactory.CreateGameState(Constants);
		}

		private void FixedUpdate()
		{
			State.ApplyTime(Time.deltaTime);
        }

        // TODO move to button logic
        public void RunAction()
        {
            string actionName = EventSystem.current.currentSelectedGameObject.name;
            State.RunAction(actionName);
        }
    }
}
