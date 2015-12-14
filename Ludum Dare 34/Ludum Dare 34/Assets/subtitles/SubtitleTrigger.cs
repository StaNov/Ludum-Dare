using UnityEngine;
using System.Collections;

public class SubtitleTrigger : MonoBehaviour {

	private bool visited = false;

	void OnTriggerEnter2D(Collider2D col) {
		if (visited || !col.CompareTag("Player")) {
			return;
		}

		ShowNextSubtitle();
		visited = true;
	}

	public void ShowNextSubtitle() {
		Subtitles.GetInstance().ShowNextSubtitle();
	}
}
