namespace GameOfLife.GameLogic.GameStateItem.Internal
{
    // TODO decouple
    using GameStateAction.Internal;
    using NUnit.Framework;

	public class StateItemIntTest
	{
        // TODO rename & decouple
        private const string ItemName = "Money";

		private StateItem TestItem;
        private int InitialValue;

        private void Setup()
		{
            InitialValue = 0;
		}

		private void CreateTestItem()
		{
			TestItem = new StateItemInt(ItemName, InitialValue);
        }

        // TODO remove
        private StatsDifference NewDifferenceWithMoneyValue(int value)
        {
            var result = new StatsDifference();
            result.SetMoney(value);
            return result;
        }

        [Test]
        public void InitialValueIsSet()
        {
            Setup();
            InitialValue = 123;
            CreateTestItem();

            Assert.AreEqual(123, TestItem.GetValue<int>());
        }

        [Test]
		public void IsGameOverBecauseOfThis_False()
		{
			Setup();
			CreateTestItem();

			Assert.False(TestItem.IsGameOverBecauseOfThis());
		}

		[Test]
		public void DifferenceHasZeroEffect_True()
		{
			Setup();
			CreateTestItem();

			Assert.True(TestItem.DifferenceHasZeroEffect(NewDifferenceWithMoneyValue(0)));
		}

		[Test]
		public void DifferenceHasZeroEffect_False()
		{
			Setup();
			CreateTestItem();

			Assert.False(TestItem.DifferenceHasZeroEffect(NewDifferenceWithMoneyValue(10)));
		}

		[Test]
		public void ApplyDifference()
		{
			Setup();
			CreateTestItem();

			TestItem.ApplyDifferenceByAction(NewDifferenceWithMoneyValue(10), new StateActionImpl());

			Assert.AreEqual(10, TestItem.GetValue<int>());
		}

		[Test]
		public void ApplyDifferenceWithMultiplierGivesTheSameResult()
		{
			Setup();
			CreateTestItem();

			TestItem.ApplyDifferenceByAction(NewDifferenceWithMoneyValue(10), new StateActionImpl(), 2);

			Assert.AreEqual(10, TestItem.GetValue<int>());
		}
	}
}
