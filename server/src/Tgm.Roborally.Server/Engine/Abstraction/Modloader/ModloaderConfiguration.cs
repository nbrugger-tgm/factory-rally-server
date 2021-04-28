#nullable enable
using Tgm.Roborally.Server.Engine.Abstraction.Managers;
using Tgm.Roborally.Server.Engine.Managers;

namespace Tgm.Roborally.Server.Engine.Abstraction.Modloader {
	public partial class Modloader {
		private IManagerLoader[] managerMatrix => new[] {
			Loader<IEntityManager>(mod => mod.EntityManager),
			Loader<IMovementManager>(mod => mod.MovementManager),
			Loader<IEventManager>(mod => mod.EventManager),
			Loader<IGameActionHandler>(mod => mod.GameActionHandler),
			Loader<IHardwareManager>(mod => mod.HardwareManager),
			Loader<IUpgradeManager>(mod => mod.UpgradeManager),
			Loader<IProgrammingManager>(mod => mod.ProgrammingManager)
		};
		/// <summary>
		/// Returns the Manager implementation provided by mods
		/// </summary>
		public IEventManager? EventManager(GameLogic gameLogic, IEventManager? oldManager) =>
			GetManager<IEventManager>();

		/// <inheritdoc cref="EventManager"/>
		public IGameActionHandler? GameActionHandler(GameLogic gameLogic, IGameActionHandler? oldManager) =>
			GetManager<IGameActionHandler>();

		/// <inheritdoc cref="EventManager"/>
		public IHardwareManager? HardwareManager(GameLogic gameLogic, IHardwareManager? oldManager) =>
			GetManager<IHardwareManager>();

		/// <inheritdoc cref="EventManager"/>
		public IEntityManager? EntityManager(GameLogic gameLogic, IEntityManager? oldManager) =>
			GetManager<IEntityManager>();

		/// <inheritdoc cref="EventManager"/>
		public IUpgradeManager? UpgradeManager(GameLogic gameLogic, IUpgradeManager? oldManager) =>
			GetManager<IUpgradeManager>();

		/// <inheritdoc cref="EventManager"/>
		public IProgrammingManager? ProgrammingManager(GameLogic gameLogic, IProgrammingManager? oldManager) =>
			GetManager<IProgrammingManager>();

		/// <inheritdoc cref="EventManager"/>
		public IMovementManager? MovementManager(GameLogic gameLogic, IMovementManager? oldManager) =>
			GetManager<IMovementManager>();

		private void UnloadImplementations() {
			_strategy  = (null, null);
			_gamePhase = (null, null);
		}
	}
}