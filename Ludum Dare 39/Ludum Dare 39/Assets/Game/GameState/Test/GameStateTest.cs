using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

// TODO swap expected and actual
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

		Assert.AreEqual(TestGameState.MyEnergy, initialEnergy);
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
		Assert.Greater(TestGameState.Age, initialAge + 1);
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
		int veryLowHappiness = 1;
		int veryQuickHappinessDecrease = -5 * 60;

		yield return Setup();
		TestConstants.InitialValues.MyHappiness = veryLowHappiness;
		TestConstants.ChangePerMinute.MyHappiness = veryQuickHappinessDecrease;
		yield return CreateTestGameState();

		yield return new WaitForSeconds(1);
		Assert.AreEqual(TestGameState.GameOver, GameOverReason.Happiness);
	}

	[UnityTest]
	public IEnumerator HappinessTopClampedProperly()
	{
		yield return Setup();
		TestConstants.InitialValues.MyHappiness = 99;
		TestConstants.ChangePerMinute.MyHappiness = 1 * 60;
		yield return CreateTestGameState();

		yield return new WaitForSeconds(1);
		Assert.LessOrEqual(TestGameState.MyHappiness, 100f);
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
		Assert.AreEqual(TestGameState.MyEnergy, initialMaxEnergy);
	}

	[UnityTest]
	public IEnumerator MaxEnergyTopClampedProperly()
	{
		int initialMaxEnergy = 123;
		float maxMaxEnergy = 100;

		yield return Setup();
		TestConstants.InitialValues.MyMaxEnergy = initialMaxEnergy;
		TestConstants.ChangePerMinute.MyMaxEnergy = 10 * 60;
		yield return CreateTestGameState();

		Assert.AreEqual(TestGameState.MyMaxEnergy, maxMaxEnergy);
		yield return new WaitForSeconds(1);
		Assert.AreEqual(TestGameState.MyMaxEnergy, maxMaxEnergy);
	}
}
