#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Transactions;
using Tgm.Roborally.Server.CSExtensions;
using Tgm.Roborally.Server.Engine.Abstraction.Adders;
using Tgm.Roborally.Server.Engine.Abstraction.Managers;

namespace Tgm.Roborally.Server.Engine.Abstraction.Modloader {

	public partial class Modloader {
		private readonly IList<Mod> _mods = new List<Mod>();
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
		/// Reads mods from DLL files 
		/// </summary>
		/// <returns>the mods from the DLL files</returns>
		public Mod[] Load() {
			//TODO: some dll reflection magic and return the mods
			return new Mod[] { };
		}
		
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

		

	}

	#region EngineImplementationProvider

	public partial class Modloader : EngineImplementationProvider {
		private readonly Dictionary<Type, (IManager? manager, string? owner)> _managers;
		private          (ILoadingStartegy? implementantion, string? modName) _strategy;
		private          (GamePhase? implementantion, string? modName)        _gamePhase;/// <summary>
		/// All loaded managers
		/// </summary>
		public IImmutableList<IManager?> Managers => _managers.Select(e => e.Value.manager).ToImmutableList();
		public Modloader() {
			_managers = Enumerable.ToDictionary<Type,Type,(IManager?,string?)>(managerMatrix
																				   .Select(e=>e.Type), e => e,
																			   e => (null, null)
			);
		}
		private T?           GetManager<T>() where T : class, IManager => _managers[typeof(T)].manager as T;
		private (T?,string?) GetManagerTuple<T>() where T : IManager   => ((T?, string?)) _managers[typeof(T)];
		private void SetManagerTuple<T>((T?,string?) data) where T : IManager   => _managers[typeof(T)] = data;
		

		

		
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
				LoadMod(logic, mod);
				mod.Loaded();
			}

			List<(Type,(object?, string?))> lst = new() {
				(typeof(ILoadingStartegy),_strategy),
				(typeof(GamePhase),_gamePhase)
			};
			_managers.ForEach(r => lst.Add((r.Key,r.Value)));
			foreach ((Type,(object?, string?)) val in lst)
				CheckImplementation(val);
		}

		private void LoadMod(GameLogic logic, Mod mod) {
			//TODO Load other implementations
			LoadModOverwrite(mod.Name, mod.GetCustomLoadingStrategy, "loading strategy", ref _strategy);
			LoadModOverwrite(mod.Name, mod.StartingPhase, "Game Cycle/StartingPhase", ref _gamePhase);
			foreach (var managerLoader in managerMatrix) {
				managerLoader.Load(mod,logic);
			}
		}
		


		private IManagerLoader Loader<T>(Func<Mod, ManagerImplementationProvider<T>> entityManager) where T : IManager => new ManagerLoader<T>(this, entityManager);

		private void CheckImplementation((Type type, (object? impl, string? mod) value) checking) {
			if (checking.value.impl == null)
				throw new ApplicationException($"There was no mod loading a {checking.type.Name}!");
		}

		private static void LoadModOverwrite<T>(string                                       mod,
												Func<T?, T?>                                 loadStategy,
												string                                       topic,
												ref (T? strategy, string? owner) start) {
			T? modStrategy = loadStategy(start.strategy);
			ReplaceIfNotNull(mod, topic, ref start.strategy, ref start.owner, modStrategy);
		}

		private static void LoadModOverwrite<T>(string mod,
												GameLogic logic,
												ManagerImplementationProvider<T> load,
												ref (T? strategy, string? owner) yeet) where T : IManager{
			T? modStrategy = load(logic, yeet.strategy);
			ReplaceIfNotNull(mod, ref yeet.strategy, ref yeet.owner, modStrategy);
		}

		private static void ReplaceIfNotNull<T>(string         mod,
												string      topic,
												ref T? strategy,
												ref string? loadStrategyOwner,
												T?     modStrategy) {
			if (strategy != null && modStrategy != null)
				Console.Out.WriteLine(
					$"[Warning] \"{mod}\" overwrote the {topic} of \"{loadStrategyOwner}\"");
			strategy          = modStrategy ?? strategy;
			loadStrategyOwner = modStrategy != null ? mod : loadStrategyOwner;
		}
		private static void ReplaceIfNotNull<T>(string      mod,
											 ref T?      strategy,
											 ref string? loadStrategyOwner,
											 T?          modStrategy) where T : IManager {
			ReplaceIfNotNull(mod, strategy?.Name??"Null", ref strategy, ref loadStrategyOwner, modStrategy);
		}
	}


	#endregion
}