namespace GameOfLife.GameLogic.GameState.Internal
{
    using GameOfLife.GameLogic.GameStateAction;
    using GameStateItem;
    using System.Collections.Generic;
	using UnityEngine;

    public class GameStateImpl : IGameState
	{
		private Dictionary<string, StateItem> _items;
		private Dictionary<string, StateAction> _actions;
		private bool _isFamilyActive = false;

        private CurrentAction CurrentPlayerAction;
        private CurrentAction CurrentPartnerAction;

        public GameStateImpl(List<StateItem> items, List<StateAction> actions)
		{
            _items = new Dictionary<string, StateItem>();

            foreach (var item in items)
                _items.Add(item.GetName(), item);

			_actions = new Dictionary<string, StateAction>();

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

            return new KeyValuePair<string, float>(action.Action.GetName(), 1 - (action.RemainingTime / action.Action.GetDurationInSeconds()));
        }

		public string GameOver
		{
			get
			{
				foreach (var item in _items)
					if (item.Value.IsGameOverBecauseOfThis())
						return item.Key;

                // TODO delete when no game over when no money or food
                foreach (var type in new StateItemType[] { StateItemType.Money, StateItemType.FoodSupplies })
				    if (_items.ContainsKey(type.ToString()) && GetStateItemValue<int>(type.ToString()) < 0)
					    return type.ToString();

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
			foreach (var item in _items.Values)
				if ((CurrentPlayerAction == null || item.DifferenceHasZeroEffect(CurrentPlayerAction.Action.GetEffectDuring()))
					&& (CurrentPartnerAction == null || item.DifferenceHasZeroEffect(CurrentPartnerAction.Action.GetEffectDuring()))
					&& ShouldItemBeUpdated(item))
					item.ApplyDifferenceByTime(deltaTime);
		}

		private CurrentAction UpdateByAction(CurrentAction action, float deltaTime)
		{
			if (action != null && action.Action.GetName() != null)
			{
				deltaTime = Mathf.Min(deltaTime, action.RemainingTime); // just the remaining time if smaller then deltaTime
				action.RemainingTime -= deltaTime;

				foreach (var item in _items.Values)
					if (ShouldItemBeUpdated(item))
					{
						float deltaTimeByDuration = action == null ? 0 : deltaTime / action.Action.GetDurationInSeconds();
						item.ApplyDifferenceByAction(action.Action.GetEffectDuring(), action.Action, deltaTimeByDuration);
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
			StateAction action = _actions[actionName];
			RunAction(action);
		}

		private void RunAction(StateAction action)
        {
            if (CurrentPlayerActionIsForBoth())
            {
                CurrentPlayerAction = null;
                CurrentPartnerAction = null;
            }
            
            UpdateBeforeAction(action);

            var currentAction = new CurrentAction
            {
                Action = action,
                RemainingTime = action.GetDurationInSeconds()
            };

            if (!action.IsPartnersAction() || action.IsForBoth())
                CurrentPlayerAction = currentAction;

            if (action.IsPartnersAction() || action.IsForBoth())
                CurrentPartnerAction = currentAction;
        }

        private bool CurrentPlayerActionIsForBoth()
        {
            return CurrentPlayerAction != null && CurrentPlayerAction.Action.IsForBoth();
        }

        private void UpdateBeforeAction(StateAction action)
		{
			UpdateStatsOneTime(action.GetEffectBefore(), action);
		}

		private void UpdateAfterAction(StateAction action)
		{
			UpdateStatsOneTime(action.GetEffectAfter(), action);
		}

		private void UpdateStatsOneTime(StatsDifference difference, StateAction action)
		{
			foreach (var item in _items.Values)
				if (ShouldItemBeUpdated(item))
					item.ApplyDifferenceByAction(difference, action);
		}

		private bool ShouldItemBeUpdated(StateItem item)
		{
			return _isFamilyActive || item.UpdateIfFamilyNotActive();
		}
	}
}
