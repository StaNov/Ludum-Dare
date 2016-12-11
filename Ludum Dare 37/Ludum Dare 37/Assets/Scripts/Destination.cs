using UnityEngine;
using DG.Tweening;

public class Destination : MonoBehaviour {

	public CollectibleType Type;

	internal void Hide() {
		GetComponent<SpriteRenderer>().DOFade(0, 1);
	}
}
