using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
	public GameState State;
	public GameObject Panel;
	public Text ResultYears;
	public Text Reason;

	private void Start()
	{
		Panel.SetActive(false);
	}

	private void Update () {
		if (State.GameOver == null)
			return;

		Reason.text = GetGameOverReason(State.GameOver);
		
		Panel.SetActive(true);
		ResultYears.text = ResultYears.text.Replace("XXX", Mathf.FloorToInt(State.Age).ToString());
	}

	private string GetGameOverReason(GameOverReason? reason)
	{
		switch (reason)
		{
			case GameOverReason.Energy:
				return "You ran out of energy.";
			case GameOverReason.Food:
				return "You ran out of food.";
			case GameOverReason.Health:
				return "You ran out of health.";
			case GameOverReason.Happiness:
				return "You ran out of happiness.";
			case GameOverReason.FFood:
				return "Family ran out of food.";
			case GameOverReason.FHealth:
				return "Family ran out of health.";
			case GameOverReason.FHappiness:
				return "Family ran out of happiness.";
			case GameOverReason.Money:
				return "You ran out of money.";
			case GameOverReason.FoodSupplies:
				return "You ran out of food supplies.";
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
