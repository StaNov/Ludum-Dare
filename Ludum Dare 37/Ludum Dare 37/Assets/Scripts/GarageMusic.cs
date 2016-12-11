using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GarageMusic : MonoBehaviour {

	public CollectibleSource[] sources;
	private HashSet<CollectibleType> m_Collected = new HashSet<CollectibleType>();

	void Awake() {
		foreach (var source in sources) {
			source.source.volume = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (col.CompareTag(Tags.COLLECTIBLE)) {
			Collectible collect = col.GetComponent<Collectible>();
			m_Collected.Add(collect.Type);
		}
			
		foreach (var source in sources) {
			if (m_Collected.Contains(source.type)) {
				source.source.DOFade(1, 1);
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.CompareTag(Tags.PLAYER)) {
			foreach (var source in sources) {
				source.source.DOFade(0, 1);
			}
		}
	}
}

[System.Serializable]
public class CollectibleSource {
	public CollectibleType type;
	public AudioSource source;
}
