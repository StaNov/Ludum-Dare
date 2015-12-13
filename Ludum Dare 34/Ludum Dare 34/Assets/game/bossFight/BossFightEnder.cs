using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossFightEnder : MonoBehaviour {

	private static BossFightEnder instance;

	void Start() {
		instance = this;
	}

	public void WaitForSecondsAndLoadNextScene(float seconds) {
		StartCoroutine(AfterDeathSoundPlayed(seconds));
	}

	private IEnumerator AfterDeathSoundPlayed(float seconds) {
		yield return new WaitForSeconds(seconds);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public static BossFightEnder GetInstance() {
		return instance;
	}
}
