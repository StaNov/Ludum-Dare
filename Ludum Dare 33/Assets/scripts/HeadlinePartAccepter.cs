using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeadlinePartAccepter : MonoBehaviour {

	public int takableToAccept;
	public LevelCompletedChecker levelCompletedChecker;
	public Color backgroundColor;


	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("headlineTakable")
		    	&& transform.parent.Find("headlineTakable") == null
		    	&& col.GetComponent<TakableController>().takableId == takableToAccept) {

			col.transform.SetParent(transform.parent);
			col.transform.position = transform.position + new Vector3(0,0,-1);
			col.transform.GetComponent<TakableController>().takableInTargetPosition = true;
			col.transform.GetComponentInChildren<Image>().color = backgroundColor;

			levelCompletedChecker.checkIfLevelIsCompleted();
		}
	}
}
