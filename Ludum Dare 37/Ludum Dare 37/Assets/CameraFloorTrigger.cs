using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFloorTrigger : MonoBehaviour {

	public bool IsFirst;
	public CameraMover Mover;
	
	void OnTriggerEnter2D (Collider2D col) {
		Mover.SetFloorFirst(IsFirst);
	}
}
