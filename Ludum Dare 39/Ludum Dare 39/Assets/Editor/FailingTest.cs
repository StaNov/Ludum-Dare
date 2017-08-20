using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Failingtest {

	[Test]
	public void FailingtestSimplePasses() {
		Assert.IsTrue(false);
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator FailingtestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		for (int i = 0; i < 180; i++)
			yield return null;

		Assert.IsTrue(false);
	}
}
