using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardLine : MonoBehaviour {

	public Text placeText;
	public Text playerNameText;
	public Text scoreText;

	public int place { set { if (placeText == null) return; placeText.text = value + "."; } }
	public string playerName { set { playerNameText.text = value; } }
	public int score { set { scoreText.text = value + " ants"; } }
	public bool isCurrentPlayer { set { if (value == false) return; if (placeText != null) placeText.color = Color.red; playerNameText.color = Color.red; scoreText.color = Color.red; } }
}
