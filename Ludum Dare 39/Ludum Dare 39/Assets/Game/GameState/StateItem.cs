public interface StateItem
{
	T GetValue<T>();
	bool IsGameOverBecauseOfThis();
	bool DifferenceHasZeroEffect(StatsDifference difference);
	void ApplyDifferenceByTime(float deltaTime);
	void ApplyDifference(StatsDifference difference, float multiplier = 1);
}

public enum StateItemType
{
	Age,
	MyEnergy,
	MyMaxEnergy,
	MyFood,
	MyHealth,
	MyHappiness,
	FamilyFood,
	FamilyHealth,
	FamilyHappiness,
	Money,
	FoodSupplies,
	MySalary,
	PartnerSalary
}
