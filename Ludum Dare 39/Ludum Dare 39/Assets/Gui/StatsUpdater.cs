using UnityEngine;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour
{
	public Text Money;
	public Text MoneyPerWorkshift;
	public Text MoneyPerPartnersWorkshift;
	public Text FoodSupplies;
	public Text Age;
	
	public GameState State;
	public HoveredActionEffect Effect;
	
	void Update ()
	{
		StatsDifference effect = Effect.Effect;
		
		Money.text = Mathf.CeilToInt(State.Money) + EffectSuffixCreator.Create(effect.Money);
		MoneyPerWorkshift.text = Mathf.FloorToInt(State.MoneyPerWorkshift) + EffectSuffixCreator.Create(effect.MoneyPerWorkshift);
		MoneyPerPartnersWorkshift.text = Mathf.FloorToInt(State.MoneyPerPartnersWorkshift) + EffectSuffixCreator.Create(effect.MoneyPerPartnersWorkshift);
		FoodSupplies.text = Mathf.FloorToInt(State.FoodSupplies) + EffectSuffixCreator.Create(effect.FoodSupplies);
		Age.text = Mathf.FloorToInt(State.Age).ToString();
	}
}
