using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraMover : MonoBehaviour {

	public bool skipIntro = false;
	public Transform Pernicek;

	public Transform FirstFloor;
	public Transform SecondFloor;
	public PernicekController PernicekCtrl;

	public float LerpValueX = 0.05f;
	public float LerpValueY = 0.01f;

	public float CameraSizeInGame = 6;
	public IntroItem[] IntroItems;
	public AudioClip tutorialClip;
	[Multiline]
	public string tutorialSubtitle;

	private bool m_IsFirstFloor = true;
	public bool InGame = false;
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
			Subtitles.ShowText(item.subtitle);
			yield return new WaitForSeconds(item.clip.length);
		}

		audioSource.clip = tutorialClip;
		audioSource.Play();
		Subtitles.ShowText(tutorialSubtitle);
		InGame = true;
		camera.DOOrthoSize(CameraSizeInGame, 1);

		yield return new WaitForSeconds(tutorialClip.length);

		PernicekCtrl.TurnedOn = true;

		Subtitles.HideText();
	}


	void LateUpdate () {
		if (!InGame) {
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
	[Multiline]
	public string subtitle;
}
