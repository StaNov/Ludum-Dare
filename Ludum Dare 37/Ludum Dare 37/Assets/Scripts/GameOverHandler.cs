using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverHandler : MonoBehaviour {

	public IntroItem[] items;
	public Camera camera;
	public CameraMover mover;
	public PernicekController PernicekCtrl;

	public Transform finalPosition;
	public float finalSize;
	public RectTransform credits;
	public Button playAgainButton;


	private AudioSource audioSource;
	private GameObject[] m_Destinations;
	private bool ending = false;

	void Awake() {
		m_Destinations = GameObject.FindGameObjectsWithTag(Tags.DESTINATION);
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
		if (ending) {
			return;
		}

		foreach (GameObject dest in m_Destinations) {
			if (dest.transform.childCount == 0) {
				return;
			}
		}

		StartCoroutine(Ending());
		ending = true;
	}

	private IEnumerator Ending() {

		PernicekCtrl.TurnedOn = false;
		mover.InGame = false;

		foreach (IntroItem item in items) {
			audioSource.clip = item.clip;
			audioSource.Play();
			camera.transform.DOMove(item.cameraEnd.position, item.clip.length).SetEase(Ease.InOutSine);
			camera.DOOrthoSize(item.cameraSize, item.clip.length).SetEase(Ease.InOutSine);
			Subtitles.ShowText(item.subtitle);
			yield return new WaitForSeconds(item.clip.length);
		}


		Subtitles.HideText();
		camera.DOOrthoSize(finalSize, 1);
		camera.transform.DOMove(finalPosition.position, 1);

		yield return new WaitForSeconds(1);

		credits.gameObject.SetActive(true);
		credits.DOAnchorPosY(0, 50).SetEase(Ease.Linear);
		yield return new WaitForSeconds(50);

		playAgainButton.gameObject.SetActive(true);
	}
}
