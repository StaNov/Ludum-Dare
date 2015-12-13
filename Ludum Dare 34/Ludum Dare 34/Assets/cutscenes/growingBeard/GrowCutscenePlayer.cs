using UnityEngine;
using System.Collections;

public class GrowCutscenePlayer : MonoBehaviour {

	void Awake () {
		Animator animator = GameObject.Find("cutscenePlayer").GetComponent<Animator>();
		AudioSource audioSource = GetComponent<AudioSource>();
		animator.speed = 1f / audioSource.clip.length;
	}
}
