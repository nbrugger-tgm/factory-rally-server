using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	/// <summary>
	/// Manages all running games. Only one instance allowed
	/// </summary>
	public class GameManager {
		/// <summary>
		/// The list of all running games
		/// </summary>
		public readonly Dictionary<int, GameLogic> games = new Dictionary<int, GameLogic>();

		private GameManager() {
		}

		/// <summary>
		/// The GameManger instance
		/// </summary>
		public static GameManager instance { get; } = new GameManager();

		/// <summary>
		/// Generates a random ID from 0 to 2048
		/// </summary>
		public static int randomID => new Random().Next(2048);

		/// <summary>
		/// Create a new Game with a random id
		/// </summary>
		/// <param name="rules">The rules that apply for this game session</param>
		/// <returns> the id of the created game </returns>
		public int CreateGame(GameRules rules) {
			Console.WriteLine("Create game with rules : " + rules);
			int       id   = randomID;
			GameLogic game = new GameLogic(rules) {id = id};
			games[id] = game;
			return id;
		}

		/// <summary>
		/// Returns the game for the given id or <code>null</code> if nothing was found
		/// </summary>
		/// <param name="gameId">the id of the game</param>
		/// <returns>the game itself or null</returns>
		public GameLogic GetGame(int gameId) {
			if (!games.ContainsKey(gameId)) return null;

			return games[gameId];
		}
	}
}