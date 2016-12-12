using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverHandler : MonoBehaviour {

	public IntroItem[] items;
	public Camera camera;
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

		PernicekCtrl.TurnedOn = false;
		StartCoroutine(Ending());
		ending = true;
	}

	private IEnumerator Ending() {

		foreach (IntroItem item in items) {
			audioSource.clip = item.clip;
			audioSource.Play();
			transform.DOMove(item.cameraEnd.position, item.clip.length).SetEase(Ease.InOutSine);
			camera.DOOrthoSize(item.cameraSize, item.clip.length).SetEase(Ease.InOutSine);
			Subtitles.ShowText(item.subtitle);
			yield return new WaitForSeconds(item.clip.length);
		}


		Subtitles.HideText();
		camera.DOOrthoSize(finalSize, 1);
		camera.transform.DOMove(finalPosition.position, 1);

		yield return new WaitForSeconds(1);

		credits.DOMoveY(1000, 30);
		yield return new WaitForSeconds(30);

		playAgainButton.gameObject.SetActive(true);
	}
}
