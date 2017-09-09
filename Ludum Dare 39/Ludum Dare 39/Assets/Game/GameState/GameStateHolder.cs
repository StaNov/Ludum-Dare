using UnityEngine;
using UnityEngine.EventSystems;

public class GameStateHolder : MonoBehaviour
{
	private GameState _state;

	public GameplayConstants Constants;

	// TODO return only the values we need to expose
	public GameState State { get { return _state; } }

	// TODO move to button logic
	public void RunAction()
	{
		string actionName = EventSystem.current.currentSelectedGameObject.name;
		_state.RunAction(actionName);
	}

	private void Start()
	{
		_state = new GameState(Constants);
	}

	private void FixedUpdate()
	{
		_state.ApplyTime(Time.deltaTime);
	}
}
