using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubtitlesSwitcher : MonoBehaviour {

	[Multiline]
	public string[] subtitles;
	
	private Text text;
	
	private int index;

	void Start() {
		text = GetComponentInChildren<Text>();
		
		index = 0;
		text.text = "";
	}

	public void NextSubtitle() {

		if (index >= subtitles.Length) {
			text.text = "";
			return;
		}

		text.text = subtitles[index];
		index++;
	}
}
