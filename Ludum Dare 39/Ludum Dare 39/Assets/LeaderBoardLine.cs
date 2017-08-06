using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardLine : MonoBehaviour
{
	public Text Place;
	public Text Name;
	public Text Score;

	public void SetValues(int place, string playerName, int score)
	{
		Place.text = place + ".";
		Name.text = playerName;
		Score.text = score + " years";
	}

	public void SetCurrentPlayer()
	{
		Place.color = Color.red;
		Name.color = Color.red;
		Score.color = Color.red;
	}
}
