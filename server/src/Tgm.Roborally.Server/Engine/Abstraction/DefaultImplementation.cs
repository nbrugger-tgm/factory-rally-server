#nullable enable
using Tgm.Roborally.Server.Engine.Abstraction.Adders;
using Tgm.Roborally.Server.Engine.Abstraction.Managers;
using Tgm.Roborally.Server.Engine.GameItems;
using Tgm.Roborally.Server.Engine.Managers;
using Tgm.Roborally.Server.Engine.Phases;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Abstraction {
	/// <summary>
	/// The default implementation of the game
	/// </summary>
	public class DefaultImplementation : EngineImplementationProvider {
		/// <inheritdoc />
		public IEventManager? EventManager(GameLogic gameLogic, IEventManager? oldManager) =>
			new EventManager(gameLogic);

		/// <inheritdoc />
		public IGameActionHandler? GameActionHandler(GameLogic gameLogic, IGameActionHandler? oldManager) =>
			new GameActionHandler(gameLogic);

		/// <inheritdoc />
		public IHardwareManager? HardwareManager(GameLogic gameLogic, IHardwareManager? oldManager) =>
			new HardwareManager(gameLogic);

		/// <inheritdoc />
		public IEntityManager? EntityManager(GameLogic gameLogic, IEntityManager? oldManager) =>
			new EntityManager(gameLogic);

		/// <inheritdoc />
		public IUpgradeManager? UpgradeManager(GameLogic gameLogic, IUpgradeManager? oldManager) =>
			new UpgradeManager(gameLogic);

		/// <inheritdoc />
		public IProgrammingManager? ProgrammingManager(GameLogic gameLogic, IProgrammingManager? oldManager) =>
			new ProgrammingManager(gameLogic);
		
		/// <inheritdoc />
		public IMovementManager? MovementManager(GameLogic gameLogic, IMovementManager? oldManager) => 
			new MovementManager(gameLogic);
	}
}