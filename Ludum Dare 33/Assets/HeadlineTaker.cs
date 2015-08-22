using UnityEngine;
using System.Collections;

public class HeadlineTaker : MonoBehaviour {
	
	// když se dotkneš nadpisu, vezmi nadpis na záda
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("headlineTakable")
			    && ! col.GetComponent<TakableController>().takableInTargetPosition) {			
			col.transform.parent = transform;
		}
	}
}
