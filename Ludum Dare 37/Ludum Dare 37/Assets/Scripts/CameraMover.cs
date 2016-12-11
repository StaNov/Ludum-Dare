using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraMover : MonoBehaviour {

	public Transform Pernicek;

	public Transform FirstFloor;
	public Transform SecondFloor;
	public PernicekController PernicekCtrl;

	public float LerpValueX = 0.05f;
	public float LerpValueY = 0.01f;

	public float CameraSizeInGame = 6;
	public IntroItem[] IntroItems;

	private bool m_IsFirstFloor = true;
	private bool m_InGame = false;
	private AudioSource audioSource;
	private Camera camera;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
		camera = GetComponent<Camera>();
	}

	public void StartIntro() {
		StartCoroutine(Intro());
	}

	private IEnumerator Intro() {
		foreach (IntroItem item in IntroItems) {
			audioSource.clip = item.clip;
			audioSource.Play();
			transform.DOMove(item.cameraEnd.position, item.clip.length).SetEase(Ease.InOutSine);
			camera.DOOrthoSize(item.cameraSize, item.clip.length).SetEase(Ease.InOutSine);
			yield return new WaitForSeconds(item.clip.length);
		}

		camera.DOOrthoSize(CameraSizeInGame, 1);

		m_InGame = true;
		PernicekCtrl.TurnedOn = true;
	}


	void LateUpdate () {
		if (!m_InGame) {
			return;
		}

		float targetY = m_IsFirstFloor ? FirstFloor.position.y : SecondFloor.position.y;
		
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, Pernicek.position.x, LerpValueX), Mathf.Lerp(transform.position.y, targetY, LerpValueY), transform.position.z);
	}

	public void SetFloorFirst(bool value) {
		m_IsFirstFloor = value;
	}
}

[System.Serializable]
public class IntroItem {
	public Transform cameraEnd;
	public float cameraSize;
	public AudioClip clip;
}
