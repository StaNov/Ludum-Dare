using UnityEngine;
using System.Collections;

public class LevelCompletedChecker : MonoBehaviour {

	public TextMesh gameOverText;

	public void checkIfLevelIsCompleted() {
		if (LevelIsCompleted()) {
			Debug.Log("done!");
			Instantiate(gameOverText);
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
