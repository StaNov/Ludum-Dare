using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour {

	public int startLife = 100;

	private int currentLife;

	void Start() {
		currentLife = startLife;
	}

	public void Hurt() {
		currentLife--;
		if (currentLife <= 0) {
			Destroy(gameObject);
		}
	}
}
