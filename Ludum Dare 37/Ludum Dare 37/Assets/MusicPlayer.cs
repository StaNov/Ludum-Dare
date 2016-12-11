using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicPlayer : MonoBehaviour {

	public CollectibleSource[] sources;

	void Awake() {
		foreach (var source in sources) {
			source.source.volume = 0;
		}
	}

	public void StartPlaying(CollectibleType type) {
		
		foreach (var source in sources) {
			if (source.type == type) {
				source.source.DOFade(1, 1);
			}
		}
	}

	public void StopPlaying(CollectibleType type) {
		foreach (var source in sources) {
			if (source.type == type) {
				source.source.DOFade(0, 1);
			}
		}
	}
}

[System.Serializable]
public class CollectibleSource {
	public CollectibleType type;
	public AudioSource source;
}
