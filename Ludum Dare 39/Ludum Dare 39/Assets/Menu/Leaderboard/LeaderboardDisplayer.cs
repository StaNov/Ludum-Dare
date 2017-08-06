using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class LeaderboardDisplayer : MonoBehaviour
{
	public GameObject LinePrefab;

	IEnumerator Start ()
	{
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}
		
		WWW www = new WWW("http://dreamlo.com/lb/59875abbb0b05d1ad4be0123/pipe/10");

		while (! www.isDone)
			yield return null;

		List<Line> lines = ParseLines(www.text.Trim());

		if (! string.IsNullOrEmpty(PlayerNameManager.PlayerName) && ! lines.Any(line => line.PlayerName.Equals(PlayerNameManager.PlayerName)))
		{
			www = new WWW("http://dreamlo.com/lb/59875abbb0b05d1ad4be0123/pipe-get/" + WWW.EscapeURL(PlayerNameManager.PlayerName));

			while (!www.isDone)
				yield return null;
			
			lines.AddRange(ParseLines(www.text.Trim()));
		}
			
		InstantiateLeaderboardLines(lines);
	}

	private void InstantiateLeaderboardLines(List<Line> lines)
	{
		foreach (Line line in lines)
		{
			var leaderBoardLine = Instantiate(LinePrefab).GetComponent<LeaderBoardLine>();
			leaderBoardLine.SetValues(line.Place, line.PlayerName, line.Score);
			leaderBoardLine.transform.SetParent(transform, false);
		}
	}

	private List<Line> ParseLines(string text)
	{
		if (string.IsNullOrEmpty(text))
			return new List<Line>();
		
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

		return result;
	}

	private class Line
	{
		public int Place;
		public string PlayerName;
		public int Score;
	}
}
