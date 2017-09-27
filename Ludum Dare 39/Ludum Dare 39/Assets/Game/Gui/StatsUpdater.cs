using GameOfLife.GameLogic;
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
		
		Money.text = State.State.GetStateItemValue<int>(StateItemType.Money.ToString()) + EffectSuffixCreator.Create(effect.Money);
		MoneyPerWorkshift.text = Mathf.FloorToInt(State.State.GetStateItemValue<int>(StateItemType.MySalary.ToString())) + EffectSuffixCreator.Create(effect.MoneyPerWorkshift);
		MoneyPerPartnersWorkshift.text = Mathf.FloorToInt(State.State.GetStateItemValue<int>(StateItemType.PartnerSalary.ToString())) + EffectSuffixCreator.Create(effect.MoneyPerPartnersWorkshift);
		FoodSupplies.text = Mathf.FloorToInt(State.State.GetStateItemValue<int>(StateItemType.FoodSupplies.ToString())) + EffectSuffixCreator.Create(effect.FoodSupplies);
		Age.text = Mathf.FloorToInt(State.State.GetStateItemValue<float>(StateItemType.Age.ToString())).ToString();
	}
}
