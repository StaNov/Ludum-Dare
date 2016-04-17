using UnityEngine;
using System.Collections;

public class Bus : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.gameObject.name == "Living") {
			EffectsPlayer.instance.Play (EffectsPlayer.instance.busHit);
			FaderAndMusicPlayer.instance.FadeOutAndPlayNextScene ();
		}
	}
}
