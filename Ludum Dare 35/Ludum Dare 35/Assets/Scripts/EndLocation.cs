using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndLocation : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		GetComponent<Collider2D> ().enabled = false;
		EffectsPlayer.instance.Play (EffectsPlayer.instance.endLevel);
		FaderAndMusicPlayer.instance.FadeOutAndPlayNextScene ();
	}
}
