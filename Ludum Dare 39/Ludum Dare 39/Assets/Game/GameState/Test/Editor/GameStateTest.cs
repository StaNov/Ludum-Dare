using UnityEngine;
using NUnit.Framework;

// TODO test only stuff related to state, not to items
// TODO redo tests for mock objects afterwards - with constant max value, with dynamic max value etc...
public class GameStateTest
{
	private const PlayerActionType TestActionType = PlayerActionType.Eat;
	private const PlayerActionType TestActionTypeWork = PlayerActionType.GoToWork;

	private GameState TestGameState;
	private GameplayConstants TestConstants;

	private static GameplayConstants CreateTestingGameplayConstants()
	{
		var result = GameplayConstants.CreateEmptyConstants();
		result.InitialValues = new StatsDifference
		{
			Age = 1,
			Money = 9999,
			MyMaxEnergy = 9999,
			MyEnergy = 100,
			MyHappiness = 100,
			MyHealth = 100,
			FamilyFood = 100,
			FamilyHappiness = 100,
			FamilyHealth = 100,
			FoodSupplies = 99,
			MyFood = 99
		};
		result.PlayerActions = new PlayerAction[1];
		result.PlayerActions[0] = new PlayerAction
		{
			Type = TestActionType,
			DurationInSeconds = 1,
			EffectBefore = new StatsDifference(),
			EffectDuring = new StatsDifference(),
			EffectAfter = new StatsDifference()
		};

		return result;
	}

	private void CreateTestConstants()
	{
		TestConstants = CreateTestingGameplayConstants();
	}

	private void CreateTestGameState()
	{
		TestGameState = new GameState(TestConstants);
	}

	[Test]
	public void EnergyIsInitializedAfterSceneLoad()
	{
		int initialEnergy = 99;

		CreateTestConstants();
		TestConstants.InitialValues.MyEnergy = initialEnergy;
		CreateTestGameState();

		Assert.AreEqual(initialEnergy, TestGameState.GetStateItemValue(StateItemType.MyEnergy));
	}

	[Test]
	public void AgeIsGreaterAfterSomeTime()
	{
		int initialAge = 10;

		CreateTestConstants();
		TestConstants.InitialValues.Age = initialAge;
		TestConstants.ChangePerMinute.Age = 11111;
		CreateTestGameState();

		TestGameState.ApplyTime(1);
		Assert.Less(initialAge + 1, TestGameState.GetStateItemValue(StateItemType.Age));
	}

	[Test]
	public void NotGameOverAfterInitialization()
	{
		// TODO create specific stateitem maybe? to not duplicate stateitem tests?
		CreateTestConstants();
		CreateTestGameState();

		Assert.IsNull(TestGameState.GameOver);
	}

	[Test]
	public void GameOverBecauseOfMyEnergy()
	{
		int veryLowEnergy = 1;
		int veryQuickEnergyDecrease = -5 * 60;

		CreateTestConstants();
		TestConstants.InitialValues.MyEnergy = veryLowEnergy;
		TestConstants.ChangePerMinute.MyEnergy = veryQuickEnergyDecrease;
		CreateTestGameState();

		TestGameState.ApplyTime(1);
		Assert.AreEqual(StateItemType.MyEnergy, TestGameState.GameOver);
	}

	[Test]
	public void UpdateByActionDuringLowersStat()
	{
		int initialFood = 90;
		int foodDecreaseByAction = 10;
		int actionDuration = 2;

		CreateTestConstants();

		TestConstants.InitialValues.MyFood = initialFood;
		TestConstants.PlayerActions[0].DurationInSeconds = actionDuration;
		TestConstants.PlayerActions[0].EffectDuring.MyFood = -foodDecreaseByAction;

		CreateTestGameState();
		
		TestGameState.RunAction(TestActionType);
		TestGameState.ApplyTime(3);
		Assert.AreEqual(initialFood - foodDecreaseByAction, TestGameState.GetStateItemValue(StateItemType.MyFood), 0.01);
	}

	[Test]
	public void UpdateByActionBeforeChangesStats()
	{
		int initialEnergy = 90;
		int energyDecreaseByAction = 10;

		CreateTestConstants();

		TestConstants.InitialValues.MyEnergy = initialEnergy;
		TestConstants.PlayerActions[0].DurationInSeconds = 10;
		TestConstants.PlayerActions[0].EffectBefore.MyEnergy = -energyDecreaseByAction;

		CreateTestGameState();
		
		TestGameState.RunAction(TestActionType);
		Assert.AreEqual(initialEnergy - energyDecreaseByAction, TestGameState.GetStateItemValue(StateItemType.MyEnergy), 0.01);
	}

	[Test]
	public void FamilyFoodGetUpdatedWhenFamilyIsActive_ByTime()
	{
		int initialFamilyFood = 50;
		int familyFoodDecrease = 10 * 60;

		CreateTestConstants();

		TestConstants.InitialValues.FamilyFood = initialFamilyFood;
		TestConstants.ChangePerMinute.FamilyFood = -familyFoodDecrease;

		CreateTestGameState();

		TestGameState.StartFamily();
		TestGameState.ApplyTime(2);
		Assert.Greater(initialFamilyFood - familyFoodDecrease / 60, TestGameState.GetStateItemValue(StateItemType.FamilyFood));
	}

	[Test]
	public void FamilyFoodGetUpdatedWhenFamilyIsActive_ByActionTime()
	{
		int initialFamilyFood = 50;
		int familyFoodDecrease = 10 * 60;
		int effectDuration = 1;
		int slowerDecreaseSoGreaterCanBeUsed = familyFoodDecrease / 2;

		CreateTestConstants();

		TestConstants.InitialValues.FamilyFood = initialFamilyFood;
		TestConstants.PlayerActions[0].DurationInSeconds = effectDuration;
		TestConstants.PlayerActions[0].EffectDuring.FamilyFood = -familyFoodDecrease;

		CreateTestGameState();

		TestGameState.StartFamily();
		TestGameState.RunAction(TestActionType);
		TestGameState.ApplyTime(effectDuration + 1);
		Assert.Greater(initialFamilyFood - slowerDecreaseSoGreaterCanBeUsed / 60, TestGameState.GetStateItemValue(StateItemType.FamilyFood));
	}

	[Test]
	public void FamilyFoodGetUpdatedWhenFamilyIsActive_AfterAction()
	{
		int initialFamilyFood = 50;
		int familyFoodDecrease = 10;
		int effectDuration = 1;

		CreateTestConstants();

		TestConstants.InitialValues.FamilyFood = initialFamilyFood;
		TestConstants.PlayerActions[0].DurationInSeconds = effectDuration;
		TestConstants.PlayerActions[0].EffectAfter.FamilyFood = -familyFoodDecrease;

		CreateTestGameState();

		TestGameState.StartFamily();
		TestGameState.RunAction(TestActionType);
		TestGameState.ApplyTime(effectDuration + 1);
		Assert.AreEqual(initialFamilyFood - familyFoodDecrease, TestGameState.GetStateItemValue(StateItemType.FamilyFood));
	}

	[Test]
	public void FamilyFoodNotGetUpdatedWhenFamilyIsNotActive_ByTime()
	{
		int initialFamilyFood = 50;
		int familyFoodDecrease = 10 * 60;

		CreateTestConstants();

		TestConstants.InitialValues.FamilyFood = initialFamilyFood;
		TestConstants.ChangePerMinute.FamilyFood = -familyFoodDecrease;

		CreateTestGameState();

		TestGameState.ApplyTime(1);
		Assert.AreEqual(initialFamilyFood, TestGameState.GetStateItemValue(StateItemType.FamilyFood));
	}

	[Test]
	public void FamilyFoodNotGetUpdatedWhenFamilyIsNotActive_ByActionTime()
	{
		int initialFamilyFood = 50;
		int familyFoodDecrease = 10 * 60;

		CreateTestConstants();

		TestConstants.InitialValues.FamilyFood = initialFamilyFood;
		TestConstants.PlayerActions[0].EffectDuring.FamilyFood = -familyFoodDecrease;

		CreateTestGameState();

		TestGameState.RunAction(TestActionType);
		TestGameState.ApplyTime(1);
		Assert.AreEqual(initialFamilyFood, TestGameState.GetStateItemValue(StateItemType.FamilyFood));
	}

	[Test]
	public void FamilyFoodNotGetUpdatedWhenFamilyIsNotActive_AfterAction()
	{
		int initialFamilyFood = 50;
		int familyFoodDecrease = 10;
		int effectDuration = 1;

		CreateTestConstants();

		TestConstants.InitialValues.FamilyFood = initialFamilyFood;
		TestConstants.PlayerActions[0].DurationInSeconds = effectDuration;
		TestConstants.PlayerActions[0].EffectAfter.FamilyFood = -familyFoodDecrease;

		CreateTestGameState();

		TestGameState.RunAction(TestActionType);
		TestGameState.ApplyTime(effectDuration + 1);
		Assert.AreEqual(initialFamilyFood, TestGameState.GetStateItemValue(StateItemType.FamilyFood));
	}

	[Test]
	public void MoneyProperlyInitialized()
	{
		int initialMoney = 50;

		CreateTestConstants();

		TestConstants.InitialValues.Money = initialMoney;

		CreateTestGameState();
		
		Assert.AreEqual(initialMoney, TestGameState.GetStateItemValue<int>(StateItemType.Money));
	}

	[Test]
	public void MoneyProperlyAppliedBeforeAction()
	{
		int initialMoney = 50;
		int differenceBeforeAction = -20;

		CreateTestConstants();

		TestConstants.InitialValues.Money = initialMoney;
		TestConstants.PlayerActions[0].EffectBefore.Money = differenceBeforeAction;

		CreateTestGameState();

		TestGameState.RunAction(TestActionType);

		Assert.AreEqual(initialMoney + differenceBeforeAction, TestGameState.GetStateItemValue<int>(StateItemType.Money));
	}

	[Test]
	public void MoneyProperlyAppliedAfterAction()
	{
		int initialMoney = 50;
		int differenceAfterAction = -20;
		int actionDuration = 1;

		CreateTestConstants();

		TestConstants.InitialValues.Money = initialMoney;
		TestConstants.PlayerActions[0].DurationInSeconds = actionDuration;
		TestConstants.PlayerActions[0].EffectAfter.Money = differenceAfterAction;

		CreateTestGameState();

		TestGameState.RunAction(TestActionType);

		Assert.AreEqual(initialMoney, TestGameState.GetStateItemValue<int>(StateItemType.Money));

		TestGameState.ApplyTime(actionDuration);

		Assert.AreEqual(initialMoney + differenceAfterAction, TestGameState.GetStateItemValue<int>(StateItemType.Money));
	}

	[Test]
	public void MoneyAddedAfterWork()
	{
		int initialMoney = 10;
		int initialSalary = 20;
		int salaryIncreaseAfterWorkshift = 30;
		int actionDuration = 1;

		CreateTestConstants();

		TestConstants.InitialValues.Money = initialMoney;
		TestConstants.InitialValues.MoneyPerWorkshift = initialSalary;
		TestConstants.PlayerActions[0].DurationInSeconds = actionDuration;
		TestConstants.PlayerActions[0].Type = TestActionTypeWork;
		TestConstants.PlayerActions[0].EffectAfter.MoneyPerWorkshift = salaryIncreaseAfterWorkshift; // just to make sure it gets added after money is raised by previous salary

		CreateTestGameState();

		TestGameState.RunAction(TestActionTypeWork);
		TestGameState.ApplyTime(actionDuration);

		Assert.AreEqual(initialMoney + initialSalary, TestGameState.GetStateItemValue<int>(StateItemType.Money));
	}
}
