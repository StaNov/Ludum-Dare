namespace GameOfLife
{
	using UnityEngine;
	using UnityEngine.TestTools;
	using System.Collections;
	using UnityEngine.SceneManagement;
	using NUnit.Framework;
    using GameOfLife.GameLogic;
    using GameOfLife.GameLogic.GameState;

    // TODO add test for hovering over a button
    public class ZZ_AcceptanceTest
	{
		private IGameState _gameState;

		private IEnumerator Setup()
		{
			Time.timeScale = 99;
			SceneManager.LoadScene("Game");
			yield return null;
			_gameState = Object.FindObjectOfType<GameStateHolder>().State;
		}

		[UnityTest]
		public IEnumerator AttributesAreLoweredByTime()
		{
			yield return Setup();
			yield return new WaitForSeconds(7);

			foreach (StateItemType type in new StateItemType[] { StateItemType.MyEnergy, StateItemType.MyFood, StateItemType.MyHappiness, StateItemType.MyHealth, StateItemType.MyMaxEnergy })
				Assert.Less(_gameState.GetStateItemValue<float>(type.ToString()), 99);
		}

		[UnityTest]
		public IEnumerator FamilyAttributesAreFullAfterStartFamily()
		{
			yield return Setup();
			yield return new WaitForSeconds(7);

			_gameState.StartFamily();

			foreach (StateItemType type in new StateItemType[] { StateItemType.FamilyFood, StateItemType.FamilyHappiness, StateItemType.FamilyHealth })
				Assert.AreEqual(_gameState.GetStateItemValue<float>(type.ToString()), 100f, 0.01f);
		}

		[UnityTest]
		public IEnumerator LearnNewSkillsMakesSalaryGreater()
		{
			yield return Setup();
			_gameState.RunAction(PlayerActionType.LearnNewStuffForWork.ToString());

			yield return new WaitForSeconds(11);

			Assert.Greater(_gameState.GetStateItemValue<int>(StateItemType.MySalary.ToString()), 0);
		}

		[UnityTest]
        [Timeout(int.MaxValue)]
		public IEnumerator MoreMoneyWhenPlayerGoesToWork()
		{
			yield return Setup();
			yield return null;

			_gameState.RunAction(PlayerActionType.GoToWork.ToString());

			yield return new WaitForSeconds(20);

			Assert.Greater(_gameState.GetStateItemValue<int>(StateItemType.Money.ToString()), 0);
        }

        [UnityTest]
        [Timeout(int.MaxValue)]
        public IEnumerator MoreMoneyWhenPartnerGoesToWork()
        {
            yield return Setup();
            yield return null;
            _gameState.StartFamily(); // TODO redo graphics so it gets displayed here?
            
            _gameState.RunAction(PlayerActionType.PartnerGoesToWork.ToString());

            yield return new WaitForSeconds(20);

            Assert.Greater(_gameState.GetStateItemValue<int>(StateItemType.Money.ToString()), 0);
        }

        [UnityTest]
        [Timeout(int.MaxValue)]
        public IEnumerator ShoppingAddsFoodSupplies()
        {
            yield return Setup();
            yield return null;
            _gameState.StartFamily(); // TODO redo graphics so it gets displayed here?

            _gameState.RunAction(PlayerActionType.GoToWork.ToString());
            _gameState.RunAction(PlayerActionType.PartnerGoesToWork.ToString());

            yield return new WaitForSeconds(20);

            _gameState.RunAction(PlayerActionType.GoShopping.ToString());

            yield return new WaitForSeconds(10);

            Assert.Greater(_gameState.GetStateItemValue<int>(StateItemType.FoodSupplies.ToString()), 0);
        }

        [UnityTest]
        [Timeout(int.MaxValue)]
        public IEnumerator EatingRemovesSupplies()
        {
            yield return Setup();
            yield return null;
            _gameState.StartFamily(); // TODO redo graphics so it gets displayed here?

            _gameState.RunAction(PlayerActionType.GoToWork.ToString());
            _gameState.RunAction(PlayerActionType.PartnerGoesToWork.ToString());

            yield return new WaitForSeconds(20);

            _gameState.RunAction(PlayerActionType.GoShopping.ToString());

            yield return new WaitForSeconds(10);

            int foodSuppliesAfterShopping = _gameState.GetStateItemValue<int>(StateItemType.FoodSupplies.ToString());

            _gameState.RunAction(PlayerActionType.Eat.ToString());

            yield return new WaitForSeconds(10);

            Assert.Less(_gameState.GetStateItemValue<int>(StateItemType.FoodSupplies.ToString()), foodSuppliesAfterShopping);
        }

        [UnityTest]
        [Timeout(int.MaxValue)]
        public IEnumerator EatingAddsFood()
        {
            yield return Setup();
            yield return null;
            _gameState.StartFamily(); // TODO redo graphics so it gets displayed here?

            _gameState.RunAction(PlayerActionType.GoToWork.ToString());
            _gameState.RunAction(PlayerActionType.PartnerGoesToWork.ToString());

            yield return new WaitForSeconds(20);

            _gameState.RunAction(PlayerActionType.GoShopping.ToString());

            yield return new WaitForSeconds(15);

            float myFoodBeforeEating = _gameState.GetStateItemValue<float>(StateItemType.MyFood.ToString());

            _gameState.RunAction(PlayerActionType.Eat.ToString());

            yield return new WaitForSeconds(10);

            Assert.Greater(_gameState.GetStateItemValue<float>(StateItemType.MyFood.ToString()), myFoodBeforeEating);
        }

        [UnityTest]
        [Timeout(int.MaxValue)]
        public IEnumerator SpendingTimeTogetherAddsHappinessToEveryone()
        {
            yield return Setup();
            yield return null;
            _gameState.StartFamily(); // TODO redo graphics so it gets displayed here?

            yield return new WaitForSeconds(20);

            float myHappinessBeforeTimeTogether = _gameState.GetStateItemValue<float>(StateItemType.MyHappiness.ToString());
            float familyHappinessBeforeTimeTogether = _gameState.GetStateItemValue<float>(StateItemType.FamilyHappiness.ToString());

            _gameState.RunAction(PlayerActionType.SpendTimeWithFamily.ToString());

            yield return new WaitForSeconds(10);

            Assert.Greater(_gameState.GetStateItemValue<float>(StateItemType.MyHappiness.ToString()), myHappinessBeforeTimeTogether);
            Assert.Greater(_gameState.GetStateItemValue<float>(StateItemType.FamilyHappiness.ToString()), familyHappinessBeforeTimeTogether);
        }

        // TODO delete afterwards
        [UnityTest]
        public IEnumerator GameOverAfterBuyingFoodWithoutMoney()
        {
            yield return Setup();
            _gameState.RunAction(PlayerActionType.GoShopping.ToString());

            Assert.AreEqual(StateItemType.Money.ToString(), _gameState.GameOver);
        }

        [UnityTest]
        [Timeout(int.MaxValue)]
        public IEnumerator GameOverBecauseOfEnergy()
        {
            yield return Setup();
            yield return null;

            for (int i = 0; i < 4; i++)
            {
                _gameState.RunAction(PlayerActionType.GoToWork.ToString());
                yield return new WaitForSeconds(20);
            }

            Assert.AreEqual(StateItemType.MyEnergy.ToString(), _gameState.GameOver);
        }
    }
}
