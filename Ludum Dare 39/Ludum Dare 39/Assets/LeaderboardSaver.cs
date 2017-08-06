using UnityEngine;

public class LeaderboardSaver : MonoBehaviour
{
	public GameState State;
	
	void Update () {
		if (State.GameOver != GameOverReason.StillPlaying)
		{
			ReusableLeaderboardManager.SaveNewRecord(
				"http://games.stanov.cz/ludum-dare-39/callLeaderboardSave.php",
				PlayerNameManager.PlayerName,
				Mathf.FloorToInt(State.Age));
			
			Destroy(gameObject);
		}
	}
}
