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
        private TestItem _testItem;
        private TestAction _testAction;

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

		private void CreateTestGameState()
        {
            _testItem = new TestItem();
            _testAction = new TestAction();

            _testItems = new List<StateItem>();
            _testActions = new List<StateAction>();

            _testItems.Add(_testItem);
            _testActions.Add(_testAction);

            _testGameState = new GameStateImpl(_testItems, _testActions);
		}

		[Test]
		public void ItemIsGettableFromStateAfterCreation()
		{
            CreateTestGameState();

			Assert.AreEqual(default(int), _testGameState.GetStateItemValue<int>(TestItemName));
        }

        [Test]
        public void NoPlayerActionIsSetAfterCreation()
        {
            CreateTestGameState();

            Assert.IsNull(_testGameState.GetCurrentPlayerAction());
        }

        [Test]
        public void NoPartnerActionIsSetAfterCreation()
        {
            CreateTestGameState();

            Assert.IsNull(_testGameState.GetCurrentPartnerAction());
        }

        [Test]
        public void PlayerActionIsSetAfterRunningIt()
        {
            CreateTestGameState();

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(TestActionName, _testGameState.GetCurrentPlayerAction().Value.Key);
        }

        [Test]
        public void PartnerActionIsSetAfterRunningIt()
        {
            CreateTestGameState();
            _testAction.PartnersAction = true;

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(TestActionName, _testGameState.GetCurrentPartnerAction().Value.Key);
        }

        [Test]
        public void GameOver_Null()
        {
            CreateTestGameState();

            Assert.IsNull(_testGameState.GameOver);
        }

        [Test]
        public void GameOver_NotNull()
        {
            CreateTestGameState();
            _testItem.GameOverBecauseOfThis = true;

            Assert.AreEqual(TestItemName, _testGameState.GameOver);
        }

        [Test]
        public void ApplyDifferenceByTime_Called_PlayerItem()
        {
            CreateTestGameState();
            
            _testGameState.ApplyTime(1);

            Assert.IsTrue(_testItem.ApplyDifferenceByTimeCalled);
        }

        [Test]
        public void ApplyDifferenceByTime_NotCalled_FamilyItem()
        {
            CreateTestGameState();
            _testItem.UpdateIfFamilyNotActiveLocal = false;

            _testGameState.ApplyTime(1);

            Assert.IsFalse(_testItem.ApplyDifferenceByTimeCalled);
        }

        [Test]
        public void ApplyDifferenceByTime_Called_FamilyItem_FamilyStarted()
        {
            CreateTestGameState();
            _testItem.UpdateIfFamilyNotActiveLocal = false;

            _testGameState.StartFamily();
            _testGameState.ApplyTime(1);

            Assert.IsTrue(_testItem.ApplyDifferenceByTimeCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_Called_PlayerItem()
        {
            CreateTestGameState();

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(1, _testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_NotCalled_FamilyItem_FamilyNotStarted()
        {
            CreateTestGameState();
            _testItem.UpdateIfFamilyNotActiveLocal = false;

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(0, _testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_Called_FamilyItem_FamilyStarted()
        {
            CreateTestGameState();
            _testItem.UpdateIfFamilyNotActiveLocal = false;

            _testGameState.StartFamily();
            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(1, _testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_CalledThreeTimesAfterActionIsFinished() // before action, by action time, after action
        {
            CreateTestGameState();
            _testAction.DurationInSeconds = 1;
            
            _testGameState.RunAction(TestActionName);
            _testGameState.ApplyTime(10);

            Assert.AreEqual(3, _testItem.ApplyDifferenceByActionCalled);
        }

        // TODO maybe insert the test item and test action in setup by default
    }
}
