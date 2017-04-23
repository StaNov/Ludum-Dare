using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour {

	private const float TWEEN_DURATION = 1f;

	public int quantity = 30;
	public CollectibleType type;
	
	private AntController attachedAntLeader = null;
	
	void FixedUpdate () {
		if (attachedAntLeader == null)
		{
			return;
		}

		transform.Translate(
			Input.GetAxisRaw("Horizontal") * Time.deltaTime * 10,
			Input.GetAxisRaw("Vertical") * Time.deltaTime * 10,
			0
		);
	}

	public AntController[] ReleaseAntsAndDestroy()
	{
		AntController[] ants = GetComponentsInChildren<AntController>();

		foreach (AntController ant in ants)
		{
			ant.transform.parent = null;
		}

		attachedAntLeader = null;

		Destroy(gameObject);
		return ants;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (attachedAntLeader != null)
		{
			return;
		}

		if (! collision.collider.GetComponent<AntController>().isLeader)
		{
			return;
		}
		
		foreach (AntController ant in AntsManager.GetActiveAnts())
		{
			ant.enabled = false;
			ant.transform.parent = transform;
			SetAntPositionAndRotationAfterHit(ant);
			ant.GetComponent<Collider2D>().enabled = false;

			if (ant.isLeader)
			{
				attachedAntLeader = ant;
			}
		}
	}

	private void SetAntPositionAndRotationAfterHit(AntController ant)
	{
		float xPos = Random.Range(-10f, 10f);
		float yPos = Random.Range(-10f, 10f);
		Vector3 position = new Vector3(xPos, yPos, 0);
		position.Normalize();
		position *= (GetComponent<CircleCollider2D>().radius + (ant.GetComponent<BoxCollider2D>().size.y / 2) / transform.localScale.x);
		
		DOTween.Sequence()
			.Append(ant.transform.DOLocalRotate(Quaternion.LookRotation(Vector3.forward, position - ant.transform.localPosition).eulerAngles, TWEEN_DURATION * 0.3f).SetEase(Ease.InOutSine))
			.AppendInterval(TWEEN_DURATION * 0.7f)
			.Append(ant.transform.DOLocalRotate(Quaternion.LookRotation(Vector3.forward, Vector3.zero - position).eulerAngles, TWEEN_DURATION * 0.5f).SetEase(Ease.InOutSine))
			.Play();
		ant.transform.DOLocalMove(position, TWEEN_DURATION);
	}
}

public enum CollectibleType
{
	FOOD, MATERIAL
}
