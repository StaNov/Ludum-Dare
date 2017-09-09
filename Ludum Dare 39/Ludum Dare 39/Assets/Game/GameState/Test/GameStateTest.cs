using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

// TODO redo tests for mock objects afterwards - with constant max value, with dynamic max value etc...
public class GameStateTest : AbstractTest
{
	private const PlayerActionType TestActionType = PlayerActionType.Eat;

	private GameStateHolder TestGameState;
	private GameplayConstants TestConstants;
	
	protected override void BeforeClass()
	{
		Time.timeScale = 20;
	}

	private static GameplayConstants CreateTestingGameplayConstants()
	{
		var result = ScriptableObject.CreateInstance<GameplayConstants>();
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
		result.ChangePerMinute = new StatsDifference();
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

	protected override void SetupSpecific()
	{
		TestConstants = CreateTestingGameplayConstants();
	}

	private IEnumerator CreateTestGameState()
	{
		TestGameState = new GameObject().AddComponent<GameStateHolder>();
		TestGameState.Constants = TestConstants;

		yield return new WaitForEndOfFrame();
	}

	[UnityTest]
	public IEnumerator EnergyIsInitializedAfterSceneLoad()
	{
		int initialEnergy = 99;

		yield return Setup();
		TestConstants.InitialValues.MyEnergy = initialEnergy;
		yield return CreateTestGameState();

		Assert.AreEqual(initialEnergy, TestGameState.State.MyEnergy);
	}

	[UnityTest]
	public IEnumerator AgeIsGreaterAfterSomeTime()
	{
		int initialAge = 10;

		yield return Setup();
		TestConstants.InitialValues.Age = initialAge;
		TestConstants.ChangePerMinute.Age = 11111;
		yield return CreateTestGameState();

		yield return new WaitForSeconds(1);
		Assert.Less(initialAge + 1, TestGameState.State.Age);
	}

	[UnityTest]
	public IEnumerator NotGameOverAfterInitialization()
	{
		yield return Setup();
		yield return CreateTestGameState();

		Assert.IsNull(TestGameState.State.GameOver);
	}

	[UnityTest]
	public IEnumerator GameOverBecauseOfMyEnergy()
	{
		int veryLowEnergy = 1;
		int veryQuickEnergyDecrease = -5 * 60;

		yield return Setup();
		TestConstants.InitialValues.MyEnergy = veryLowEnergy;
		TestConstants.ChangePerMinute.MyEnergy = veryQuickEnergyDecrease;
		yield return CreateTestGameState();

		yield return new WaitForSeconds(1);
		Assert.AreEqual(StateItemType.MyEnergy, TestGameState.State.GameOver);
	}

	[UnityTest]
	public IEnumerator EnergyTopClampedProperly()
	{
		int initialMaxEnergy = 80;

		yield return Setup();
		TestConstants.InitialValues.MyMaxEnergy = initialMaxEnergy;
		TestConstants.InitialValues.MyEnergy = initialMaxEnergy - 5;
		TestConstants.ChangePerMinute.MyEnergy = 10 * 60;
		yield return CreateTestGameState();

		yield return new WaitForSeconds(1);
		Assert.AreEqual(initialMaxEnergy, TestGameState.State.MyEnergy);
	}

	[UnityTest]
	public IEnumerator MaxEnergyTopClampedProperly()
	{
		int initialMaxEnergy = 123;

		// TODO no it is not clean this way, max maxenergy is not setable dynamically, but 100 hardcoded in GameState
		float maxMaxEnergy = 100;

		yield return Setup();
		TestConstants.InitialValues.MyMaxEnergy = initialMaxEnergy;
		TestConstants.ChangePerMinute.MyMaxEnergy = 10 * 60;
		yield return CreateTestGameState();

		Assert.AreEqual(maxMaxEnergy, TestGameState.State.MyMaxEnergy);
		yield return new WaitForSeconds(1);
		Assert.AreEqual(maxMaxEnergy, TestGameState.State.MyMaxEnergy);
	}

	[UnityTest]
	public IEnumerator MaxEnergyBottomClampedProperly()
	{
		int initialMaxEnergy = -999;

		// TODO no it is not clean this way, min maxenergy is not setable dynamically, but 0 hardcoded in GameState
		float minMaxEnergy = 0;

		yield return Setup();
		TestConstants.InitialValues.MyMaxEnergy = initialMaxEnergy;
		TestConstants.ChangePerMinute.MyMaxEnergy = -10 * 60;
		yield return CreateTestGameState();

		Assert.AreEqual(minMaxEnergy, TestGameState.State.MyMaxEnergy);
		yield return new WaitForSeconds(1);
		Assert.AreEqual(minMaxEnergy, TestGameState.State.MyMaxEnergy);
	}

	[UnityTest]
	public IEnumerator UpdateByActionDuringLowersStat()
	{
		int initialFood = 90;
		int foodDecreaseByAction = 10;
		int actionDuration = 2;

		yield return Setup();

		TestConstants.InitialValues.MyFood = initialFood;
		TestConstants.PlayerActions[0].DurationInSeconds = actionDuration;
		TestConstants.PlayerActions[0].EffectDuring.MyFood = -foodDecreaseByAction;

		yield return CreateTestGameState();

		Assert.AreEqual(initialFood, TestGameState.State.MyFood);
		TestGameState.State.RunAction(TestActionType);
		yield return new WaitForSeconds(3);
		Assert.AreEqual(initialFood - foodDecreaseByAction, TestGameState.State.MyFood, 0.01);
	}

	[UnityTest]
	public IEnumerator UpdateByActionBeforeChangesStats()
	{
		int initialEnergy = 90;
		int energyDecreaseByAction = 10;

		yield return Setup();

		TestConstants.InitialValues.MyEnergy = initialEnergy;
		TestConstants.PlayerActions[0].DurationInSeconds = 10;
		TestConstants.PlayerActions[0].EffectBefore.MyEnergy = -energyDecreaseByAction;

		yield return CreateTestGameState();

		Assert.AreEqual(initialEnergy, TestGameState.State.MyEnergy);
		TestGameState.State.RunAction(TestActionType);
		Assert.AreEqual(initialEnergy - energyDecreaseByAction, TestGameState.State.MyEnergy, 0.01);
	}

	[UnityTest]
	public IEnumerator FamilyFoodGetUpdatedWhenFamilyIsActive_ByTime()
	{
		int initialFamilyFood = 50;
		int familyFoodDecrease = 10 * 60;

		yield return Setup();

		TestConstants.InitialValues.FamilyFood = initialFamilyFood;
		TestConstants.ChangePerMinute.FamilyFood = -familyFoodDecrease;

		yield return CreateTestGameState();

		TestGameState.State.StartFamily();
		yield return new WaitForSeconds(2);
		Assert.Greater(initialFamilyFood - familyFoodDecrease / 60, TestGameState.State.FamilyFood);
	}

	[UnityTest]
	public IEnumerator FamilyFoodGetUpdatedWhenFamilyIsActive_ByActionTime()
	{
		int initialFamilyFood = 50;
		int familyFoodDecrease = 10 * 60;
		int effectDuration = 1;
		int slowerDecreaseSoGreaterCanBeUsed = familyFoodDecrease / 2;

		yield return Setup();

		TestConstants.InitialValues.FamilyFood = initialFamilyFood;
		TestConstants.PlayerActions[0].DurationInSeconds = effectDuration;
		TestConstants.PlayerActions[0].EffectDuring.FamilyFood = -familyFoodDecrease;

		yield return CreateTestGameState();

		TestGameState.State.StartFamily();
		TestGameState.State.RunAction(TestActionType);
		yield return new WaitForSeconds(effectDuration + 1);
		Assert.Greater(initialFamilyFood - slowerDecreaseSoGreaterCanBeUsed / 60, TestGameState.State.FamilyFood);
	}

	[UnityTest]
	public IEnumerator FamilyFoodGetUpdatedWhenFamilyIsActive_AfterAction()
	{
		int initialFamilyFood = 50;
		int familyFoodDecrease = 10;
		int effectDuration = 1;

		yield return Setup();

		TestConstants.InitialValues.FamilyFood = initialFamilyFood;
		TestConstants.PlayerActions[0].DurationInSeconds = effectDuration;
		TestConstants.PlayerActions[0].EffectAfter.FamilyFood = -familyFoodDecrease;

		yield return CreateTestGameState();

		TestGameState.State.StartFamily();
		TestGameState.State.RunAction(TestActionType);
		yield return new WaitForSeconds(effectDuration + 1);
		Assert.AreEqual(initialFamilyFood - familyFoodDecrease, TestGameState.State.FamilyFood);
	}

	[UnityTest]
	public IEnumerator FamilyFoodNotGetUpdatedWhenFamilyIsNotActive_ByTime()
	{
		int initialFamilyFood = 50;
		int familyFoodDecrease = 10 * 60;

		yield return Setup();

		TestConstants.InitialValues.FamilyFood = initialFamilyFood;
		TestConstants.ChangePerMinute.FamilyFood = -familyFoodDecrease;

		yield return CreateTestGameState();
		
		yield return new WaitForSeconds(1);
		Assert.AreEqual(initialFamilyFood, TestGameState.State.FamilyFood);
	}

	[UnityTest]
	public IEnumerator FamilyFoodNotGetUpdatedWhenFamilyIsNotActive_ByActionTime()
	{
		int initialFamilyFood = 50;
		int familyFoodDecrease = 10 * 60;

		yield return Setup();

		TestConstants.InitialValues.FamilyFood = initialFamilyFood;
		TestConstants.PlayerActions[0].EffectDuring.FamilyFood = -familyFoodDecrease;

		yield return CreateTestGameState();

		TestGameState.State.RunAction(TestActionType);
		yield return new WaitForSeconds(1);
		Assert.AreEqual(initialFamilyFood, TestGameState.State.FamilyFood);
	}

	[UnityTest]
	public IEnumerator FamilyFoodNotGetUpdatedWhenFamilyIsNotActive_AfterAction()
	{
		int initialFamilyFood = 50;
		int familyFoodDecrease = 10;
		int effectDuration = 1;

		yield return Setup();

		TestConstants.InitialValues.FamilyFood = initialFamilyFood;
		TestConstants.PlayerActions[0].DurationInSeconds = effectDuration;
		TestConstants.PlayerActions[0].EffectAfter.FamilyFood = -familyFoodDecrease;

		yield return CreateTestGameState();

		TestGameState.State.RunAction(TestActionType);
		yield return new WaitForSeconds(effectDuration + 1);
		Assert.AreEqual(initialFamilyFood, TestGameState.State.FamilyFood);
	}
}
