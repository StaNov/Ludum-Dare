using UnityEngine;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour
{
	public Text Money;
	public Text MoneyPerWorkshift;
	public Text MoneyPerPartnersWorkshift;
	public Text FoodSupplies;
	public GameState State;
	
	void Update ()
	{
		Money.text = "Money: " + Mathf.CeilToInt(State.Money);
		MoneyPerWorkshift.text = "Money per work shift: " + Mathf.FloorToInt(State.MoneyPerWorkshift);
		MoneyPerPartnersWorkshift.text = "Money per partner's work shift: " + Mathf.FloorToInt(State.MoneyPerPartnersWorkshift);
		FoodSupplies.text = "Food supplies: " + Mathf.FloorToInt(State.FoodSupplies);
	}
}
