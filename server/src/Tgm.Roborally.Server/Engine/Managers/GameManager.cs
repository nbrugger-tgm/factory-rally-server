using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	public class GameManager {
		public Dictionary<int, GameLogic> games = new Dictionary<int, GameLogic>();

		private GameManager() {
		}

		public static GameManager instance { get; } = new GameManager();

		public static int randomID => new Random().Next(2048);

		/// <summary>
		public int CreateGame(GameRules rules) {
			Console.WriteLine("Create game with rules : " + rules);
			int       id   = randomID;
			GameLogic game = new GameLogic(rules) {id = id};
			games[id] = game;
			return id;
		}

		public GameLogic GetGame(int gameId) {
			if (!games.ContainsKey(gameId)) return null;

			return games[gameId];
		}
	}
}