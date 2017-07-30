using UnityEngine;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour
{
	public Text Money;
	public Text MoneyPerWorkshift;
	public Text MoneyPerPartnersWorkshift;
	public Text FoodSupplies;
	
	public GameState State;
	public HoveredActionEffect Effect;
	
	void Update ()
	{
		StatsDifference effect = Effect.Effect;
		
		Money.text = "Money: " + Mathf.CeilToInt(State.Money) + EffectSuffixCreator.Create(effect.Money);
		MoneyPerWorkshift.text = "Money per work shift: " + Mathf.FloorToInt(State.MoneyPerWorkshift) + EffectSuffixCreator.Create(effect.MoneyPerWorkshift);
		MoneyPerPartnersWorkshift.text = "Money per partner's work shift: " + Mathf.FloorToInt(State.MoneyPerPartnersWorkshift) + EffectSuffixCreator.Create(effect.MoneyPerPartnersWorkshift);
		FoodSupplies.text = "Food supplies: " + Mathf.FloorToInt(State.FoodSupplies) + EffectSuffixCreator.Create(effect.FoodSupplies);
	}
}
