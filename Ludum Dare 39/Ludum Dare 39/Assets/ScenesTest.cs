using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ScenesTest {

	// TODO [UnityTest]
	public IEnumerator LoadScenesAndLetThemGoForSomeTime () {
		for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
		{
			SceneManager.LoadScene(i);
			yield return new WaitForSeconds(5);

			var operation = SceneManager.UnloadSceneAsync(i);
			while(! operation.isDone)
			{
				yield return null;
			}
		}
	}
}
