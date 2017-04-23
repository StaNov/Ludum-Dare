using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MravecBar : MonoBehaviour {

	public Text text;
	public AntHill antHill;

	void Update () {
		text.text = "x " + antHill.level;
	}
}
