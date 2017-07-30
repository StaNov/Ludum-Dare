using UnityEngine;
using UnityEngine.EventSystems;

public class StatsAffector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public GameplayConstants Constants;
	public HoveredActionEffect Effect;

	public void OnPointerEnter(PointerEventData eventData)
	{
		var playerAction = Constants.GetPlayerAction(gameObject.name);
		Effect.SetEffect(playerAction.Effect, playerAction.DurationInSeconds);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Effect.ResetDifference();
	}
}
