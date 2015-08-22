using UnityEngine;
using System.Collections;

public class LevelCompletedChecker : MonoBehaviour {

	public TextMesh gameOverText;

	public void checkIfLevelIsCompleted() {
		if (LevelIsCompleted()) {
			if (Application.loadedLevel == Application.levelCount - 1) {
				Instantiate(gameOverText);
			} else {
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		};
	}

	private bool LevelIsCompleted() {
		GameObject[] headlineTakables = GameObject.FindGameObjectsWithTag("headlineTakable");

		foreach (GameObject headlineTakable in headlineTakables) {
			if (! headlineTakable.GetComponent<TakableController>().takableInTargetPosition) {
				return false;
			}
		}

		return true;
	}
}
