using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {
	
	private static float FADE_DURATION = 3;
	private static float WAIT_AFTER_FADE_OUT = 0.5f;

	private Image panel;
	private AudioClip audioClip;
	private Animator animator;

	void Start () {
		panel = GetComponentInChildren<Image>();
		audioClip = GetComponent<AudioSource>().clip;
		animator = GetComponent<Animator>();
		panel.color = Color.black;
		animator.speed = 1/audioClip.length;

		StartCoroutine(fadeInPlayAnimationFadeOut());
	}
	

	private IEnumerator fadeInPlayAnimationFadeOut() {

		// fade in
		while (Time.time <= 3) {
			panel.color = Color.Lerp(Color.black, Color.clear, Time.time / FADE_DURATION);
			yield return null;
		}


		// wait for audioClip end
		yield return new WaitForSeconds(audioClip.length - 2*FADE_DURATION);


		// fade out
		float timeOfFadeOutStart = Time.time;
		
		while (Time.time <= timeOfFadeOutStart + FADE_DURATION) {
			panel.color = Color.Lerp(Color.clear, Color.black, (Time.time - timeOfFadeOutStart) / FADE_DURATION);
			yield return null;
		}

		yield return new WaitForSeconds(WAIT_AFTER_FADE_OUT);
		
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
