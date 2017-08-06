using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LeaderboardDisplayer : MonoBehaviour
{
	public GameObject LinePrefab;

	IEnumerator Start () {
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}
		
		WWW www = new WWW("http://dreamlo.com/lb/59875abbb0b05d1ad4be0123/pipe/10");

		while (! www.isDone)
			yield return null;

		Line[] lines = ParseLines(www.text.Trim());

		InstantiateLeaderboardLines(lines);
		
		if (! lines.Any(line => line.PlayerName.Equals("StaNov" /* TODO */)))
		{
			www = new WWW("http://dreamlo.com/lb/59875abbb0b05d1ad4be0123/pipe-get/StaNov" /* TODO */);

			while (!www.isDone)
				yield return null;
			
			lines = ParseLines(www.text.Trim());
			
			InstantiateLeaderboardLines(lines);
		}
	}

	private void InstantiateLeaderboardLines(Line[] lines)
	{
		foreach (Line line in lines)
		{
			var leaderBoardLine = Instantiate(LinePrefab).GetComponent<LeaderBoardLine>();
			leaderBoardLine.SetValues(line.Place, line.PlayerName, line.Score);
			leaderBoardLine.transform.SetParent(transform, false);
		}
	}

	private Line[] ParseLines(string text)
	{
		if (string.IsNullOrEmpty(text))
			return new Line[0];
		
		string[] linesStrings = text.Split('\n');
		
		List<Line> result = new List<Line>();

		foreach (string line in linesStrings)
		{
			string[] fields = line.Split('|');
			Line newLine = new Line
			{
				Place = int.Parse(fields[5]) + 1,
				PlayerName = fields[0],
				Score = int.Parse(fields[1])
			};
			result.Add(newLine);
		}

		return result.ToArray();
	}

	private class Line
	{
		public int Place;
		public string PlayerName;
		public int Score;
	}
}
