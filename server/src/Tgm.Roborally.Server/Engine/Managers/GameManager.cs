using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine
{
	public class GameManager
	{
		public static GameManager instance { get; } = new GameManager();

		public Dictionary<int, GameLogic> games = new Dictionary<int, GameLogic>();

		private GameManager()
		{
		}

		public int startGame(GameRules rules)
		{
			Console.WriteLine("Create game with rules : " + rules);
			int id = randomID;
			GameLogic game = new GameLogic(rules) {Id = id};
			games[id] = game;
			return id;
		}

		public static int randomID => new Random().Next(2048);

		public GameLogic GetGame(int gameId, ref IActionResult response)
		{
			if (response != null)
				return null;
			if (!games.ContainsKey(gameId))
			{
				response = new NotFoundResult();
				return null;
			}
			return games[gameId];
		}
	}
}