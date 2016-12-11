using UnityEngine;

public class RoomMusic : MonoBehaviour {
	
	private AudioSource source;

	void Awake() {
		source = GetComponent<AudioSource>();
		source.volume = 0;
	}

	void OnTriggerEnter2D (Collider2D col) {
		source.volume = 1;
	}

	void OnTriggerExit2D(Collider2D col) {
		source.volume = 0;
	}
}
