using GameOfLife.GameLogic;
using UnityEngine;

public class LeaderboardSaver : MonoBehaviour
{
	public GameStateHolder State;

	private bool _recordSaved;
	
	void Update () {
		if (!_recordSaved && State.State.GameOver != null)
		{
			StartCoroutine(ReusableLeaderboardManager.SaveNewRecord(
				"http://games.stanov.cz/ludum-dare-39/callLeaderboardSave.php",
				PlayerNameManager.PlayerName,
				Mathf.FloorToInt(State.State.GetStateItemValue<float>(StateItemType.Age.ToString()))));
			
			_recordSaved = true;
		}
	}
}
