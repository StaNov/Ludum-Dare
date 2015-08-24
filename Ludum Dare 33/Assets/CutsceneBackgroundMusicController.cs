using UnityEngine;
using System.Collections;

public class CutsceneBackgroundMusicController : MonoBehaviour {

	private static CutsceneBackgroundMusicController instance = null;

	void Start () {

		if (instance == null && Application.loadedLevelName != "cutscene9") {
			instance = this;
			DontDestroyOnLoad(this);
			return;
		}

		if (Application.loadedLevelName == "cutscene9") {
			Destroy(gameObject);
			if (instance != null) {
				Destroy(instance.gameObject);
				instance = null;
			}

			return;
		}

		Destroy(gameObject);
	}
}
