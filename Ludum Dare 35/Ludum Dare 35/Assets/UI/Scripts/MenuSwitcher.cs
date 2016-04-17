using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuSwitcher : MonoBehaviour {

	public void SwitchToMenu () {
		SceneManager.LoadScene (0);
	}
}
