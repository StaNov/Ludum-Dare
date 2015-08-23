using UnityEngine;
using System.Collections;

public class CutsceneBackgroundMusicController : MonoBehaviour {

	private static CutsceneBackgroundMusicController instance = null;

	// Use this for initialization
	void Start () {

		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this);
		} else if (Application.loadedLevelName == "cutscene9") {
			Destroy(gameObject);
			Destroy(instance.gameObject);
		} else {
			Destroy(gameObject);
		}
	}
}
