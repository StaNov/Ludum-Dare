using UnityEngine;
using System.Collections;

public class CutsceneBackgroundMusicController : MonoBehaviour {

	private static CutsceneBackgroundMusicController instance = null;

	void Start () {

		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this);
			return;
		}

		if (Application.loadedLevelName == "cutscene9") {
			Destroy(gameObject);
			Destroy(instance.gameObject);
			return;
		}

		Destroy(gameObject);
	}
}
