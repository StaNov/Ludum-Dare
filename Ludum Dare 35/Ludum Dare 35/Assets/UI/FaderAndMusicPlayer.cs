using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FaderAndMusicPlayer : MonoBehaviour {

	private Animator animator;

	public static FaderAndMusicPlayer instance;

	void Awake() {
		instance = this;
		animator = GetComponent<Animator> ();
	}
	
	public void FadeOutAndPlayNextScene () {
		animator.SetTrigger ("FadeOut");	
	}

	public void PlayNextScene() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}
}
