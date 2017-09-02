using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public abstract class AbstractTest
{
	private float OriginalTimeScale = 1;
	private float OriginalVolume = 1;

	[UnityTest]
	public IEnumerator AAA_SetupClass()
	{
		OriginalTimeScale = Time.timeScale;
		OriginalVolume = AudioListener.volume;
		BeforeClass();

		yield break;
	}

	// Called at the start of each test
	protected IEnumerator Setup ()
	{
		yield return DestroyAllObjectsInScene();

		// wait until the objects are destroyed
		yield return null;

		SetupSpecific();
	}

	protected virtual void SetupSpecific() { }
	protected virtual void BeforeClass() { }
	protected virtual void AfterClass() { }

	private static IEnumerator DestroyAllObjectsInScene()
	{
		foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
		{
			if (o.name != "Code-based tests runner")
				Object.Destroy(o);
		}

		// wait a frame so the objects are really destroyed
		yield return null;
	}

	[UnityTest]
	public IEnumerator ZZZ_TearDownClass()
	{
		AfterClass();

		Time.timeScale = OriginalTimeScale;
		AudioListener.volume = OriginalVolume;

		yield break;
	}
}
