using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

	public Transform Pernicek;

	public Transform FirstFloor;
	public Transform SecondFloor;

	public float LerpValueX = 0.05f;
	public float LerpValueY = 0.01f;

	private bool m_IsFirstFloor = true;

	void LateUpdate () {
		float targetY = m_IsFirstFloor ? FirstFloor.position.y : SecondFloor.position.y;
		
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, Pernicek.position.x, LerpValueX), Mathf.Lerp(transform.position.y, targetY, LerpValueY), transform.position.z);
	}

	public void SetFloorFirst(bool value) {
		m_IsFirstFloor = value;
	}
}
