using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour {
	
	private GameObject[] m_Destinations;

	void Awake() {
		m_Destinations = GameObject.FindGameObjectsWithTag(Tags.DESTINATION);
	}
	
	void Update () {
		foreach (GameObject dest in m_Destinations) {
			if (dest.transform.childCount == 0) {
				return;
			}
		}

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
