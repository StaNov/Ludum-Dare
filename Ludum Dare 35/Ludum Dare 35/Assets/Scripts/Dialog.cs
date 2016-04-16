using UnityEngine;
using System.Collections;

[System.Serializable]
public class DialogLine {
	[Multiline]
	public string line;
	public DialogActor actor;
}

public enum DialogActor {
	ACTOR_1, ACTOR2
}

public class Dialog : MonoBehaviour {

	public Color color1;
	public Color color2;
	public DialogLine[] dialogLines;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
