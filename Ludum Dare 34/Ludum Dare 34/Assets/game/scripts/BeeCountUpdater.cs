using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BeeCountUpdater : MonoBehaviour {

	public int maximumBeeCount = 0;

	private Transform beesParent;
	private Text beeCountText;

	private static BeeCountUpdater instance = null;

	void Awake() {
		instance = this;
		beesParent = GameObject.Find("bees").transform;
		beeCountText = GetComponent<Text>();
	}

	void Start () {
		UpdateBeeCount ();
	}

	public void UpdateBeeCount () {
		beeCountText.text = "Bees in beard: " + beesParent.childCount + " / " + maximumBeeCount;
	}

	public static BeeCountUpdater GetInstance() {
		return instance;
	}
}
