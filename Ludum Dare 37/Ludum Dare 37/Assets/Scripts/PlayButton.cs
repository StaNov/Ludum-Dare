using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayButton : MonoBehaviour {

	public CameraMover mover;
	public CanvasGroup group;

	// Use this for initialization
	public void Start () { // TODO potom změnit na Play
	// public void Play () {
		mover.StartIntro();
		GetComponent<Button>().interactable = false;
		group.DOFade(0, 1);
	}
	
}
