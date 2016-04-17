using UnityEngine;
using System.Collections;

public class Living : MonoBehaviour {

	private Animator anim;
	private ShapeShiftController ss;
	private Dialog dia;
	private GameObject bus;
	private Rigidbody2D rb;
	private GameObject right;
	private BoxCollider2D bc;

	private bool ending_scene;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		ss = GetComponent<ShapeShiftController> ();
		dia = GetComponentInChildren<Dialog> ();
		bus = GameObject.Find("Bus").gameObject;
		right = GameObject.Find("Right_border").gameObject;
		rb = bus.GetComponent<Rigidbody2D> ();
		bc = right.GetComponent<BoxCollider2D> ();
		ending_scene = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (! ending_scene && ss.enabled) {
			ending_scene = true;
			anim.enabled = true;
			dia.enabled = true;
			bc.enabled = false;
			rb.AddForce (new Vector2 (-10000, 0));
		}
	}

	void FixedUpdate () {
		if (ending_scene) {
			rb.AddForce (new Vector2 (-100, 0), ForceMode2D.Impulse);
		}
	}
}
