using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntsManager : MonoBehaviour {

	private static AntsManager instance;
	
	void Awake () {
		instance = this;
	}

	public static void ReturnAntToActives(AntController ant)
	{
		ant.enabled = true;
		ant.transform.parent = instance.transform;
	}
	
	public static AntController[] GetActiveAnts () {
		return instance.GetComponentsInChildren<AntController>();
	}

	public static int GetLiveAntsCount()
	{
		return 5; // TODO
	}
}
