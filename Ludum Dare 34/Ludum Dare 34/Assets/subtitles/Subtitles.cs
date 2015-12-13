using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Subtitles : MonoBehaviour {

	public bool showFirstSubtitleAtStart = false;
	[Multiline]
	public string[] subtitles = new string[] {"Test subtitle!\nTest subtitle new line!", "", "", "", ""};

	private GameObject panel;
	private Text textField;

	private static Subtitles instance;

	private int currentSubtitle = 0;


	void Awake() {
		instance = this;
	}

	void Start () {
		panel = transform.GetChild(0).gameObject;
		textField = panel.transform.GetChild(0).GetComponent<Text>();
		panel.SetActive(false);

		if (showFirstSubtitleAtStart) {
			ShowNextSubtitle();
		}
	}

	public void ShowNextSubtitle () {
		if (currentSubtitle >= subtitles.Length) {
			return;
		}

		StopAllCoroutines();

		textField.text = subtitles[currentSubtitle];
		currentSubtitle++;
		StartCoroutine(HideSubtitle());
		panel.SetActive(true);
	}

	private IEnumerator HideSubtitle() {
		yield return new WaitForSeconds(10);
		panel.SetActive(false);
	}

	public static Subtitles GetInstance() {
		return instance;
	}
}
