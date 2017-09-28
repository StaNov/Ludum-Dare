namespace GameOfLife.GameLogic.GameState.Internal
{
    // TODO redo to factory visible from outside this internal!
    using GameStateItem.Internal;
    using GameStateItem;
    using System.Collections.Generic;
	using UnityEngine;
    using System;

    public class GameStateImpl : IGameState
	{
		private Dictionary<string, StateItem> _items;
		private Dictionary<string, PlayerAction> _actions;
		private bool _isFamilyActive = false;

        private CurrentAction CurrentPlayerAction;
        private CurrentAction CurrentPartnerAction;

        public GameStateImpl(List<StateItem> items, List<PlayerAction> actions)
		{
			CurrentPlayerAction = null;
			CurrentPartnerAction = null;

            _items = new Dictionary<string, StateItem>();

            foreach (var item in items)
                _items.Add(item.GetName(), item);

			_actions = new Dictionary<string, PlayerAction>();

            foreach (var action in actions)
                _actions.Add(action.GetName(), action);
        }

		public T GetStateItemValue<T>(string type)
		{
			return _items[type].GetValue<T>();
		}

        public KeyValuePair<string, float>? GetCurrentPlayerAction()
        {
            return GetCurrentAction(CurrentPlayerAction);
        }

        public KeyValuePair<string, float>? GetCurrentPartnerAction()
        {
            return GetCurrentAction(CurrentPartnerAction);
        }

        private KeyValuePair<string, float>? GetCurrentAction(CurrentAction action)
        {
            if (action == null)
                return null;

            return new KeyValuePair<string, float>(action.Action.GetName(), 1 - (action.RemainingTime / action.Action.DurationInSeconds));
        }

		public string GameOver
		{
			get
			{
				foreach (var item in _items)
					if (item.Value.IsGameOverBecauseOfThis())
						return item.Key;

				// TODO delete when no game over when no money or food
				if (GetStateItemValue<int>(StateItemType.Money.ToString()) < 0)
					return StateItemType.Money.ToString();
				if (GetStateItemValue<int>(StateItemType.FoodSupplies.ToString()) < 0)
					return StateItemType.FoodSupplies.ToString();

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

		public void RunAction(string actionName)
		{
			PlayerAction action = _actions[actionName];
			RunAction(action);
		}

		private void RunAction(PlayerAction action)
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
