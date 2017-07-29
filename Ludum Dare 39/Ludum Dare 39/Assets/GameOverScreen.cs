using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
	public GameState State;
	public GameObject Panel;
	public Text ResultYears;

	private void Start()
	{
		Panel.SetActive(false);
	}

	private void Update () {
		if (! State.IsGameOver)
			return;
		
		Panel.SetActive(true);
		ResultYears.text = ResultYears.text.Replace("XXX", Mathf.FloorToInt(State.Age).ToString());
	}
}
