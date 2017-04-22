using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour {

	public void OnPressed () {
		SceneManager.LoadScene(0);
	}
}
