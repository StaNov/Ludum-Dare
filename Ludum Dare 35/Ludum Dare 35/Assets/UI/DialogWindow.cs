using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogWindow : MonoBehaviour {

	public GameObject leftPanel;
	public Text leftText;
	public GameObject rightPanel;
	public Text rightText;

	public void ShowLeftText(string text, Color color) {
		rightPanel.SetActive (false);
		leftPanel.SetActive (true);

		leftText.text = text;
		leftText.color = color;
	}

	public void ShowRightText(string text, Color color) {
		leftPanel.SetActive (false);
		rightPanel.SetActive (true);

		rightText.text = text;
		rightText.color = color;
	}
}
