using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public float MyEnergy = 100;
    public float MyMaxEnergy = 100;
    public float MyFood = 100;
    public float MyHappiness = 100;
    public float MyHealth = 100;
    public float FamilyFood = 100;
    public float FamilyHappiness = 100;
    public float FamilyHealth = 100;

	public static GameState Instance;

	private void Awake ()
	{
		Instance = this;
	}
	
	private void Update () {
		
	}
}
