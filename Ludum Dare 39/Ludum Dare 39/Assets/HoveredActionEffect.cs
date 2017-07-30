using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class HoveredActionEffect : MonoBehaviour
{
	public StatsDifference Effect
	{
		get { return m_Effect; }
		set { m_Effect = value.Clone() as StatsDifference; }
	}

	private StatsDifference m_Effect = new StatsDifference();

	public void ResetDifference()
	{
		m_Effect = new StatsDifference();
	}
}
