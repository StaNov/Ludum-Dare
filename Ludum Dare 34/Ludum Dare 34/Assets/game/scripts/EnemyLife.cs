using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyLife : MonoBehaviour {

	public int startLife = 100;
	public bool nextSceneOnDeath = false;

	private int currentLife;
	private Transform healthBar;

	void Start() {
		currentLife = startLife;
		healthBar = transform.Find("healthBar");

		if (healthBar == null) {
			healthBar = transform.Find("healthBarWrapper/healthBar");
		}
	}

	public void Hurt() {
		currentLife--;
		if (currentLife <= 0) {
			if (nextSceneOnDeath) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			} else {
				Destroy(gameObject);
			}
		}
		healthBar.localScale = new Vector3((float) currentLife / startLife, healthBar.localScale.y, healthBar.localScale.z);
	}
}
