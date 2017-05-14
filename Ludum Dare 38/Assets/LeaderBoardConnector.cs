using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LeaderBoardConnector {
	
	private const string SECRET_KEY_PRIVATE = "xxx";
	private const string SECRET_KEY_PUBLIC = "xxx";

	public static WWW FetchResultsAll(int lines)
	{
		CheckSecretKey();
		return new WWW("http://dreamlo.com/lb/" + SECRET_KEY_PUBLIC + "/pipe/" + lines);
	}

	public static WWW FetchResultsPlayer(string playerName)
	{
		CheckSecretKey();
		return new WWW("http://dreamlo.com/lb/" + SECRET_KEY_PUBLIC + "/pipe-get/" + Uri.EscapeDataString(playerName));
	}

	public static List<LeaderboardResult> ConvertStringToResult(string wwwResult)
	{
		List<LeaderboardResult> result = new List<LeaderboardResult>();

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
		
		return new WWW("http://dreamlo.com/lb/" + SECRET_KEY_PRIVATE  + "/add/" + Uri.EscapeDataString(name) + "/" + score);
	}

	private static void CheckSecretKey()
	{
		if (SECRET_KEY_PRIVATE == "xxx")
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
