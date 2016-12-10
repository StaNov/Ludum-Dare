using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoShower : MonoBehaviour {

	public string InfoText = "Test Text!";

	void OnTriggerEnter2D(Collider2D col) {
		InfoPanel.ShowText(InfoText);
	}

	void OnTriggerExit2D(Collider2D col) {
		InfoPanel.HideText();
	}
}
