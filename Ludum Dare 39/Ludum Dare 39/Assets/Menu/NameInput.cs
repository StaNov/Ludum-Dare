using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
	public InputField Input;
	
	void Start ()
	{
		Input.text = PlayerNameManager.PlayerName;
	}
	
	public void OnEditEnd () {
		PlayerNameManager.PlayerName = Input.text;
	}
}
