using UnityEngine;
using System.Collections;

public class GameRestarter : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel(0);
		}
	}
}
