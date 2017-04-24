using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalDisplayer : MonoBehaviour {

	public GameObject bronze;
	public GameObject silver;
	public GameObject gold;

	// Use this for initialization
	void Start () {
		bronze.SetActive(PlayerPrefs.GetString("medal", "") == "bronze");
		silver.SetActive(PlayerPrefs.GetString("medal", "") == "silver");
		gold.SetActive(PlayerPrefs.GetString("medal", "") == "gold");
	}
}
