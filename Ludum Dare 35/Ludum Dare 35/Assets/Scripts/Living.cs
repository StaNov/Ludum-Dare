using UnityEngine;
using System.Collections;

public class Living : MonoBehaviour {

	private Animator anim;
	private ShapeShiftController ss;
	private Dialog dia;
	private GameObject bus;
	private Rigidbody2D rb;

	private bool ending_scene;

	void Start () {
		anim = GetComponent<Animator> ();
		ss = GetComponent<ShapeShiftController> ();
		dia = GetComponentInChildren<Dialog> ();
		bus = GameObject.Find("Bus").gameObject;
		rb = bus.GetComponent<Rigidbody2D> ();
		ending_scene = false;
	}

	void Update () {
		if (! ending_scene && ss.enabled) {
			ending_scene = true;
			anim.enabled = true;
			dia.enabled = true;
			rb.AddForce (new Vector2 (-10000, 0));
		}
	}

	void FixedUpdate () {
		if (ending_scene) {
			rb.AddForce (new Vector2 (-8000, 0));
		}
	}
}
