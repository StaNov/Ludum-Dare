using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicPlayer : MonoBehaviour {

	public CollectibleSource[] sources;
	public AudioSource wind;
	public float fadeDuration = 2;

	void Awake() {
		foreach (var source in sources) {
			source.source.volume = 0;
			source.playing = false;
		}

		wind.DOFade(1, 1);
	}

	public void StartPlaying(CollectibleType type) {
		
		foreach (var source in sources) {
			if (source.type == type) {
				source.source.DOFade(1, fadeDuration);
				source.playing = true;
			}
		}

		if (! NoSourcePlaying()) {
			wind.DOFade(0, fadeDuration);
		}
	}

	public void StopPlaying(CollectibleType type) {
		foreach (var source in sources) {
			if (source.type == type) {
				source.source.DOFade(0, fadeDuration);
				source.playing = false;
			}
		}

		if (NoSourcePlaying()) {
			wind.DOFade(1, fadeDuration);
		}
	}

	private bool NoSourcePlaying() {
		foreach (var source in sources) {
			if (source.playing) {
				return false;
			}
		}

		return true;
	}
}

[System.Serializable]
public class CollectibleSource {
	public CollectibleType type;
	public AudioSource source;
	public bool playing;
}
