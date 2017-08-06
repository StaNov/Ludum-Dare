using UnityEngine;

public static class PlayerNameManager {

	private const string NamePrefsKey = "PlayersNameGameOfLife"; 
	
	public static string PlayerName {
		get
		{
			return PlayerPrefs.GetString(NamePrefsKey, "");
		}
		set
		{
			PlayerPrefs.SetString(NamePrefsKey, value);
			PlayerPrefs.Save();
		}
	}
}
