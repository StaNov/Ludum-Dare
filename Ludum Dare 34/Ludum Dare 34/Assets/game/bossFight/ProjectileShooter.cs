using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour {

	public GameObject projectilePrefab;
	public int projectileSpeed;
	public AudioSource audioSource;

	private Transform player;
	private Transform hand;

	void Start () {
		player = GameObject.FindWithTag("Player").transform;
		hand = transform.Find("hand");
		audioSource = GetComponent<AudioSource>();
	}

	public void ShootProjectile () {
		GameObject projectile = (GameObject) Instantiate(projectilePrefab, hand .position, Quaternion.identity);
		Vector2 projectileVelocity = player.position - transform.position;
		projectileVelocity.Normalize();
		projectileVelocity *= projectileSpeed;
		projectile.GetComponent<Rigidbody2D>().velocity = projectileVelocity;
		Destroy(projectile, 5);
	}

	public void PlayAngerSound() {
		audioSource.Play();
	}
}
