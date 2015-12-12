using UnityEngine;
using System.Collections;

public class BeeCountStore : MonoBehaviour {

	private static BeeCountStore instance;

	public int beesAtStartOfLevel;

	void Awake () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	public static BeeCountStore GetInstance() {
		return instance;
	}
}
