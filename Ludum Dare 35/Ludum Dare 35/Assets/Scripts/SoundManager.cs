using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioSource musicSource;

	// Use this for initialization
	void Start () {
		musicSource.Play ();
	}
}
