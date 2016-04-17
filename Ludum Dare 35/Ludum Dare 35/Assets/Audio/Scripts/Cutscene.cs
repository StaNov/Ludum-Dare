using UnityEngine;
using System.Collections;

public class Cutscene : MonoBehaviour {
	
	void Start () {
		Fader.instance.FadeOutAndPlayNextScene ();
	}
}
