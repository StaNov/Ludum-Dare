using NUnit.Framework;
using UnityEngine;

public class StateItemTest {

	[Test]
	public void NotGameOverAfterInitialization()
	{
		GameplayConstants constants = GameplayConstants.CreateEmptyConstants();
		constants.InitialValues.MyFood = 10;

		StateItem item = new StateItemFloat(0, 100, constants, (d) => d.MyFood, () => true);

		Assert.False(item.IsGameOverBecauseOfThis());
	}

}
