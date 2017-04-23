using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour {

	private const float TWEEN_DURATION = 1;

	private const float COEF_ROTATION_SPEED = 1000;
	private const float COEF_MOVE_SPEED = 200;
	private const float MAX_ROTATION_SPEED = 100;
	private const float MAX_MOVE_SPEED = 25;

	public int quantity = 30;
	public CollectibleType type;
	
	private AntController attachedAntLeader = null;
	private AntController[] attachedAnts;

	private Rigidbody2D rb;
	private float weight { get { return type == CollectibleType.FOOD ? quantity : quantity * 100; } }

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		if (attachedAntLeader == null)
		{
			return;
		}

		if (Input.GetAxisRaw("Vertical") > float.Epsilon)
		{
			float moveSpeed = (attachedAnts.Length * COEF_MOVE_SPEED) / weight;
			moveSpeed = Mathf.Clamp(moveSpeed, 0, MAX_MOVE_SPEED);
			rb.MovePosition(transform.position + attachedAntLeader.transform.up * moveSpeed * Time.deltaTime);
		}

		float rotationSpeed = (Input.GetAxisRaw("Horizontal") * attachedAnts.Length * COEF_ROTATION_SPEED) / weight;
		rotationSpeed = Mathf.Clamp(rotationSpeed, -MAX_ROTATION_SPEED, MAX_ROTATION_SPEED);
		transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));
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

		attachedAnts = AntsManager.GetActiveAnts();

		foreach (AntController ant in attachedAnts)
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
		if (ant.isLeader)
		{
			ant.transform.DOLocalRotate(Quaternion.LookRotation(Vector3.forward, - ant.transform.localPosition).eulerAngles, TWEEN_DURATION * 0.3f).SetEase(Ease.InOutSine);
			return;
		}

		float xPos = Random.Range(-10f, 10f);
		float yPos = Random.Range(-10f, 10f);
		Vector3 position = new Vector3(xPos, yPos, 0);
		position.Normalize();
		position *= (GetComponent<CircleCollider2D>().radius + (ant.GetComponent<BoxCollider2D>().size.y / 2) / transform.localScale.x);
		
		DOTween.Sequence()
			.Append(ant.transform.DOLocalRotate(Quaternion.LookRotation(Vector3.forward, position - ant.transform.localPosition).eulerAngles, TWEEN_DURATION * 0.3f).SetEase(Ease.InOutSine))
			.AppendInterval(TWEEN_DURATION * 0.7f)
			.Append(ant.transform.DOLocalRotate(Quaternion.LookRotation(Vector3.forward, - position).eulerAngles, TWEEN_DURATION * 0.5f).SetEase(Ease.InOutSine))
			.Play();
		ant.transform.DOLocalMove(position, TWEEN_DURATION);
	}
}

public enum CollectibleType
{
	FOOD, MATERIAL
}
