using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour {

	private static Subtitles m_Instance;

	private Text Text;
	
	void Awake () {
		m_Instance = this;
		Text = GetComponentInChildren<Text>();
		gameObject.SetActive(false);
	}

	public static void ShowText(string text) {
		m_Instance.Text.text = text;
		m_Instance.gameObject.SetActive(true);
	}

	public static void HideText() {
		m_Instance.gameObject.SetActive(false);
	}
}
