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
            public bool ApplyDifferenceByActionCalled { get; private set; }
            public bool ApplyDifferenceByTimeCalled { get; private set; }

            public virtual void ApplyDifferenceByAction(StatsDifference difference, PlayerAction action, float multiplier = 1) { ApplyDifferenceByActionCalled = true; }
                    
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

            Assert.IsTrue(testItem.ApplyDifferenceByActionCalled);
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

            Assert.IsFalse(testItem.ApplyDifferenceByActionCalled);
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

            Assert.IsTrue(testItem.ApplyDifferenceByActionCalled);
        }

        /*[Test]
		public void AgeIsGreaterAfterSomeTime()
		{
			int initialAge = 10;

			CreateTestInputs();
			_testItems.InitialValues.Age = initialAge;
			_testItems.ChangePerMinute.Age = 11111;
			CreateTestGameState();

			_testGameState.ApplyTime(1);
			Assert.Less(initialAge + 1, _testGameState.GetStateItemValue<float>(StateItemType.Age.ToString()));
		}

		[Test]
		public void NotGameOverAfterInitialization()
		{
			// TODO create specific stateitem maybe? to not duplicate stateitem tests?
			CreateTestInputs();
			CreateTestGameState();

			Assert.IsNull(_testGameState.GameOver);
		}

		[Test]
		public void GameOverBecauseOfMyEnergy()
		{
			int veryLowEnergy = 1;
			int veryQuickEnergyDecrease = -5 * 60;

			CreateTestInputs();
			_testItems.InitialValues.MyEnergy = veryLowEnergy;
			_testItems.ChangePerMinute.MyEnergy = veryQuickEnergyDecrease;
			CreateTestGameState();

			_testGameState.ApplyTime(1);
			Assert.AreEqual(StateItemType.MyEnergy.ToString(), _testGameState.GameOver);
		}

		[Test]
		public void UpdateByActionDuringLowersStat()
		{
			int initialFood = 90;
			int foodDecreaseByAction = 10;
			int actionDuration = 2;

			CreateTestInputs();

			_testItems.InitialValues.MyFood = initialFood;
			_testItems.PlayerActions[0].DurationInSeconds = actionDuration;
			_testItems.PlayerActions[0].EffectDuring.MyFood = -foodDecreaseByAction;

			CreateTestGameState();

			_testGameState.RunAction(TestActionType.ToString());
			_testGameState.ApplyTime(3);
			Assert.AreEqual(initialFood - foodDecreaseByAction, _testGameState.GetStateItemValue<float>(StateItemType.MyFood.ToString()), 0.01);
		}

		[Test]
		public void UpdateByActionBeforeChangesStats()
		{
			int initialEnergy = 90;
			int energyDecreaseByAction = 10;

			CreateTestInputs();

			_testItems.InitialValues.MyEnergy = initialEnergy;
			_testItems.PlayerActions[0].DurationInSeconds = 10;
			_testItems.PlayerActions[0].EffectBefore.MyEnergy = -energyDecreaseByAction;

			CreateTestGameState();

			_testGameState.RunAction(TestActionType.ToString());
			Assert.AreEqual(initialEnergy - energyDecreaseByAction, _testGameState.GetStateItemValue<float>(StateItemType.MyEnergy.ToString()), 0.01);
		}

		[Test]
		public void FamilyFoodGetUpdatedWhenFamilyIsActive_ByTime()
		{
			int initialFamilyFood = 50;
			int familyFoodDecrease = 10 * 60;

			CreateTestInputs();

			_testItems.InitialValues.FamilyFood = initialFamilyFood;
			_testItems.ChangePerMinute.FamilyFood = -familyFoodDecrease;

			CreateTestGameState();

			_testGameState.StartFamily();
			_testGameState.ApplyTime(2);
			Assert.Greater(initialFamilyFood - familyFoodDecrease / 60, _testGameState.GetStateItemValue<float>(StateItemType.FamilyFood.ToString()));
		}

		[Test]
		public void FamilyFoodGetUpdatedWhenFamilyIsActive_ByActionTime()
		{
			int initialFamilyFood = 50;
			int familyFoodDecrease = 10 * 60;
			int effectDuration = 1;
			int slowerDecreaseSoGreaterCanBeUsed = familyFoodDecrease / 2;

			CreateTestInputs();

			_testItems.InitialValues.FamilyFood = initialFamilyFood;
			_testItems.PlayerActions[0].DurationInSeconds = effectDuration;
			_testItems.PlayerActions[0].EffectDuring.FamilyFood = -familyFoodDecrease;

			CreateTestGameState();

			_testGameState.StartFamily();
			_testGameState.RunAction(TestActionType.ToString());
			_testGameState.ApplyTime(effectDuration + 1);
			Assert.Greater(initialFamilyFood - slowerDecreaseSoGreaterCanBeUsed / 60, _testGameState.GetStateItemValue<float>(StateItemType.FamilyFood.ToString()));
		}

		[Test]
		public void FamilyFoodGetUpdatedWhenFamilyIsActive_AfterAction()
		{
			int initialFamilyFood = 50;
			int familyFoodDecrease = 10;
			int effectDuration = 1;

			CreateTestInputs();

			_testItems.InitialValues.FamilyFood = initialFamilyFood;
			_testItems.PlayerActions[0].DurationInSeconds = effectDuration;
			_testItems.PlayerActions[0].EffectAfter.FamilyFood = -familyFoodDecrease;

			CreateTestGameState();

			_testGameState.StartFamily();
			_testGameState.RunAction(TestActionType.ToString());
			_testGameState.ApplyTime(effectDuration + 1);
			Assert.AreEqual(initialFamilyFood - familyFoodDecrease, _testGameState.GetStateItemValue<float>(StateItemType.FamilyFood.ToString()));
		}

		[Test]
		public void FamilyFoodNotGetUpdatedWhenFamilyIsNotActive_ByTime()
		{
			int initialFamilyFood = 50;
			int familyFoodDecrease = 10 * 60;

			CreateTestInputs();

			_testItems.InitialValues.FamilyFood = initialFamilyFood;
			_testItems.ChangePerMinute.FamilyFood = -familyFoodDecrease;

			CreateTestGameState();

			_testGameState.ApplyTime(1);
			Assert.AreEqual(initialFamilyFood, _testGameState.GetStateItemValue<float>(StateItemType.FamilyFood.ToString()));
		}

		[Test]
		public void FamilyFoodNotGetUpdatedWhenFamilyIsNotActive_ByActionTime()
		{
			int initialFamilyFood = 50;
			int familyFoodDecrease = 10 * 60;

			CreateTestInputs();

			_testItems.InitialValues.FamilyFood = initialFamilyFood;
			_testItems.PlayerActions[0].EffectDuring.FamilyFood = -familyFoodDecrease;

			CreateTestGameState();

			_testGameState.RunAction(TestActionType.ToString());
			_testGameState.ApplyTime(1);
			Assert.AreEqual(initialFamilyFood, _testGameState.GetStateItemValue<float>(StateItemType.FamilyFood.ToString()));
		}

		[Test]
		public void FamilyFoodNotGetUpdatedWhenFamilyIsNotActive_AfterAction()
		{
			int initialFamilyFood = 50;
			int familyFoodDecrease = 10;
			int effectDuration = 1;

			CreateTestInputs();

			_testItems.InitialValues.FamilyFood = initialFamilyFood;
			_testItems.PlayerActions[0].DurationInSeconds = effectDuration;
			_testItems.PlayerActions[0].EffectAfter.FamilyFood = -familyFoodDecrease;

			CreateTestGameState();

			_testGameState.RunAction(TestActionType.ToString());
			_testGameState.ApplyTime(effectDuration + 1);
			Assert.AreEqual(initialFamilyFood, _testGameState.GetStateItemValue<float>(StateItemType.FamilyFood.ToString()));
		}

		[Test]
		public void MoneyProperlyInitialized()
		{
			int initialMoney = 50;

			CreateTestInputs();

			_testItems.InitialValues.Money = initialMoney;

			CreateTestGameState();

			Assert.AreEqual(initialMoney, _testGameState.GetStateItemValue<int>(StateItemType.Money.ToString()));
		}

		[Test]
		public void MoneyProperlyAppliedBeforeAction()
		{
			int initialMoney = 50;
			int differenceBeforeAction = -20;

			CreateTestInputs();

			_testItems.InitialValues.Money = initialMoney;
			_testItems.PlayerActions[0].EffectBefore.Money = differenceBeforeAction;

			CreateTestGameState();

			_testGameState.RunAction(TestActionType.ToString());

			Assert.AreEqual(initialMoney + differenceBeforeAction, _testGameState.GetStateItemValue<int>(StateItemType.Money.ToString()));
		}

		[Test]
		public void MoneyProperlyAppliedAfterAction()
		{
			int initialMoney = 50;
			int differenceAfterAction = -20;
			int actionDuration = 1;

			CreateTestInputs();

			_testItems.InitialValues.Money = initialMoney;
			_testItems.PlayerActions[0].DurationInSeconds = actionDuration;
			_testItems.PlayerActions[0].EffectAfter.Money = differenceAfterAction;

			CreateTestGameState();

			_testGameState.RunAction(TestActionType.ToString());

			Assert.AreEqual(initialMoney, _testGameState.GetStateItemValue<int>(StateItemType.Money.ToString()));

			_testGameState.ApplyTime(actionDuration);

			Assert.AreEqual(initialMoney + differenceAfterAction, _testGameState.GetStateItemValue<int>(StateItemType.Money.ToString()));
		}

		[Test]
		public void MoneyAddedAfterWork()
		{
			int initialMoney = 10;
			int initialSalary = 20;
			int salaryIncreaseAfterWorkshift = 30;
			int actionDuration = 1;

			CreateTestInputs();

			_testItems.InitialValues.Money = initialMoney;
			_testItems.InitialValues.MoneyPerWorkshift = initialSalary;
			_testItems.PlayerActions[0].DurationInSeconds = actionDuration;
			_testItems.PlayerActions[0].Type = TestActionTypeWork;
			_testItems.PlayerActions[0].EffectAfter.MoneyPerWorkshift = salaryIncreaseAfterWorkshift; // just to make sure it gets added after money is raised by previous salary

			CreateTestGameState();

			_testGameState.RunAction(TestActionTypeWork.ToString());
			_testGameState.ApplyTime(actionDuration);

			Assert.AreEqual(initialMoney + initialSalary, _testGameState.GetStateItemValue<int>(StateItemType.Money.ToString()));
		}*/
    }
	}
