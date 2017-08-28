using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateTest
{
	private GameState State;

	private const int InitialAge = 40;
	private const int InitialMoney = 12345;

	public IEnumerator Setup()
	{
		yield return DestroyAllObjectsInScene();

		State = new GameObject().AddComponent<GameState>();
		State.Constants = CreateTestingGameplayConstants();
		Time.timeScale = 20;

		yield return new WaitForEndOfFrame();
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

	private static IEnumerator DestroyAllObjectsInScene()
	{
		foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
		{
			if (o.name != "Code-based tests runner")
				Object.Destroy(o);
		}

		// wait a frame so the objects are really destroyed
		yield return null;
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
		Assert.AreEqual(State.GameOver, GameOverReason.StillPlaying);
	}

	[UnityTest]
	public IEnumerator GameOverAfterSomeTime()
	{
		yield return Setup();
		yield return new WaitForSeconds(1);
		Assert.AreEqual(State.GameOver, GameOverReason.Happiness);
	}
}
