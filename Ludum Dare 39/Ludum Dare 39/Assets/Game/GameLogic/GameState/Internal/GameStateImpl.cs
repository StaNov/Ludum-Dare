namespace GameOfLife.GameLogic.GameState.Internal
{
    // TODO redo to factory visible from outside this internal!
    using GameStateItem.Internal;
    using GameStateItem;
    using System.Collections.Generic;
	using UnityEngine;

	public class GameStateImpl : IGameState
	{
		private Dictionary<StateItemType, StateItem> _items;
		private Dictionary<string, PlayerAction> _actions;
		private bool _isFamilyActive = false;

		public GameStateImpl(GameplayConstants constants)
		{
			CurrentPlayerAction = null;
			CurrentPartnerAction = null;
			
			_items = new Dictionary<StateItemType, StateItem>();
			_actions = constants.GetPlayerActions();

			// TODO move to State Factory
			_items.Add(StateItemType.Age, new StateItemFloat(0, 99999, constants, (d) => d.Age, true));
			_items.Add(StateItemType.MyMaxEnergy, new StateItemFloat(0, 100, constants, (d) => d.MyMaxEnergy, true));
			_items.Add(StateItemType.MyEnergy, new StateItemFloat(0, () => GetStateItemValue<float>(StateItemType.MyMaxEnergy), constants, (d) => d.MyEnergy, true));
			_items.Add(StateItemType.MyFood, new StateItemFloat(0, 100, constants, (d) => d.MyFood, true));
			_items.Add(StateItemType.MyHappiness, new StateItemFloat(0, 100, constants, (d) => d.MyHappiness, true));
			_items.Add(StateItemType.MyHealth, new StateItemFloat(0, 100, constants, (d) => d.MyHealth, true));
			_items.Add(StateItemType.FamilyFood, new StateItemFloat(0, 100, constants, (d) => d.FamilyFood, false));
			_items.Add(StateItemType.FamilyHappiness, new StateItemFloat(0, 100, constants, (d) => d.FamilyHappiness, false));
			_items.Add(StateItemType.FamilyHealth, new StateItemFloat(0, 100, constants, (d) => d.FamilyHealth, false));
			_items.Add(StateItemType.Money, new StateItemMoney(constants, (d) => d.Money, () => GetStateItemValue<int>(StateItemType.MySalary), () => GetStateItemValue<int>(StateItemType.PartnerSalary)));
			_items.Add(StateItemType.MySalary, new StateItemInt(constants, (d) => d.MoneyPerWorkshift));
			_items.Add(StateItemType.PartnerSalary, new StateItemInt(constants, (d) => d.MoneyPerPartnersWorkshift));
			_items.Add(StateItemType.FoodSupplies, new StateItemInt(constants, (d) => d.FoodSupplies));
		}

		public T GetStateItemValue<T>(StateItemType type)
		{
			return _items[type].GetValue<T>();
		}

		public CurrentAction CurrentPlayerAction { get; private set; }
		public CurrentAction CurrentPartnerAction { get; private set; }

		public StateItemType? GameOver
		{
			get
			{
				foreach (var item in _items)
					if (item.Value.IsGameOverBecauseOfThis())
						return item.Key;

				// TODO delete when no game over when no money or food
				if (GetStateItemValue<int>(StateItemType.Money) < 0)
					return StateItemType.Money;
				if (GetStateItemValue<int>(StateItemType.FoodSupplies) < 0)
					return StateItemType.FoodSupplies;

				return null;
			}
		}

		public void StartFamily()
		{
			_isFamilyActive = true;
		}

		public void ApplyTime(float deltaTime)
		{
			if (GameOver != null)
			{
				return;
			}

			CurrentPlayerAction = UpdateByAction(CurrentPlayerAction, deltaTime);
			CurrentPartnerAction = UpdateByAction(CurrentPartnerAction, deltaTime);

			UpdateByTime(deltaTime);
		}

		private void UpdateByTime(float deltaTime)
		{
			foreach (var item in _items)
				if ((CurrentPlayerAction == null || item.Value.DifferenceHasZeroEffect(CurrentPlayerAction.Action.EffectDuring))
					&& (CurrentPartnerAction == null || item.Value.DifferenceHasZeroEffect(CurrentPartnerAction.Action.EffectDuring))
					&& ShouldItemBeUpdated(item.Value))
					item.Value.ApplyDifferenceByTime(deltaTime);
		}

		private CurrentAction UpdateByAction(CurrentAction action, float deltaTime)
		{
			if (action != null && action.Action.Type != PlayerActionType.None)
			{
				deltaTime = Mathf.Min(deltaTime, action.RemainingTime); // just the remaining time if smaller then deltaTime
				action.RemainingTime -= deltaTime;

				foreach (var item in _items)
					if (ShouldItemBeUpdated(item.Value))
					{
						float deltaTimeByDuration = action == null ? 0 : deltaTime / action.Action.DurationInSeconds;
						item.Value.ApplyDifferenceByAction(action.Action.EffectDuring, action.Action, deltaTimeByDuration);
					}

				if (action.RemainingTime <= 0)
				{
					UpdateAfterAction(action.Action);
					// TODO action may be "set or unset" internally, not "null or non-null" by itself
					return null;
				}
			}

			return action;
		}

		public void RunAction(PlayerActionType type)
		{
			RunAction(type.ToString());
		}

		public void RunAction(string actionName)
		{
			PlayerAction action = _actions[actionName];
			RunAction(action);
		}

		public void RunAction(PlayerAction action)
		{
			bool isPartnersAction = action.Type.IsPartnersAction();

			if (action.Type.IsForBoth() && CurrentPartnerAction != null && CurrentPartnerAction.Action.Type != PlayerActionType.None)
			{
				CurrentPartnerAction = null;
			}

			UpdateBeforeAction(action);

			var currentAction = new CurrentAction
			{
				Action = action,
				RemainingTime = action.DurationInSeconds
			};

			if (!isPartnersAction)
				CurrentPlayerAction = currentAction;
			else
				CurrentPartnerAction = currentAction;
		}

		private void UpdateBeforeAction(PlayerAction action)
		{
			UpdateStatsOneTime(action.EffectBefore, action);
		}

		private void UpdateAfterAction(PlayerAction action)
		{
			UpdateStatsOneTime(action.EffectAfter, action);
		}

		private void UpdateStatsOneTime(StatsDifference difference, PlayerAction action)
		{
			foreach (var item in _items)
				if (ShouldItemBeUpdated(item.Value))
					item.Value.ApplyDifferenceByAction(difference, action);
		}

		private bool ShouldItemBeUpdated(StateItem item)
		{
			return _isFamilyActive || item.UpdateIfFamilyNotActive();
		}
	}
}
