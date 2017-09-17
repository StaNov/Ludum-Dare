using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatsAffector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public GameplayConstants Constants;
	public HoveredActionEffect Effect;

	private Dictionary<string, PlayerAction> _actions;

	private void Start()
	{
		_actions = Constants.GetPlayerActions();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		var playerAction = _actions[gameObject.name];
		Effect.SetEffect(playerAction);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Effect.ResetDifference();
	}
}
