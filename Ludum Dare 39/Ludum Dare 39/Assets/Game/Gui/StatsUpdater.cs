using GameOfLife.GameState;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour
{
	public Text Money;
	public Text MoneyPerWorkshift;
	public Text MoneyPerPartnersWorkshift;
	public Text FoodSupplies;
	public Text Age;
	
	public GameStateHolder State;
	public HoveredActionEffect Effect;
	
	void Update ()
	{
		StatsDifference effect = Effect.Effect;
		
		Money.text = State.State.GetStateItemValue<int>(StateItemType.Money) + EffectSuffixCreator.Create(effect.Money);
		MoneyPerWorkshift.text = Mathf.FloorToInt(State.State.GetStateItemValue<int>(StateItemType.MySalary)) + EffectSuffixCreator.Create(effect.MoneyPerWorkshift);
		MoneyPerPartnersWorkshift.text = Mathf.FloorToInt(State.State.GetStateItemValue<int>(StateItemType.PartnerSalary)) + EffectSuffixCreator.Create(effect.MoneyPerPartnersWorkshift);
		FoodSupplies.text = Mathf.FloorToInt(State.State.GetStateItemValue<int>(StateItemType.FoodSupplies)) + EffectSuffixCreator.Create(effect.FoodSupplies);
		Age.text = Mathf.FloorToInt(State.State.GetStateItemValue<float>(StateItemType.Age)).ToString();
	}
}
