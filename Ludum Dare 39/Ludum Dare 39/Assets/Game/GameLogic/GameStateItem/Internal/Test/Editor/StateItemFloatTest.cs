namespace GameOfLife.GameLogic.GameStateItem.Internal
{
    using NUnit.Framework;

	public class StateItemFloatTest
	{
		private StateItem TestItem;

		private const float MinValue = 0;
		private const float MaxValue = 100;

        // TODO rename to decouple
        private const string TestName = "MyFood";

        private float InitialValue;
        private float ChangePerMinute;

        private void Setup()
		{
            InitialValue = 0;
            ChangePerMinute = 0;
        }

		private void CreateTestItem()
		{
			TestItem = new StateItemFloat(TestName, MinValue, MaxValue, InitialValue, ChangePerMinute, true);
		}

        // TODO remove
        private StatsDifference NewDifferenceWithMyFoodValue(int value)
        {
            var result = new StatsDifference();
            result.SetMyFood(value);
            return result;
        }

		[Test]
		public void IsGameOverBecauseOfThis_False()
		{
			Setup();
            InitialValue = 10;
			CreateTestItem();

			Assert.False(TestItem.IsGameOverBecauseOfThis());
		}

		[Test]
		public void IsGameOverBecauseOfThis_True()
		{
			Setup();
            InitialValue = 0;
			CreateTestItem();

			Assert.True(TestItem.IsGameOverBecauseOfThis());
		}

		[Test]
		public void DifferenceHasZeroEffect_True()
		{
			Setup();
			CreateTestItem();

			Assert.True(TestItem.DifferenceHasZeroEffect(NewDifferenceWithMyFoodValue(0)));
		}

		[Test]
		public void DifferenceHasZeroEffect_False()
		{
			Setup();
			CreateTestItem();

			Assert.False(TestItem.DifferenceHasZeroEffect(NewDifferenceWithMyFoodValue(10)));
		}

		[Test]
		public void ApplyDifference()
		{
			Setup();
			CreateTestItem();

			TestItem.ApplyDifferenceByAction(NewDifferenceWithMyFoodValue(10), new PlayerAction());

			Assert.AreEqual(10, TestItem.GetValue<float>());
		}

		[Test]
		public void ApplyDifferenceMultiplier()
		{
			Setup();
			CreateTestItem();

			TestItem.ApplyDifferenceByAction(NewDifferenceWithMyFoodValue(10), new PlayerAction(), 2);

			Assert.AreEqual(20, TestItem.GetValue<float>());
		}

		[Test]
		public void ApplyDifferenceByTime()
		{
			Setup();
			ChangePerMinute = 10;
			CreateTestItem();

			TestItem.ApplyDifferenceByTime(60);

			Assert.AreEqual(10, TestItem.GetValue<float>());
		}

		[Test]
		public void ValueTopClampedProperly_AfterCreation()
		{
			Setup();
			InitialValue = 123;
			CreateTestItem();

			Assert.AreEqual(MaxValue, TestItem.GetValue<float>());
		}

		[Test]
		public void ValueBottomClampedProperly_AfterCreation()
		{
			Setup();
			InitialValue = -99;
			CreateTestItem();

			Assert.AreEqual(MinValue, TestItem.GetValue<float>());
		}

		[Test]
		public void ValueTopClampedProperly_AfterSomeTime()
		{
			Setup();
			InitialValue = 99;
			ChangePerMinute = 10 * 60;
			CreateTestItem();

			TestItem.ApplyDifferenceByTime(1);
			Assert.AreEqual(MaxValue, TestItem.GetValue<float>());
		}

		[Test]
		public void ValueBottomClampedProperly_AfterSomeTime()
		{
			Setup();
			InitialValue = 1;
			ChangePerMinute = -10 * 60;
			CreateTestItem();

			TestItem.ApplyDifferenceByTime(1);
			Assert.AreEqual(MinValue, TestItem.GetValue<float>());
		}

		[Test]
		public void ValueTopClampedProperly_ByFunction()
		{
			Setup();
			InitialValue = 1234;

			// TODO can be done cleaner, create two items as fields? // maybe outdated comment
			TestItem = new StateItemFloat(TestName, MinValue, () => MaxValue, InitialValue, ChangePerMinute, true);

			Assert.AreEqual(MaxValue, TestItem.GetValue<float>());
		}

	}
}
