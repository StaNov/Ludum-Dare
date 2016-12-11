using UnityEngine;
using DG.Tweening;

public class RoomMusic : MonoBehaviour {
	
	public CollectibleType Type;
	public MusicPlayer MusicPlayer;
	public Collector Collector;
	
	void OnTriggerEnter2D (Collider2D col) {
		MusicPlayer.StartPlaying(Type);
	}

	void OnTriggerExit2D(Collider2D col) {
		if (Collector.current == null || Collector.current.Type != Type) {
			MusicPlayer.StopPlaying(Type);
		}
	}
}
