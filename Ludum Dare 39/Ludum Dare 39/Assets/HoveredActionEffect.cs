using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class HoveredActionEffect : MonoBehaviour
{
	public StatsDifference Effect
	{
		get { return m_Effect; }
	}

	public float /*TODO int*/ TimeToFinish
	{
		get { return m_TimeToFinish; }
	}

	public void SetEffect(StatsDifference difference, float timeToFinish)
	{
		m_Effect = difference.Clone() as StatsDifference;
		m_TimeToFinish = timeToFinish;
	}

	private StatsDifference m_Effect = new StatsDifference();
	private float m_TimeToFinish;

	public void ResetDifference()
	{
		m_Effect = new StatsDifference();
		m_TimeToFinish = 0;
	}
}
