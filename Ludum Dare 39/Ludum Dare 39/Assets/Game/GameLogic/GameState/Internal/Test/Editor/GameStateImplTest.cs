namespace GameOfLife.GameLogic.GameState.Internal
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using GameStateItem;
    using GameOfLife.GameLogic.GameStateAction;
    using System;

    public class GameStateImplTest
    {
        private const string TestItemName = "testItem";
        private const string TestActionName = "testAction";
		private const string TestActionNameWork = "testActionWork";

		private IGameState _testGameState;
		private List<StateItem> _testItems;
        private List<StateAction> _testActions;

        private class TestItem : StateItem
        {
            public string Name = TestItemName;
            public bool GameOverBecauseOfThis = false;
            public bool UpdateIfFamilyNotActiveLocal = true;
            public int ApplyDifferenceByActionCalled { get; private set; }
            public bool ApplyDifferenceByTimeCalled { get; private set; }

            public virtual void ApplyDifferenceByAction(StatsDifference difference, StateAction action, float multiplier = 1) { ApplyDifferenceByActionCalled++; }
                    
            public virtual void ApplyDifferenceByTime(float deltaTime) { ApplyDifferenceByTimeCalled = true; }
                    
            public virtual bool DifferenceHasZeroEffect(StatsDifference difference) { return true; }
                    
            public virtual string GetName() { return Name; }
                    
            public virtual T GetValue<T>() { return default(T); }
                    
            public virtual bool IsGameOverBecauseOfThis() { return GameOverBecauseOfThis; }
                    
            public virtual bool UpdateIfFamilyNotActive() { return UpdateIfFamilyNotActiveLocal; }
        }

        private class TestAction : StateAction
        {
            public string Name = TestActionName;
            public int DurationInSeconds = 10;
            public StatsDifference EffectBefore = new StatsDifference();
            public StatsDifference EffectDuring = new StatsDifference();
            public StatsDifference EffectAfter = new StatsDifference();
            public bool ForBoth = false;
            public bool PartnersAction = false;
            public bool WorkAction = false; // TODO add test for work action

            public int GetDurationInSeconds()
            {
                return DurationInSeconds;
            }

            public StatsDifference GetEffectAfter()
            {
                return EffectAfter;
            }

            public StatsDifference GetEffectBefore()
            {
                return EffectBefore;
            }

            public StatsDifference GetEffectDuring()
            {
                return EffectDuring;
            }

            public string GetName()
            {
                return Name;
            }

            public bool IsForBoth()
            {
                return ForBoth;
            }

            public bool IsPartnersAction()
            {
                return PartnersAction;
            }

            public bool IsWorkAction()
            {
                return WorkAction;
            }
        }

        private void CreateTestInputs()
		{
			_testItems = new List<StateItem>();
            _testActions = new List<StateAction>();
		}

		private void CreateTestGameState()
		{
			_testGameState = new GameStateImpl(_testItems, _testActions);
		}

		[Test]
		public void ItemIsGettableFromStateAfterCreation()
		{
			CreateTestInputs();
			_testItems.Add(new TestItem { Name = "test"});
            CreateTestGameState();

			Assert.AreEqual(default(int), _testGameState.GetStateItemValue<int>("test"));
        }

        [Test]
        public void NoPlayerActionIsSetAfterCreation()
        {
            CreateTestInputs();
            CreateTestGameState();

            Assert.IsNull(_testGameState.GetCurrentPlayerAction());
        }

        [Test]
        public void NoPartnerActionIsSetAfterCreation()
        {
            CreateTestInputs();
            CreateTestGameState();

            Assert.IsNull(_testGameState.GetCurrentPartnerAction());
        }

        [Test]
        public void PlayerActionIsSetAfterRunningIt()
        {
            CreateTestInputs();
            _testActions.Add(new TestAction());
            CreateTestGameState();

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(TestActionName, _testGameState.GetCurrentPlayerAction().Value.Key);
        }

        [Test]
        public void PartnerActionIsSetAfterRunningIt()
        {
            CreateTestInputs();
            _testActions.Add(new TestAction { PartnersAction = true });
            CreateTestGameState();

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(TestActionName, _testGameState.GetCurrentPartnerAction().Value.Key);
        }

        [Test]
        public void GameOver_Null()
        {
            CreateTestInputs();
            _testItems.Add(new TestItem());
            CreateTestGameState();

            Assert.IsNull(_testGameState.GameOver);
        }

        [Test]
        public void GameOver_NotNull()
        {
            CreateTestInputs();
            _testItems.Add(new TestItem { GameOverBecauseOfThis = true });
            CreateTestGameState();

            Assert.AreEqual(TestItemName, _testGameState.GameOver);
        }

        [Test]
        public void ApplyDifferenceByTime_Called_PlayerItem()
        {
            var testItem = new TestItem();

            CreateTestInputs();
            _testItems.Add(testItem);
            _testActions.Add(new TestAction());
            CreateTestGameState();
            
            _testGameState.ApplyTime(1);

            Assert.IsTrue(testItem.ApplyDifferenceByTimeCalled);
        }

        [Test]
        public void ApplyDifferenceByTime_NotCalled_FamilyItem()
        {
            var testItem = new TestItem { UpdateIfFamilyNotActiveLocal = false };

            CreateTestInputs();
            _testItems.Add(testItem);
            _testActions.Add(new TestAction());
            CreateTestGameState();

            _testGameState.ApplyTime(1);

            Assert.IsFalse(testItem.ApplyDifferenceByTimeCalled);
        }

        [Test]
        public void ApplyDifferenceByTime_Called_FamilyItem_FamilyStarted()
        {
            var testItem = new TestItem { UpdateIfFamilyNotActiveLocal = false };

            CreateTestInputs();
            _testItems.Add(testItem);
            _testActions.Add(new TestAction());
            CreateTestGameState();

            _testGameState.StartFamily();
            _testGameState.ApplyTime(1);

            Assert.IsTrue(testItem.ApplyDifferenceByTimeCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_Called_PlayerItem()
        {
            var testItem = new TestItem();

            CreateTestInputs();
            _testItems.Add(testItem);
            _testActions.Add(new TestAction());
            CreateTestGameState();

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(1, testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_NotCalled_FamilyItem_FamilyNotStarted()
        {
            var testItem = new TestItem { UpdateIfFamilyNotActiveLocal = false };

            CreateTestInputs();
            _testItems.Add(testItem);
            _testActions.Add(new TestAction());
            CreateTestGameState();

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(0, testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_Called_FamilyItem_FamilyStarted()
        {
            var testItem = new TestItem { UpdateIfFamilyNotActiveLocal = false };

            CreateTestInputs();
            _testItems.Add(testItem);
            _testActions.Add(new TestAction());
            CreateTestGameState();

            _testGameState.StartFamily();
            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(1, testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_CalledThreeTimesAfterActionIsFinished() // before action, by action time, after action
        {
            var testItem = new TestItem();

            CreateTestInputs();
            _testItems.Add(testItem);
            _testActions.Add(new TestAction { DurationInSeconds = 1 });
            CreateTestGameState();
            
            _testGameState.RunAction(TestActionName);
            _testGameState.ApplyTime(10);

            Assert.AreEqual(3, testItem.ApplyDifferenceByActionCalled);
        }

        // TODO maybe insert the test item and test action in setup by default
    }
}
