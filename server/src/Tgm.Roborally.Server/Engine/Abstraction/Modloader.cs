#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Tgm.Roborally.Server.CSExtensions;
using Tgm.Roborally.Server.Engine.Abstraction.Adders;
using Tgm.Roborally.Server.Engine.Abstraction.Managers;
using Tgm.Roborally.Server.Engine.GameItems;
using Tgm.Roborally.Server.Engine.Managers;

namespace Tgm.Roborally.Server.Engine.Abstraction {
	public partial class Modloader {
		private readonly IList<Mod> _mods = new List<Mod>();
		private          (ILoadingStartegy? implementantion, string? modName) _strategy;
		private          (GamePhase? implementantion, string? modName) _gamePhase;
		public           IImmutableList<Mod> Mods => _mods.ToImmutableList();
		public           GamePhase? StartingPhase => _gamePhase.implementantion;

		/// <summary>
		/// the ItemLoadingStrategy loaded from mods
		/// </summary>
		public ILoadingStartegy? ItemLoadingStartegy => _strategy.implementantion;

		/// <summary>
		/// The item loader mapped to the name of the mod that loaded it
		/// </summary>
		public Dictionary<string, IItemLoader> ItemLoaders =>
			Mods.ToDictionary(pair => pair.Name, pair => pair as IItemLoader);

		/// <summary>
		/// Adds a mod to the list of mods. BUT IT DOES NOT LOADS the mod. Therefore see <see cref="LoadMods"/>
		/// </summary>
		/// <param name="m">the mod to add</param>
		/// <exception cref="ArgumentException">Thrown when the modnames conflict</exception>
		public void AddMod(Mod m) {
			if (_mods.Select(e => e.Name).Contains(m.Name))
				throw new ArgumentException("Each Mod-name shall be unique. The Mod : " + m.Name + " was added twice!");
			_mods.Add(m);
		}

		/// <summary>
		/// Called by the game in the case of an event
		/// </summary>
		/// <param name="logic">the game that called the function and the event appeared</param>
		/// <param name="ev">the fired event</param>
		public void OnEvent(GameLogic logic, Event ev) {
			_mods.ForEach(e => e.OnEvent(logic, ev));
		}

		/// <summary>
		/// Reads mods from DLL files 
		/// </summary>
		/// <returns>the mods from the DLL files</returns>
		public Mod[] Load() {
			//TODO: some dll reflection magic and return the mods
			return new Mod[] { };
		}
	}

	#region EngineImplementationProvider

	public partial class Modloader : EngineImplementationProvider {
		private (IEventManager? manager, string? owner)       _eventManager;
		private (IGameActionHandler? manager, string? owner)  _gameActionHandler;
		private (IHardwareManager? manager, string? owner)    _hwManager;
		private (IEntityManager? manager, string? owner)      _entityManager;
		private (IUpgradeManager? manager, string? owner)     _upgradeManager;
		private (IProgrammingManager? manager, string? owner) _programmingManager;

		/// <summary>
		/// All loaded managers
		/// </summary>
		public List<IManager?> managers => new() {
			_eventManager.manager,
			_gameActionHandler.manager,
			_hwManager.manager,
			_entityManager.manager,
			_upgradeManager.manager,
			_programmingManager.manager
		};

		private delegate T? ManagerImplementationProvider<T>(GameLogic logic, T? prev);

		public IEventManager? EventManager(GameLogic gameLogic, IEventManager? oldManager) => _eventManager.manager;

		public IGameActionHandler? GameActionHandler(GameLogic gameLogic, IGameActionHandler? oldManager) =>
			_gameActionHandler.manager;

		public IHardwareManager? HardwareManager(GameLogic gameLogic, IHardwareManager? oldManager) =>
			_hwManager.manager;

		public IEntityManager? EntityManager(GameLogic gameLogic, IEntityManager? oldManager) => _entityManager.manager;

		public IUpgradeManager? UpgradeManager(GameLogic gameLogic, IUpgradeManager? oldManager) =>
			_upgradeManager.manager;

		public IProgrammingManager? ProgrammingManager(GameLogic gameLogic, IProgrammingManager? oldManager) =>
			_programmingManager.manager;
	}

	#endregion


	#region Load modded replacements

	public partial class Modloader {
		/// <summary>
		/// Loads the important aspects of the mod
		/// </summary>
		public void LoadMods(GameLogic logic) {
			foreach (Mod mod in Mods) {
				mod.BevoreLoad();

				LoadModOverwrite(mod, mod.GetCustomLoadingStrategy, "loading strategy", ref _strategy);
				LoadModOverwrite(mod, mod.StartingPhase, "Game Cycle/StartingPhase", ref _gamePhase);
				LoadModOverwrite(mod, logic, (gameLogic, oldManager) => mod.EntityManager(gameLogic, oldManager),
								 "Entity Manager", ref _entityManager);
				LoadModOverwrite(mod, logic, (gameLogic, oldManager) => mod.EventManager(gameLogic, oldManager),
								 "Event Manager", ref _eventManager);
				LoadModOverwrite(mod, logic, (gameLogic, oldManager) => mod.GameActionHandler(gameLogic, oldManager),
								 "GameAction Manager", ref _gameActionHandler);
				LoadModOverwrite(mod, logic, (gameLogic, oldManager) => mod.HardwareManager(gameLogic, oldManager),
								 "Hardware Manager", ref _hwManager);
				LoadModOverwrite(mod, logic, (gameLogic, oldManager) => mod.UpgradeManager(gameLogic, oldManager),
								 "Upgrade Manager", ref _upgradeManager);
				LoadModOverwrite(mod, logic, (gameLogic, oldManager) => mod.ProgrammingManager(gameLogic, oldManager),
								 "Programming Manager", ref _programmingManager);

				//TODO Load other things
				mod.Loaded();
			}

			((object?, string?), string)[] lst = {
				(_strategy, "loading strategy"),
				(_gamePhase, "Game Cycle/StartingPhase"),
				(_entityManager, "Entity Manager"),
				(_eventManager, "Event Manager"),
				(_gameActionHandler, "GameAction Manager"),
				(_hwManager, "Hardware Manager"),
				(_upgradeManager, "Upgrade Manager"),
				(_programmingManager, "Programming Manager")
			};
			foreach (((object?, string?) val, var topic) in lst)
				CheckImplementation(val, topic);
		}

		private void CheckImplementation((object?, string?) checking, string topic) {
			if (checking.Item1 == null)
				throw new ApplicationException($"There was no mod loading a {topic}!");
		}

		private static void LoadModOverwrite<T>(Mod mod, Func<T?, T?> getCustomLoadingStrategy, string topic,
												ref (T? strategy, string? loadStrategyOwner) start) {
			T? modStrategy = getCustomLoadingStrategy(start.strategy);
			ReplaceIfNotNull(mod, topic, ref start.strategy, ref start.loadStrategyOwner, modStrategy);
		}

		private static void LoadModOverwrite<T>(Mod mod, GameLogic logic,
												ManagerImplementationProvider<T> getCustomLoadingStrategy, string topic,
												ref (T? strategy, string? loadStrategyOwner) yeet) {
			T? modStrategy = getCustomLoadingStrategy(logic, yeet.strategy);
			ReplaceIfNotNull(mod, topic, ref yeet.strategy, ref yeet.loadStrategyOwner, modStrategy);
		}

		private static void ReplaceIfNotNull<T>(Mod mod, string topic, ref T strategy, ref string? loadStrategyOwner,
												T   modStrategy) {
			if (strategy != null && modStrategy != null)
				Console.Out.WriteLine(
					$"[Warning] \"{mod.Name}\" overwrote the {topic} of \"{loadStrategyOwner}\"");
			strategy          = modStrategy ?? strategy;
			loadStrategyOwner = modStrategy != null ? mod.Name : loadStrategyOwner;
		}
	}

	#endregion
}