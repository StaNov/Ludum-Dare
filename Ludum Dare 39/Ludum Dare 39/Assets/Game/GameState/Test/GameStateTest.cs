using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

// TODO redo tests for mock objects afterwards - with constant max value, with dynamic max value etc...
public class GameStateTest : AbstractTest
{
	private const int InitialAge = 40;
	private const int InitialMoney = 12345;

	private GameState TestGameState;
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

		return result;
	}

	protected override void SetupSpecific()
	{
		TestConstants = CreateTestingGameplayConstants();
	}

	private IEnumerator CreateTestGameState()
	{
		TestGameState = new GameObject().AddComponent<GameState>();
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

		Assert.AreEqual(initialEnergy, TestGameState.MyEnergy);
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
		Assert.Less(initialAge + 1, TestGameState.Age);
	}

	[UnityTest]
	public IEnumerator NotGameOverAfterInitialization()
	{
		yield return Setup();
		yield return CreateTestGameState();

		Assert.IsNull(TestGameState.GameOver);
	}

	[UnityTest]
	public IEnumerator GameOverAfterSomeTime()
	{
		int veryLowEnergy = 1;
		int veryQuickEnergyDecrease = -5 * 60;

		yield return Setup();
		TestConstants.InitialValues.MyEnergy = veryLowEnergy;
		TestConstants.ChangePerMinute.MyEnergy = veryQuickEnergyDecrease;
		yield return CreateTestGameState();

		yield return new WaitForSeconds(1);
		Assert.AreEqual(GameOverReason.Energy, TestGameState.GameOver);
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
		Assert.AreEqual(initialMaxEnergy, TestGameState.MyEnergy);
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

		Assert.AreEqual(maxMaxEnergy, TestGameState.MyMaxEnergy);
		yield return new WaitForSeconds(1);
		Assert.AreEqual(maxMaxEnergy, TestGameState.MyMaxEnergy);
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

		Assert.AreEqual(minMaxEnergy, TestGameState.MyMaxEnergy);
		yield return new WaitForSeconds(1);
		Assert.AreEqual(minMaxEnergy, TestGameState.MyMaxEnergy);
	}
}
