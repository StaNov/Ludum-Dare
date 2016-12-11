using UnityEngine;
using DG.Tweening;

public class Collector : MonoBehaviour {

	public Transform slot;
	public Collectible current = null;
	public MusicPlayer musicPlayer;
	public float tweenTime = 0.5f;

	public bool HasCollectible { get { return current != null; } }

	void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag(Tags.COLLECTIBLE) && current == null && (col.transform.parent == null || ! col.transform.parent.CompareTag(Tags.DESTINATION))) {
			current = col.transform.GetComponent<Collectible>();

			current.transform.SetParent(slot);
			current.transform.DOLocalMove(Vector3.zero, tweenTime);
			current.transform.DOLocalRotateQuaternion(Quaternion.identity, tweenTime);

			musicPlayer.StartPlaying(current.Type);
		}

		Destination dest = col.GetComponent<Destination>();
		if (dest != null && current != null && dest.Type == current.Type) {
			current.transform.parent = col.transform;
			current.transform.DOLocalMove(Vector3.zero, tweenTime);
			current.transform.DOLocalRotateQuaternion(Quaternion.identity, tweenTime);

			current.DisableTrigger();

			current = null;
		}
	}

	public void ReturnCollectible() {
		if (current == null) {
			return;
		}

		current.ReturnToOrigin();
		musicPlayer.StopPlaying(current.Type);
		current = null;
	}
}
