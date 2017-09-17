namespace GameOfLife.GameState.Internal
{
	using NUnit.Framework;

	public class StateItemFloatTest
	{

		private GameplayConstants TestConstants;
		private StateItem TestItem;

		private const float MinValue = 0;
		private const float MaxValue = 100;

		private void CreateTestConstants()
		{
			TestConstants = GameplayConstants.CreateEmptyConstants();
		}

		private void CreateTestItem()
		{
			TestItem = new StateItemFloat(MinValue, MaxValue, TestConstants, (d) => d.MyFood, true);
		}

		[Test]
		public void IsGameOverBecauseOfThis_False()
		{
			CreateTestConstants();
			TestConstants.InitialValues.MyFood = 10;
			CreateTestItem();

			Assert.False(TestItem.IsGameOverBecauseOfThis());
		}

		[Test]
		public void IsGameOverBecauseOfThis_True()
		{
			CreateTestConstants();
			TestConstants.InitialValues.MyFood = 0;
			CreateTestItem();

			Assert.True(TestItem.IsGameOverBecauseOfThis());
		}

		[Test]
		public void DifferenceHasZeroEffect_True()
		{
			CreateTestConstants();
			CreateTestItem();

			Assert.True(TestItem.DifferenceHasZeroEffect(new StatsDifference { MyFood = 0 }));
		}

		[Test]
		public void DifferenceHasZeroEffect_False()
		{
			CreateTestConstants();
			CreateTestItem();

			Assert.False(TestItem.DifferenceHasZeroEffect(new StatsDifference { MyFood = 10 }));
		}

		[Test]
		public void ApplyDifference()
		{
			CreateTestConstants();
			CreateTestItem();

			TestItem.ApplyDifferenceByAction(new StatsDifference { MyFood = 10 }, new PlayerAction());

			Assert.AreEqual(10, TestItem.GetValue<float>());
		}

		[Test]
		public void ApplyDifferenceMultiplier()
		{
			CreateTestConstants();
			CreateTestItem();

			TestItem.ApplyDifferenceByAction(new StatsDifference { MyFood = 10 }, new PlayerAction(), 2);

			Assert.AreEqual(20, TestItem.GetValue<float>());
		}

		[Test]
		public void ApplyDifferenceByTime()
		{
			CreateTestConstants();
			TestConstants.ChangePerMinute = new StatsDifference { MyFood = 10 };
			CreateTestItem();

			TestItem.ApplyDifferenceByTime(60);

			Assert.AreEqual(10, TestItem.GetValue<float>());
		}

		[Test]
		public void ValueTopClampedProperly_AfterCreation()
		{
			CreateTestConstants();
			TestConstants.InitialValues.MyFood = 123;
			CreateTestItem();

			Assert.AreEqual(MaxValue, TestItem.GetValue<float>());
		}

		[Test]
		public void ValueBottomClampedProperly_AfterCreation()
		{
			CreateTestConstants();
			TestConstants.InitialValues.MyFood = -99;
			CreateTestItem();

			Assert.AreEqual(MinValue, TestItem.GetValue<float>());
		}

		[Test]
		public void ValueTopClampedProperly_AfterSomeTime()
		{
			CreateTestConstants();
			TestConstants.InitialValues.MyFood = 99;
			TestConstants.ChangePerMinute.MyFood = 10 * 60;
			CreateTestItem();

			TestItem.ApplyDifferenceByTime(1);
			Assert.AreEqual(MaxValue, TestItem.GetValue<float>());
		}

		[Test]
		public void ValueBottomClampedProperly_AfterSomeTime()
		{
			CreateTestConstants();
			TestConstants.InitialValues.MyFood = 1;
			TestConstants.ChangePerMinute.MyFood = -10 * 60;
			CreateTestItem();

			TestItem.ApplyDifferenceByTime(1);
			Assert.AreEqual(MinValue, TestItem.GetValue<float>());
		}

		[Test]
		public void ValueTopClampedProperly_ByFunction()
		{
			CreateTestConstants();
			TestConstants.InitialValues.MyFood = 1234;

			// TODO can be done cleaner, create two items as fields?
			TestItem = new StateItemFloat(MinValue, () => MaxValue, TestConstants, (d) => d.MyFood, true);

			Assert.AreEqual(MaxValue, TestItem.GetValue<float>());
		}

	}
}
