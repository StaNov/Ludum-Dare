using UnityEngine;
using System.Collections;

public class CreditsMusicPlayer : MonoBehaviour {

	private AudioClip song;

	// Use this for initialization
	void Start () {
		CutsceneBackgroundMusicController cutsceneBackgroundMusicController = (CutsceneBackgroundMusicController) GameObject.FindObjectOfType(typeof(CutsceneBackgroundMusicController));

		if (cutsceneBackgroundMusicController != null) {
			Destroy(cutsceneBackgroundMusicController.gameObject);
		}

		song = GetComponent<AudioSource>().clip;
		StartCoroutine(PlaySongAndSwitchToNextScene());
	}

	private IEnumerator PlaySongAndSwitchToNextScene() {
		yield return new WaitForSeconds(song.length);
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
