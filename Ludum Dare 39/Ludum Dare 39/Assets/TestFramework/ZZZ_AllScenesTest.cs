using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


// "ZZZ" to run it as last because we lost the initial scene afterwards
// I tried this, but did some weird stuff:
// SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings);
public class ZZZ_AllScenesTest {


	// Those values are tweaked so it loads each scene.
	// Too big wait time seems to cause not loading second scene.
	private const float TimeScale = 20;
	private const float WaitSecondsEachScene = 10;

	private float OriginalTimeScale = 1;
	private float OriginalVolume = 1;


	[UnityTest]
	public IEnumerator LoadEverySceneAndLetItGoForSomeTime () {
		OriginalTimeScale = Time.timeScale;
		OriginalVolume = AudioListener.volume;

		Time.timeScale = TimeScale;
		AudioListener.volume = 0;

		// last scene is the testing initial one, but it doesn't seem to work - it reloads all other scenes multiple times
		int scenesInBuildSettings = SceneManager.sceneCountInBuildSettings - 1;

		for(int i = 0; i < scenesInBuildSettings; i++)
		{
			SceneManager.LoadScene(i);
			yield return new WaitForSeconds(WaitSecondsEachScene);
		}
	}

	[UnityTest]
	public IEnumerator ZZZ_TearDown()
	{
		Time.timeScale = OriginalTimeScale;
		AudioListener.volume = OriginalVolume;

		yield break;
	}
}
