using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {
	
	private static float FADE_DURATION = 3;
	private static float WAIT_AFTER_FADE_OUT = 1;

	private Image panel;

	void Start () {
		panel = GetComponentInChildren<Image>();
		panel.color = Color.black;

		StartCoroutine(fadeInCoroutine());
	}


	public void fadeOutAndLoadNextLevel() {
		StartCoroutine(fadeOutAndLoadNextLevelCoroutine());
	}
	
	private IEnumerator fadeInCoroutine() {		
		while (Time.time <= 3) {
			panel.color = Color.Lerp(Color.black, Color.clear, Time.time / FADE_DURATION);
			yield return null;
		}
	}
	
	private IEnumerator fadeOutAndLoadNextLevelCoroutine() {
		float timeOfFadeOutStart = Time.time;
		
		while (Time.time <= timeOfFadeOutStart + FADE_DURATION) {
			panel.color = Color.Lerp(Color.clear, Color.black, (Time.time - timeOfFadeOutStart) / FADE_DURATION);
			yield return null;
		}

		yield return new WaitForSeconds(WAIT_AFTER_FADE_OUT);
		
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
