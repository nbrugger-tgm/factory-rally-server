using Tgm.Roborally.Server.Engine.Abstraction.Managers;
using Tgm.Roborally.Server.Engine.Managers;

namespace Tgm.Roborally.Server.Engine.Abstraction {
	/// <summary>
	/// The default implementation of the game
	/// </summary>
	public class DefaultImplementation : EngineImplementationProvider{
		/// <inheritdoc />
		public IEventManager EventManager(GameLogic gameLogic) => new EventManager(gameLogic);

		/// <inheritdoc />
		public IGameActionHandler GameActionHandler(GameLogic gameLogic) => new GameActionHandler(gameLogic);

		/// <inheritdoc />
		public IHardwareManager HardwareManager(GameLogic gameLogic) => new HardwareManager(gameLogic);

		/// <inheritdoc />
		public IEntityManager EntityManager(GameLogic gameLogic) => new EntityManager(gameLogic);

		/// <inheritdoc />
		public IUpgradeManager UpgradeManager(GameLogic gameLogic) => new UpgradeManager(gameLogic);

		/// <inheritdoc />
		public IProgrammingManager ProgrammingManager(GameLogic gameLogic) => new ProgrammingManager(gameLogic);
	}
}