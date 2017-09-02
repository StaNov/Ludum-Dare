using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameStateTest : AbstractTest
{
	private const int InitialAge = 40;
	private const int InitialMoney = 12345;

	private GameState State;
	private GameplayConstants TestConstants;
	
	protected override void BeforeClass()
	{
		Time.timeScale = 20;
		TestConstants = CreateTestingGameplayConstants();
	}

	private static GameplayConstants CreateTestingGameplayConstants()
	{
		var result = ScriptableObject.CreateInstance<GameplayConstants>();
		result.InitialValues = new StatsDifference
		{
			Age = InitialAge,
			Money = InitialMoney,
			MyMaxEnergy = 1111,
			MyEnergy = 1000,
			MyHappiness = 1,
			MyHealth = 30,
			FamilyFood = 10,
			FamilyHappiness = 20,
			FamilyHealth = 30,
			FoodSupplies = 99,
			MyFood = 20
		};
		result.ChangePerMinute = new StatsDifference
		{
			Age = 11111,
			MyHappiness = -5 * 60
		};

		return result;
	}

	protected override void SetupSpecific()
	{
		State = new GameObject().AddComponent<GameState>();
		State.Constants = TestConstants;
	}

	[UnityTest]
	public IEnumerator MoneyStatIsInitializedAfterSceneLoad()
	{
		yield return Setup();
		Assert.AreEqual(State.Money, InitialMoney);
	}

	[UnityTest]
	public IEnumerator AgeIsGreaterAfterSomeTime()
	{
		yield return Setup();
		yield return new WaitForSeconds(1);
		Assert.Greater(State.Age, InitialAge + 1);
	}

	[UnityTest]
	public IEnumerator NotGameOverAfterInitialization()
	{
		yield return Setup();
		Assert.AreEqual(State.GameOver, null);
	}

	[UnityTest]
	public IEnumerator GameOverAfterSomeTime()
	{
		yield return Setup();
		yield return new WaitForSeconds(1);
		Assert.AreEqual(State.GameOver, GameOverReason.Happiness);
	}
}
