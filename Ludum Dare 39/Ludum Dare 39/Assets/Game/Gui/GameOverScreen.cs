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

	private string GetGameOverReason(StateItemType? reason)
	{
		switch (reason)
		{
			case StateItemType.MyEnergy:
			case StateItemType.MyMaxEnergy:
				return "You ran out of energy.";
			case StateItemType.MyFood:
				return "You ran out of food.";
			case StateItemType.MyHealth:
				return "You ran out of health.";
			case StateItemType.MyHappiness:
				return "You ran out of happiness.";
			case StateItemType.FamilyFood:
				return "Family ran out of food.";
			case StateItemType.FamilyHealth:
				return "Family ran out of health.";
			case StateItemType.FamilyHappiness:
				return "Family ran out of happiness.";
			case StateItemType.Money:
				// TODO remove
				return "You ran out of money.";
			case StateItemType.FoodSupplies:
				// TODO remove
				return "You ran out of food supplies.";
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
