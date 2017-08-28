using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ZZZ_ScenesTest {

	[UnityTest]
	public IEnumerator LoadScenesAndLetThemGoForSomeTime () {
		Time.timeScale = 1;

		for(int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++)
		{
			var operation = SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
			while (!operation.isDone)
			{
				yield return null;
			}
			operation.allowSceneActivation = true;

			yield return new WaitForSeconds(5);

			operation = SceneManager.UnloadSceneAsync(i);
			while(! operation.isDone)
			{
				yield return null;
			}
		}
	}
}
