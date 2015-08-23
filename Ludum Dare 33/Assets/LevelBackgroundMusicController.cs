using UnityEngine;
using System.Collections;

public class LevelBackgroundMusicController : MonoBehaviour {

	public AudioClip intro;
	public AudioClip song;

	private AudioSource audioSource;

	void Start () {
		CutsceneBackgroundMusicController cutsceneMusicController = (CutsceneBackgroundMusicController) GameObject.FindObjectOfType(typeof(CutsceneBackgroundMusicController));

		if (cutsceneMusicController != null) {
			Destroy(cutsceneMusicController.gameObject);
		}

		audioSource = GetComponent<AudioSource>();

		StartCoroutine(PlayIntroAndLoopSong());
	}

	private IEnumerator PlayIntroAndLoopSong() {
		audioSource.clip = intro;
		audioSource.Play();
		yield return new WaitForSeconds(intro.length);
		audioSource.clip = song;
		audioSource.loop = true;
		audioSource.Play();
	}
}
