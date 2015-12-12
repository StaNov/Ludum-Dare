using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour {

	public int startLife = 100;

	private int currentLife;
	private Transform healthBar;

	void Start() {
		currentLife = startLife;
		healthBar = transform.Find("healthBar").transform;
	}

	public void Hurt() {
		currentLife--;
		if (currentLife <= 0) {
			Destroy(gameObject);
		}
		healthBar.localScale = new Vector3((float) currentLife / startLife, healthBar.localScale.y, healthBar.localScale.z);
	}
}
