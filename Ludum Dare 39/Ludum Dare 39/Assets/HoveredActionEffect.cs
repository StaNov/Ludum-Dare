using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveredActionEffect : MonoBehaviour
{
	public StatsDifference Effect;
	
	public void SetDifference(StatsDifference difference) {
		Effect = difference.Clone() as StatsDifference;
	}

	public void ResetDifference()
	{
		Effect = new StatsDifference();
	}
}
