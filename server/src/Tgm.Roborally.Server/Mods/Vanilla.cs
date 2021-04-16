#nullable enable
using Tgm.Roborally.Server.Engine;
using Tgm.Roborally.Server.Engine.Abstraction;
using Tgm.Roborally.Server.Engine.Abstraction.Adders;
using Tgm.Roborally.Server.Engine.Abstraction.Managers;
using Tgm.Roborally.Server.Engine.GameItems;
using Tgm.Roborally.Server.Engine.Managers;
using Tgm.Roborally.Server.Engine.Phases;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Mods {
	/// <summary>
	/// Vanilla Mod
	/// </summary>
	public class Vanilla : Mod {
		/// <inheritdoc />
		public string Name => "Vanilla Implementation";

		private static readonly IItemLoader           Loader = new VanillaCards();
		private static readonly DefaultImplementation _impl  = new();


		public GamePhase?        StartingPhase(GamePhase?                    arg)  => new LobbyPhase();
		public ILoadingStartegy? GetCustomLoadingStrategy(ILoadingStartegy?  prev) => new VanillaItemLoader();
		public void              AddUpgrades(Add<Upgrade>                    add)  => Loader.AddUpgrades(add);
		public void              AddProgrammingCards(IItemLoader.AddCommands add)  => Loader.AddProgrammingCards(add);

		public IEventManager? EventManager(GameLogic gameLogic, IEventManager? oldManager) =>
			_impl.EventManager(gameLogic, oldManager);

		public IGameActionHandler? GameActionHandler(GameLogic gameLogic, IGameActionHandler? oldManager) =>
			_impl.GameActionHandler(gameLogic, oldManager);

		public IHardwareManager? HardwareManager(GameLogic gameLogic, IHardwareManager? oldManager) =>
			_impl.HardwareManager(gameLogic, oldManager);

		public IEntityManager? EntityManager(GameLogic gameLogic, IEntityManager? oldManager) =>
			_impl.EntityManager(gameLogic, oldManager);

		public IUpgradeManager? UpgradeManager(GameLogic gameLogic, IUpgradeManager? oldManager) =>
			_impl.UpgradeManager(gameLogic, oldManager);

		public IProgrammingManager? ProgrammingManager(GameLogic gameLogic, IProgrammingManager? oldManager) =>
			_impl.ProgrammingManager(gameLogic, oldManager);
	}
}