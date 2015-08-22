using UnityEngine;
using System.Collections;

public class HeadlinePartAccepter : MonoBehaviour {

	public int takableToAccept;
	public LevelCompletedChecker levelCompletedChecker;


	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("headlineTakable")
		    	&& transform.parent.FindChild("headlineTakable") == null
		    	&& col.GetComponent<TakableController>().takableId == takableToAccept) {

			col.transform.parent = transform.parent;
			col.transform.position = transform.position;
			col.transform.GetComponent<TakableController>().takableInTargetPosition = true;

			levelCompletedChecker.checkIfLevelIsCompleted();
		}
	}
}
