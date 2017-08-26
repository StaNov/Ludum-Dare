using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameStateTest
{
	private GameState State;

	public IEnumerator Setup()
	{
		SceneManager.LoadScene("GameStateTestScene", LoadSceneMode.Additive);
		yield return null;
		State = GameObject.Find("GameObject").GetComponent<GameState>();
	}

	[UnityTest]
	public IEnumerator StateIsNotNullAfterTestSceneIsLoaded()
	{
		yield return Setup();
		Assert.NotNull(State);
	}
}
