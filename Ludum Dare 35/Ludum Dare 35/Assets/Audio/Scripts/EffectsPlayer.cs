using UnityEngine;
using System.Collections;

public class EffectsPlayer : MonoBehaviour {

	public AudioClip jump;
	public AudioClip shapeshift;
	public AudioClip openDoor;
	public AudioClip endLevel;
	public AudioClip rockFall;

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
