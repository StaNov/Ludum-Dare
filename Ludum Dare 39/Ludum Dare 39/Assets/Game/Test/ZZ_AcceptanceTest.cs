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
    // TODO add test for showed game over screen
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
		public IEnumerator MoreMoneyWhenBothGoToWork()
		{
			yield return Setup();
			yield return null;
			_gameState.StartFamily(); // TODO redo graphics so it gets displayed here?

			_gameState.RunAction(PlayerActionType.GoToWork.ToString());
			_gameState.RunAction(PlayerActionType.PartnerGoesToWork.ToString());

			yield return new WaitForSeconds(15);

			Assert.Greater(_gameState.GetStateItemValue<int>(StateItemType.Money.ToString()), 0);

			_gameState.RunAction(PlayerActionType.GoShopping.ToString());

			yield return new WaitForSeconds(10);

			Assert.Greater(_gameState.GetStateItemValue<int>(StateItemType.FoodSupplies.ToString()), 0);
		}
	}
}
