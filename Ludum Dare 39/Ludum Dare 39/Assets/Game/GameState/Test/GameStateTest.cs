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

		yield return null;
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
			MyHappiness = 2,
			MyHealth = 30,
			FamilyFood = 10,
			FamilyHappiness = 20,
			FamilyHealth = 30,
			FoodSupplies = 99,
			MyFood = 20
		};
		result.ChangePerMinute = new StatsDifference
		{
			Age = 11111
		};

		return result;
	}

	private static IEnumerator DestroyAllObjectsInScene()
	{
		foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
		{
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
	public IEnumerator AgeIsGreaterAfterCoupleOfFrames()
	{
		yield return Setup();
		yield return new WaitForSeconds(0.5f);
		Assert.Greater(State.Age, InitialAge + 1);
	}
}
