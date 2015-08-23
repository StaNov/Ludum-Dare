using UnityEngine;
using System.Collections;

public class CutsceneController : MonoBehaviour {

	public ScreenFader screenFader;

	public void FadeOutAndLoadNextLevel() {
		screenFader.fadeOutAndLoadNextLevel();
	}
}
