using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConstants", menuName = "Create GameplayConstants", order = 1)]
public class GameplayConstants : ScriptableObject
{
	public ChangePerMinuteClass ChangePerMinute;
	
	[Serializable]
	public class ChangePerMinuteClass {
		public float MyEnergy = 10;
		public float MyMaxEnergy = 1;
		public float MyFood = 5;
		public float MyHappiness = 5;
		public float MyHealth = 5;
		public float FamilyFood = 5;
		public float FamilyHappiness = 5;
		public float FamilyHealth = 5;
		public float Age = 2;
	}
}