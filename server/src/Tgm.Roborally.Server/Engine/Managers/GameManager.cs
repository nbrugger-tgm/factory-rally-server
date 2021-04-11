using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Engine.Abstraction;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	/// <summary>
	/// Manages all running games. Only one instance allowed
	/// </summary>
	public class GameManager {
		private readonly Modloader _loader = new Modloader();

		/// <summary>
		/// The list of all running games
		/// </summary>
		public readonly Dictionary<int, GameLogic> Games = new Dictionary<int, GameLogic>();

		/// <summary>
		/// Creates the first and only game manager with the specified Impl.Provider.<br/>
		/// <b>Can only be called ONCE</b>
		/// </summary>
		/// <param name="impl">the implementation provider</param>
		/// <exception cref="ApplicationException">If called more than one time</exception>
		public static void Init(EngineImplementationProvider impl) {
			if (_instance == null)
				_instance = new GameManager(impl);
			else
				throw new ApplicationException("Game Manager can only be instanciated once");
		}

		private readonly EngineImplementationProvider _implementationProvider;
		private static   GameManager                  _instance;

		private GameManager(EngineImplementationProvider implementationProvider) {
			_implementationProvider = implementationProvider;
		}

		/// <summary>
		/// The GameManger instance
		/// </summary>
		public static GameManager Instance => _instance;

		/// <summary>
		/// Generates a random ID from 0 to 2048
		/// </summary>
		public static int RandomId => new Random().Next(2048);

		/// <summary>
		/// The modloader
		/// </summary>
		public Modloader ModLoader => _loader;

		/// <summary>
		/// Create a new Game with a random id
		/// </summary>
		/// <param name="rules">The rules that apply for this game session</param>
		/// <returns> the id of the created game </returns>
		public int CreateGame(GameRules rules) {
			Console.WriteLine("Create game with rules : " + rules);
			int       id   = RandomId;
			GameLogic game = new(rules) {id = id};//blame microsoft this syntax is  .... special
			_loader.LoadMods(game);
			game.UseImplementation(_loader, _loader.StartingPhase);
			_loader.managers.ForEach(e => e.Setup());
			_loader.ItemLoadingStartegy.LoadAllItems(
				_loader.ItemLoaders,
				game.Upgrades.AddToDeck,
				game.Programming.AddCard,
				() => game.Upgrades.Deck.Count,
				() => game.Programming.Deck.Count);
			Games[id] = game;
			game.StartThread();
			return id;
		}

		/// <summary>
		/// Returns the game for the given id or <code>null</code> if nothing was found
		/// </summary>
		/// <param name="gameId">the id of the game</param>
		/// <returns>the game itself or null</returns>
		public GameLogic GetGame(int gameId) {
			return Games.ContainsKey(gameId) ? Games[gameId] : null;
		}
	}
}