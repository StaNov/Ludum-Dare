using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	private AudioSource audioSource;
	private Animator animator;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();

		animator.speed /= audioSource.clip.length;
	}
}
