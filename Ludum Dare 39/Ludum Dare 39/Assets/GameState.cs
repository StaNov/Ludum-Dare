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
    public float Age = 25;

	public GameplayConstants Constants;

	public static GameState Instance;
	
	private static float DeltaTimeInMinutes { get { return Time.deltaTime * (1.0f / 60.0f); }}

	private void Awake ()
	{
		Instance = this;
	}
	
	private void Update ()
	{
		MyEnergy -= Constants.ChangePerMinute.MyEnergy * DeltaTimeInMinutes;
		MyMaxEnergy -= Constants.ChangePerMinute.MyMaxEnergy * DeltaTimeInMinutes;
		MyFood -= Constants.ChangePerMinute.MyFood * DeltaTimeInMinutes;
		MyHappiness -= Constants.ChangePerMinute.MyHappiness * DeltaTimeInMinutes;
		MyHealth -= Constants.ChangePerMinute.MyHealth * DeltaTimeInMinutes;
		FamilyFood -= Constants.ChangePerMinute.FamilyFood * DeltaTimeInMinutes;
		FamilyHappiness -= Constants.ChangePerMinute.FamilyHappiness * DeltaTimeInMinutes;
		FamilyHealth -= Constants.ChangePerMinute.FamilyHealth * DeltaTimeInMinutes;
		Age += Constants.ChangePerMinute.Age * DeltaTimeInMinutes;
	}
}
