using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LeaderBoardConnector {

	private const string SECRET_KEY = "xxx";
	private const int LINES = 2;

	public static LeaderboardResult[] Load(string currentPlayerName) {
		CheckSecretKey();

		List<LeaderboardResult> result = FetchResultsAll();

		if (! result.Any(r => r.name == currentPlayerName))
		{
			result.AddRange(FetchResultsPlayer(currentPlayerName));
		}
		
		return result.ToArray();
	}

	private static List<LeaderboardResult> FetchResultsAll()
	{
		return FetchResults("http://dreamlo.com/lb/" + SECRET_KEY + "/pipe/" + LINES);
	}

	private static List<LeaderboardResult> FetchResultsPlayer(string playerName)
	{
		return FetchResults("http://dreamlo.com/lb/" + SECRET_KEY + "/pipe-get/" + playerName);
	}

	private static List<LeaderboardResult> FetchResults(string url)
	{
		WWW www = new WWW(url);

		while (!www.isDone) { }

		List<LeaderboardResult> result = new List<LeaderboardResult>();

		if (www.error != null)
		{
			return result;
		}

		string wwwResult = www.text;

		string[] lines = wwwResult.Split('\n');

		foreach (string line in lines)
		{
			if (string.IsNullOrEmpty(line))
			{
				continue;
			}

			string[] lineParts = line.Split('|');

			LeaderboardResult convertedLine = new LeaderboardResult
			{
				place = int.Parse(lineParts[5]) + 1,
				name = lineParts[0],
				score = int.Parse(lineParts[1])
			};

			result.Add(convertedLine);
		}

		return result;
	}
	
	public static WWW Save(string name, int score) {
		CheckSecretKey();

		return new WWW("http://dreamlo.com/lb/" + SECRET_KEY + "/add/" + name + "/" + score);
	}

	private static void CheckSecretKey()
	{
		if (SECRET_KEY == "xxx")
		{
			throw new System.Exception("SECRET KEY WAS NOT REPLACED!!!");
		}
	}

	public class LeaderboardResult
	{
		public int place;
		public string name;
		public int score;
	}
}
