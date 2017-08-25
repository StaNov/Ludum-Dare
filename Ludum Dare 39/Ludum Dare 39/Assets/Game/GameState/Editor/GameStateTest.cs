using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class GameStateTest
{
	private GameState State;

	public void Setup()
	{
		State = new GameObject().AddComponent<GameState>();
	}

	[Test]
	public void NewEditModeTestSimplePasses()
	{
		Setup();
		Assert.NotNull(State);
	}
}
