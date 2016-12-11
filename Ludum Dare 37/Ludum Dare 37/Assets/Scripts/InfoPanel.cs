using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

	public float SecondsToHide = 3;

	private static InfoPanel m_Instance;

	private Text Text;
	
	void Awake () {
		m_Instance = this;
		Text = GetComponentInChildren<Text>();
		gameObject.SetActive(false);
	}

	public static void ShowText(string text) {
		m_Instance.Text.text = text;
		m_Instance.gameObject.SetActive(true);
		//m_Instance.StartCoroutine(m_Instance.HideAfterSeconds(m_Instance.SecondsToHide));
	}

	public static void HideText() {
		m_Instance.gameObject.SetActive(false);
	}

	private IEnumerator HideAfterSeconds(float seconds) {
		yield return new WaitForSeconds(seconds);

		m_Instance.gameObject.SetActive(false);
	}
}
