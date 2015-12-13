using UnityEngine;
using System.Collections;

public class CutscenePlayer : MonoBehaviour {

	void Awake () {
		AudioSource audioPlayer = GetComponent<AudioSource>();
		Animator cameraAnimator = GameObject.FindObjectOfType<Animator>();

		cameraAnimator.speed = 1f / audioPlayer.clip.length;
	}
}
