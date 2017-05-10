using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameManager : MonoBehaviour {

	private const string PLAYER_NAME_KEY = "PlayerName";

	public static string Name
	{
		get
		{
			return PlayerPrefs.GetString(PLAYER_NAME_KEY, "");
		}
		set
		{
			PlayerPrefs.SetString(PLAYER_NAME_KEY, value);
		}
	}
}
