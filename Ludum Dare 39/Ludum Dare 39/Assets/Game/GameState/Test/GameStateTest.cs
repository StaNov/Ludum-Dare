using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateTest
{
	private GameState State;

	public IEnumerator Setup()
	{
		yield return DestroyAllObjectsInScene();

		State = new GameObject().AddComponent<GameState>();
		State.Constants = ScriptableObject.CreateInstance<GameplayConstants>();
		State.Constants.InitialValues = new StatsDifference
		{
			Age = 40,
			Money = 12345
		};
		State.Constants.ChangePerMinute = new StatsDifference
		{
			Age = 11
		};

		yield return null;
	}

	private static IEnumerator DestroyAllObjectsInScene()
	{
		foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
		{
			Object.Destroy(o);
		}

		yield return null;
	}

	[UnityTest]
	public IEnumerator StateIsInitializedAfterSceneLoad()
	{
		yield return Setup();
		Assert.AreEqual(State.Age, 40, 0.01f);
		Assert.AreEqual(State.Money, 12345);
	}
}
