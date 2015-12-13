using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyLife : MonoBehaviour {

	public int startLife = 100;
	public bool boss = false;

	private int currentLife;
	private Transform healthBar;
	private AudioSource deathAudioSource;

	private GameObject deathAudioPlayer;
	private AudioSource musicPlayer;

	void Start() {
		currentLife = startLife;
		healthBar = transform.Find("healthBarWrapper/healthBar");
		deathAudioPlayer = transform.Find("deathAudioPlayer").gameObject;
		deathAudioSource = deathAudioPlayer.GetComponent<AudioSource>();
		musicPlayer = GameObject.Find("musicPlayer").GetComponent<AudioSource>();
	}

	public void Hurt() {
		currentLife--;
		if (currentLife <= 0) {
			deathAudioSource.Play();
			deathAudioPlayer.transform.parent = null;

			if (boss) {
				musicPlayer.Stop();
				BossFightEnder.GetInstance().WaitForSecondsAndLoadNextScene(deathAudioSource.clip.length);
			}

			Destroy(gameObject);
		}

		healthBar.localScale = new Vector3((float) currentLife / startLife, healthBar.localScale.y, healthBar.localScale.z);
	}
}
