using UnityEngine;
using System.Collections;

public class TakableController : MonoBehaviour {

	public int takableId = 0;
	public bool takableInTargetPosition = false;

	void Start () {
		if (takableId == 0) {
			Debug.LogError("takableId is not defined!");
		}
	}
}
