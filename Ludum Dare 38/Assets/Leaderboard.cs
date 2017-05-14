using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour {
	
	void Start () {
		LeaderBoardConnector.Load(NameManager.Name);
	}
}
