using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicPlayer : MonoBehaviour {

	public CollectibleSource[] sources;
	public AudioSource wind;

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
				source.source.DOFade(1, 1);
				source.playing = true;
			}
		}

		if (! NoSourcePlaying()) {
			wind.DOFade(0, 1);
		}
	}

	public void StopPlaying(CollectibleType type) {
		foreach (var source in sources) {
			if (source.type == type) {
				source.source.DOFade(0, 1);
				source.playing = false;
			}
		}

		if (NoSourcePlaying()) {
			wind.DOFade(1, 1);
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
