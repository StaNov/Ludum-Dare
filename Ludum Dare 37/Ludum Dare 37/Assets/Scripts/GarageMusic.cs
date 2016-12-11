using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GarageMusic : MonoBehaviour {

	public MusicPlayer MusicPlayer;

	private HashSet<CollectibleType> m_Collected = new HashSet<CollectibleType>();

	void OnTriggerEnter2D(Collider2D col) {

		if (col.CompareTag(Tags.COLLECTIBLE)) {
			Collectible collect = col.GetComponent<Collectible>();
			m_Collected.Add(collect.Type);
		}
			
		foreach (CollectibleType type in m_Collected) {
			MusicPlayer.StartPlaying(type);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.CompareTag(Tags.PLAYER)) {
			foreach (CollectibleType type in m_Collected) {
				MusicPlayer.StopPlaying(type);
			}
		}
	}
}
