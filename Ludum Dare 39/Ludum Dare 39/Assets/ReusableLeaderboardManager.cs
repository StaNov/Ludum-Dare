using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ReusableLeaderboardManager {

	public static IEnumerator GetLeaderboard(
		string leaderboardId, 
		string currentPlayerName, 
		Action<int, string, int> forEachLine)
	{
		WWW www = new WWW("http://dreamlo.com/lb/" + leaderboardId + "/pipe/10");

		while (! www.isDone)
			yield return null;

		List<Line> lines = ParseLines(www.text.Trim());

		if (! string.IsNullOrEmpty(currentPlayerName) && ! lines.Any(line => line.PlayerName.Equals(currentPlayerName)))
		{
			www = new WWW("http://dreamlo.com/lb/" + leaderboardId + "/pipe-get/" + WWW.EscapeURL(currentPlayerName));

			while (!www.isDone)
				yield return null;
			
			lines.AddRange(ParseLines(www.text.Trim()));
		}
			
		foreach (Line line in lines)
		{
			forEachLine(line.Place, line.PlayerName, line.Score);
		}
	}

	private static List<Line> ParseLines(string text)
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
