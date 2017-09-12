using NUnit.Framework;
using UnityEngine;

public class StateItemTest {

	private GameplayConstants TestConstants;

	private void Setup()
	{
		TestConstants = GameplayConstants.CreateEmptyConstants();
	}

	[Test]
	public void NotGameOverAfterInitialization()
	{
		Setup();
		TestConstants.InitialValues.MyFood = 10;

		StateItem item = new StateItemFloat(0, 100, TestConstants, (d) => d.MyFood, () => true);

		Assert.False(item.IsGameOverBecauseOfThis());
	}

}
