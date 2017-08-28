using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

public class TestingScenesInBuildPostProcess : IPostprocessBuild
{
	public int callbackOrder { get { return 0; } }

	public void OnPostprocessBuild(BuildTarget target, string path)
	{
		if (ThereAreTestingScenesInBuildSettings())
		{
			Debug.LogError("There are testing scenes in the build. Remove the test scenes and run the build again.");
		}
	}

	private static bool ThereAreTestingScenesInBuildSettings()
	{
		return EditorBuildSettings.scenes.Any(scene => scene.path.IndexOf("test", StringComparison.OrdinalIgnoreCase) > -1);
	}
}
