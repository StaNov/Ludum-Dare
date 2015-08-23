using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {

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
			panel.color = Color.Lerp(Color.black, Color.clear, Time.time / 3);
			yield return null;
		}
	}
	
	private IEnumerator fadeOutAndLoadNextLevelCoroutine() {
		float timeOfFadeOutStart = Time.time;
		
		while (Time.time <= timeOfFadeOutStart + 3) {
			panel.color = Color.Lerp(Color.clear, Color.black, (Time.time - timeOfFadeOutStart) / 3);
			yield return null;
		}

		yield return new WaitForSeconds(1);
		
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
