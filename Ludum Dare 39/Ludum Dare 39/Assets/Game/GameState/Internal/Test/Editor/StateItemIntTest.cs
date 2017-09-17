namespace GameOfLife.GameState.Internal
{
	using NUnit.Framework;

	public class StateItemIntTest
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
			TestItem = new StateItemInt(TestConstants, (d) => d.Money);
		}

		[Test]
		public void IsGameOverBecauseOfThis_False()
		{
			CreateTestConstants();
			CreateTestItem();

			Assert.False(TestItem.IsGameOverBecauseOfThis());
		}

		[Test]
		public void DifferenceHasZeroEffect_True()
		{
			CreateTestConstants();
			CreateTestItem();

			Assert.True(TestItem.DifferenceHasZeroEffect(new StatsDifference { Money = 0 }));
		}

		[Test]
		public void DifferenceHasZeroEffect_False()
		{
			CreateTestConstants();
			CreateTestItem();

			Assert.False(TestItem.DifferenceHasZeroEffect(new StatsDifference { Money = 10 }));
		}

		[Test]
		public void ApplyDifference()
		{
			CreateTestConstants();
			CreateTestItem();

			TestItem.ApplyDifferenceByAction(new StatsDifference { Money = 10 }, new PlayerAction());

			Assert.AreEqual(10, TestItem.GetValue<int>());
		}

		[Test]
		public void ApplyDifferenceWithMultiplierGivesTheSameResult()
		{
			CreateTestConstants();
			CreateTestItem();

			TestItem.ApplyDifferenceByAction(new StatsDifference { Money = 10 }, new PlayerAction(), 2);

			Assert.AreEqual(10, TestItem.GetValue<int>());
		}

		[Test]
		public void ApplyDifferenceByTimeDoesNotChangeValue()
		{
			CreateTestConstants();
			TestConstants.ChangePerMinute = new StatsDifference { Money = 10 };
			CreateTestItem();

			TestItem.ApplyDifferenceByTime(60);

			Assert.AreEqual(0, TestItem.GetValue<int>());
		}
	}
}
