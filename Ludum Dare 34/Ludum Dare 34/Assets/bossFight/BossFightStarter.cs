using UnityEngine;
using System.Collections;

public class BossFightStarter : MonoBehaviour {

	private Camera playerCamera;
	private Vector3 cameraTargetPosition;
	private Animator bossAnimator;

	void Start () {
		playerCamera = GameObject.FindWithTag("Player").GetComponentInChildren<Camera>();
		cameraTargetPosition = transform.GetChild(0).position;
		bossAnimator = GameObject.Find("boss").GetComponent<Animator>();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (!col.CompareTag("Player")) {
			return;
		}

		playerCamera.transform.parent = null;

		StartCoroutine(LerpCameraAndStartFight());
	}

	private IEnumerator LerpCameraAndStartFight() {
		while(Vector3.Distance(playerCamera.transform.position, cameraTargetPosition) > 0.01f) {
			playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, cameraTargetPosition, 0.03f);
			yield return null;
		}

		bossAnimator.SetTrigger("StartFight");
	}
}
