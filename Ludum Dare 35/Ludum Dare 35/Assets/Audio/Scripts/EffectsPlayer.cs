using UnityEngine;
using System.Collections;

public class EffectsPlayer : MonoBehaviour {

	public AudioClip jump;
	public AudioClip shapeshiftIn;
	public AudioClip shapeshiftOut;
	public AudioClip openDoor;

	private AudioSource source;
	public static EffectsPlayer instance;

	void Awake () {
		instance = this;
	}

	void Start () {
		source = GetComponent<AudioSource> ();
	}

	public void Play (AudioClip clip) {
		source.PlayOneShot (clip);
	}
}
