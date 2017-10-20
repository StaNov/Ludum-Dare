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
        private const string TestItemName2 = "testItem2";
        private const string TestActionName = "testAction";
        private const string TestActionName2 = "testAction2";
        private const string TestActionNameWork = "testActionWork";

		private IGameState _testGameState;
		private List<StateItem> _testItems;
        private List<StateAction> _testActions;
        private TestItem _testItem;
        private TestItem _testItem2;
        private TestAction _testAction;
        private TestAction _testAction2;

        private class TestItem : StateItem
        {
            public string Name;
            public bool GameOverBecauseOfThis = false;
            public bool UpdateIfFamilyNotActiveLocal = true;
            public int ApplyDifferenceByActionCalled { get; private set; }
            public bool ApplyDifferenceByTimeCalled { get; private set; }

            public TestItem(string name)
            {
                Name = name;
            }

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
            public string Name;
            public int DurationInSeconds = 10;
            public StatsDifference EffectBefore = new StatsDifference();
            public StatsDifference EffectDuring = new StatsDifference();
            public StatsDifference EffectAfter = new StatsDifference();
            public bool ForBoth = false;
            public bool PartnersAction = false;
            public bool WorkAction = false; // TODO add test for work action

            public TestAction(string name)
            {
                Name = name;
            }

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

		private void Setup()
        {
            _testItem = new TestItem(TestItemName);
            _testItem2 = new TestItem(TestItemName2);
            _testAction = new TestAction(TestActionName);
            _testAction2 = new TestAction(TestActionName2);

            _testItems = new List<StateItem>();
            _testActions = new List<StateAction>();

            _testItems.Add(_testItem);
            _testItems.Add(_testItem2);
            _testActions.Add(_testAction);
            _testActions.Add(_testAction2);

            _testGameState = new GameStateImpl(_testItems, _testActions);
		}

		[Test]
		public void ItemIsGettableFromStateAfterCreation()
		{
            Setup();

			Assert.AreEqual(default(int), _testGameState.GetStateItemValue<int>(TestItemName));
        }

        [Test]
        public void NoPlayerActionIsSetAfterCreation()
        {
            Setup();

            Assert.IsNull(_testGameState.GetCurrentPlayerAction());
        }

        [Test]
        public void NoPartnerActionIsSetAfterCreation()
        {
            Setup();

            Assert.IsNull(_testGameState.GetCurrentPartnerAction());
        }

        [Test]
        public void ActionIsSetAfterRunningIt_Player()
        {
            Setup();

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(TestActionName, _testGameState.GetCurrentPlayerAction().Value.Key);
        }

        [Test]
        public void ActionIsSetAfterRunningIt_Partner()
        {
            Setup();
            _testAction.PartnersAction = true;

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(TestActionName, _testGameState.GetCurrentPartnerAction().Value.Key);
        }

        [Test]
        public void ActionIsSetAfterRunningIt_Both()
        {
            Setup();
            _testAction.ForBoth = true;

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(TestActionName, _testGameState.GetCurrentPlayerAction().Value.Key);
            Assert.AreEqual(TestActionName, _testGameState.GetCurrentPartnerAction().Value.Key);
        }

        [Test]
        public void GameOver_Null()
        {
            Setup();

            Assert.IsNull(_testGameState.GameOver);
        }

        [Test]
        public void GameOver_NotNull()
        {
            Setup();
            _testItem.GameOverBecauseOfThis = true;

            Assert.AreEqual(TestItemName, _testGameState.GameOver);
        }

        [Test]
        public void ApplyDifferenceByTime_Called_PlayerItem()
        {
            Setup();
            
            _testGameState.ApplyTime(1);

            Assert.IsTrue(_testItem.ApplyDifferenceByTimeCalled);
        }

        [Test]
        public void ApplyDifferenceByTime_NotCalled_FamilyItem()
        {
            Setup();
            _testItem.UpdateIfFamilyNotActiveLocal = false;

            _testGameState.ApplyTime(1);

            Assert.IsFalse(_testItem.ApplyDifferenceByTimeCalled);
        }

        [Test]
        public void ApplyDifferenceByTime_Called_FamilyItem_FamilyStarted()
        {
            Setup();
            _testItem.UpdateIfFamilyNotActiveLocal = false;

            _testGameState.StartFamily();
            _testGameState.ApplyTime(1);

            Assert.IsTrue(_testItem.ApplyDifferenceByTimeCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_Called_PlayerItem()
        {
            Setup();

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(1, _testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_NotCalled_FamilyItem_FamilyNotStarted()
        {
            Setup();
            _testItem.UpdateIfFamilyNotActiveLocal = false;

            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(0, _testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_Called_FamilyItem_FamilyStarted()
        {
            Setup();
            _testItem.UpdateIfFamilyNotActiveLocal = false;

            _testGameState.StartFamily();
            _testGameState.RunAction(TestActionName);

            Assert.AreEqual(1, _testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_CalledThreeTimesAfterActionIsFinished() // before action, by action time, after action
        {
            Setup();
            _testAction.DurationInSeconds = 1;
            
            _testGameState.RunAction(TestActionName);
            _testGameState.ApplyTime(10);

            Assert.AreEqual(3, _testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ActionIsReplacedWhenOneAlreadyRuns_Player()
        {
            ActionIsReplacedWhenOneAlreadyRuns(false);
        }

        [Test]
        public void ActionIsReplacedWhenOneAlreadyRuns_Partner()
        {
            ActionIsReplacedWhenOneAlreadyRuns(true);
        }
        
        private void ActionIsReplacedWhenOneAlreadyRuns(bool partner)
        {
            Setup();
            _testAction.PartnersAction = partner;
            _testAction2.PartnersAction = partner;

            _testGameState.RunAction(TestActionName);
            _testGameState.ApplyTime(1);
            _testGameState.RunAction(TestActionName2);

            var actualAction = partner ? _testGameState.GetCurrentPartnerAction() : _testGameState.GetCurrentPlayerAction();
            Assert.AreEqual(TestActionName2, actualAction.Value.Key);
        }

        [Test]
        public void ActionForBothIsReplaced_Player()
        {
            ActionForBothIsReplaced(false);
        }

        [Test]
        public void ActionForBothIsReplaced_Partner()
        {
            ActionForBothIsReplaced(true);
        }
        
        private void ActionForBothIsReplaced(bool partner)
        {
            Setup();

            _testAction.ForBoth = true;
            _testAction2.PartnersAction = partner;

            _testGameState.RunAction(TestActionName);
            _testGameState.ApplyTime(1);
            _testGameState.RunAction(TestActionName2);

            var actualAction = partner ? _testGameState.GetCurrentPartnerAction() : _testGameState.GetCurrentPlayerAction();
            var otherPersonAction = !partner ? _testGameState.GetCurrentPartnerAction() : _testGameState.GetCurrentPlayerAction();

            Assert.AreEqual(TestActionName2, actualAction.Value.Key);
            Assert.IsNull(otherPersonAction);
        }

        // TODO redesign "Action for both" and "partners action" to enum ActionType - players, partners, for both
    }
}
