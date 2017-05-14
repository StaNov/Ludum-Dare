using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvas : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.identity;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<AntController>() != null)
		{
			gameObject.SetActive(false);
		}
	}
}
