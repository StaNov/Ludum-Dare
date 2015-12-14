using UnityEngine;
using System.Collections;

public class BossFightStarter : MonoBehaviour {

	public AudioClip bossIntro;
	public AudioClip bossFight;

	private Camera playerCamera;
	private Vector3 cameraTargetPosition;
	private Animator bossAnimator;
	private AudioSource musicPlayer;

	private bool visited = false;

	void Start () {
		playerCamera = GameObject.Find("player").GetComponentInChildren<Camera>();
		cameraTargetPosition = transform.GetChild(0).position;
		bossAnimator = GameObject.Find("boss").GetComponent<Animator>();
		musicPlayer = GameObject.Find("musicPlayer").GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (visited || !col.CompareTag("Player")) {
			return;
		}

		playerCamera.transform.parent = null;
		musicPlayer.clip = bossIntro;
		musicPlayer.volume = 0.65f;
		musicPlayer.Play();

		StartCoroutine(LerpCameraAndStartFight());
		visited = true;
	}

	private IEnumerator LerpCameraAndStartFight() {
		while(Vector3.Distance(playerCamera.transform.position, cameraTargetPosition) > 0.01f) {
			playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, cameraTargetPosition, 0.03f);
			yield return null;
		}

		bossAnimator.SetTrigger("StartFight");
		musicPlayer.clip = bossFight;
		musicPlayer.Play();
	}
}
