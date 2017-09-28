namespace GameOfLife.GameLogic.GameState.Internal
{
	using NUnit.Framework;
    using System.Collections.Generic;
    using GameStateItem;
    using System;
    
	public class GameStateImplTest
	{
		private const PlayerActionType TestActionType = PlayerActionType.Eat;
		private const PlayerActionType TestActionTypeWork = PlayerActionType.GoToWork;

		private IGameState _testGameState;
		private List<StateItem> _testItems;
        private List<PlayerAction> _testActions;

        private class TestItem : StateItem
        {
            public string Name;
            public bool GameOverBecauseOfThis;
            public bool UpdateIfFamilyNotActiveLocal = true;
            public int ApplyDifferenceByActionCalled { get; private set; }
            public bool ApplyDifferenceByTimeCalled { get; private set; }

            public virtual void ApplyDifferenceByAction(StatsDifference difference, PlayerAction action, float multiplier = 1) { ApplyDifferenceByActionCalled++; }
                    
            public virtual void ApplyDifferenceByTime(float deltaTime) { ApplyDifferenceByTimeCalled = true; }
                    
            public virtual bool DifferenceHasZeroEffect(StatsDifference difference) { return true; }
                    
            public virtual string GetName() { return Name; }
                    
            public virtual T GetValue<T>() { return default(T); }
                    
            public virtual bool IsGameOverBecauseOfThis() { return GameOverBecauseOfThis; }
                    
            public virtual bool UpdateIfFamilyNotActive() { return UpdateIfFamilyNotActiveLocal; }
        }

        private class TestAction : PlayerAction
        {

        }

		private void CreateTestInputs()
		{
			_testItems = new List<StateItem>();
            _testActions = new List<PlayerAction>();
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
            PlayerActionType playerAction = PlayerActionType.DoSport;

            CreateTestInputs();
            // TODO remove dependency on PlayerActionType
            _testActions.Add(new TestAction { Type = playerAction });
            CreateTestGameState();

            _testGameState.RunAction(playerAction.ToString());

            Assert.AreEqual(playerAction.ToString(), _testGameState.GetCurrentPlayerAction().Value.Key);
        }

        [Test]
        public void PartnerActionIsSetAfterRunningIt()
        {
            PlayerActionType partnerAction = PlayerActionType.PartnerFeedsFamily;

            CreateTestInputs();
            // TODO remove dependency on PlayerActionType, redo to string
            _testActions.Add(new TestAction { Type = partnerAction });
            CreateTestGameState();

            _testGameState.RunAction(partnerAction.ToString());

            Assert.AreEqual(partnerAction.ToString(), _testGameState.GetCurrentPartnerAction().Value.Key);
        }

        [Test]
        public void GameOver_Null()
        {
            CreateTestInputs();
            _testItems.Add(new TestItem { Name = "test" });
            CreateTestGameState();

            Assert.IsNull(_testGameState.GameOver);
        }

        [Test]
        public void GameOver_NotNull()
        {
            CreateTestInputs();
            _testItems.Add(new TestItem { Name = "test", GameOverBecauseOfThis = true });
            CreateTestGameState();

            Assert.AreEqual("test", _testGameState.GameOver);
        }

        [Test]
        public void ApplyDifferenceByTime_Called_PlayerItem()
        {
            var testItem = new TestItem { Name = "test" };
            var testActionType = PlayerActionType.DoHobby;

            CreateTestInputs();
            _testItems.Add(testItem);
            // TODO remove dependency on PlayerActionType, redo to string
            _testActions.Add(new TestAction { Type = testActionType });
            CreateTestGameState();
            
            _testGameState.ApplyTime(1);

            Assert.IsTrue(testItem.ApplyDifferenceByTimeCalled);
        }

        [Test]
        public void ApplyDifferenceByTime_NotCalled_FamilyItem()
        {
            var testItem = new TestItem { Name = "test", UpdateIfFamilyNotActiveLocal = false };
            var testActionType = PlayerActionType.DoHobby;

            CreateTestInputs();
            _testItems.Add(testItem);
            // TODO remove dependency on PlayerActionType, redo to string
            _testActions.Add(new TestAction { Type = testActionType });
            CreateTestGameState();

            _testGameState.ApplyTime(1);

            Assert.IsFalse(testItem.ApplyDifferenceByTimeCalled);
        }

        [Test]
        public void ApplyDifferenceByTime_Called_FamilyItem_FamilyStarted()
        {
            var testItem = new TestItem { Name = "test", UpdateIfFamilyNotActiveLocal = false };
            var testActionType = PlayerActionType.DoHobby;

            CreateTestInputs();
            _testItems.Add(testItem);
            // TODO remove dependency on PlayerActionType, redo to string
            _testActions.Add(new TestAction { Type = testActionType });
            CreateTestGameState();

            _testGameState.StartFamily();
            _testGameState.ApplyTime(1);

            Assert.IsTrue(testItem.ApplyDifferenceByTimeCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_Called_PlayerItem()
        {
            var testItem = new TestItem { Name = "test" };
            var testActionType = PlayerActionType.DoHobby;

            CreateTestInputs();
            _testItems.Add(testItem);
            // TODO remove dependency on PlayerActionType, redo to string
            _testActions.Add(new TestAction { Type = testActionType });
            CreateTestGameState();

            _testGameState.RunAction(testActionType.ToString());

            Assert.AreEqual(1, testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_NotCalled_FamilyItem_FamilyNotStarted()
        {
            var testItem = new TestItem { Name = "test", UpdateIfFamilyNotActiveLocal = false };
            var testActionType = PlayerActionType.DoHobby;

            CreateTestInputs();
            _testItems.Add(testItem);
            // TODO remove dependency on PlayerActionType, redo to string
            _testActions.Add(new TestAction { Type = testActionType });
            CreateTestGameState();

            _testGameState.RunAction(testActionType.ToString());

            Assert.AreEqual(0, testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_Called_FamilyItem_FamilyStarted()
        {
            var testItem = new TestItem { Name = "test", UpdateIfFamilyNotActiveLocal = false };
            var testActionType = PlayerActionType.DoHobby;

            CreateTestInputs();
            _testItems.Add(testItem);
            // TODO remove dependency on PlayerActionType, redo to string
            _testActions.Add(new TestAction { Type = testActionType });
            CreateTestGameState();

            _testGameState.StartFamily();
            _testGameState.RunAction(testActionType.ToString());

            Assert.AreEqual(1, testItem.ApplyDifferenceByActionCalled);
        }

        [Test]
        public void ApplyDifferenceByAction_CalledThreeTimesAfterActionIsFinished() // before action, by action time, after action
        {
            var testItem = new TestItem { Name = "test" };
            var testActionType = PlayerActionType.DoHobby;

            CreateTestInputs();
            _testItems.Add(testItem);
            // TODO remove dependency on PlayerActionType, redo to string
            _testActions.Add(new TestAction { Type = testActionType, DurationInSeconds = 1 });
            CreateTestGameState();
            
            _testGameState.RunAction(testActionType.ToString());
            _testGameState.ApplyTime(10);

            Assert.AreEqual(3, testItem.ApplyDifferenceByActionCalled);
        }

        // TODO maybe insert the test item and test action in setup by default
    }
}
