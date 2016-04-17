using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	private Animator animator;

	void Awake () {
		animator = GetComponent<Animator> ();
	}

	public void Open() {
		animator.SetTrigger ("Open");
	}
}
