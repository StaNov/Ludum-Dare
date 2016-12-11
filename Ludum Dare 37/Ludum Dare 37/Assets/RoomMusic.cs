using UnityEngine;
using DG.Tweening;

public class RoomMusic : MonoBehaviour {
	
	private AudioSource source;

	void Awake() {
		source = GetComponent<AudioSource>();
		source.volume = 0;
	}

	void OnTriggerEnter2D (Collider2D col) {
		source.DOFade(1, 1);
	}

	void OnTriggerExit2D(Collider2D col) {
		source.DOFade(0, 1);
	}
}
