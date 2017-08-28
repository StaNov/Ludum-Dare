using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ZZZ_ScenesTest {

	[UnityTest]
	public IEnumerator LoadScenesAndLetThemGoForSomeTime () {
		Time.timeScale = 20;

		for(int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++)
		{
			SceneManager.LoadScene(i);
			yield return new WaitForSeconds(10);
		}
	}
}
