#nullable enable
using Tgm.Roborally.Server.Engine.Abstraction.Adders;
using Tgm.Roborally.Server.Engine.Abstraction.Managers;
using Tgm.Roborally.Server.Engine.Managers;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Abstraction {
	/// <summary>
	/// Provides implementations for the Managers
	/// </summary>
	public interface EngineImplementationProvider {
		/// <summary>
		/// Creates and returns a new Event Manager
		/// </summary>
		/// <param name="gameLogic">the logic to base the manager of</param>
		/// <param name="oldManager">the manager implementation that is currently used. If this is null your mod would be the first one to set the implementation</param>
		/// <returns>the created Manager</returns>
		IEventManager? EventManager(GameLogic gameLogic, IEventManager? oldManager = null) => null;

		/// <summary>
		/// Creates and returns a new GameAction Manager
		/// </summary>
		/// <param name="gameLogic">the logic to base the manager of</param>
		/// <param name="oldManager">the manager implementation that is currently used. If this is null your mod would be the first one to set the implementation</param>
		/// <returns>the created Manager</returns>
		IGameActionHandler? GameActionHandler(GameLogic gameLogic, IGameActionHandler? oldManager = null) => null;

		/// <summary>
		/// Creates and returns a new Hardware Manager
		/// </summary>
		/// <param name="gameLogic">the logic to base the manager of</param>
		/// <param name="oldManager">the manager implementation that is currently used. If this is null your mod would be the first one to set the implementation</param>
		/// <returns>the created Manager</returns>
		IHardwareManager? HardwareManager(GameLogic gameLogic, IHardwareManager? oldManager = null) => null;

		/// <summary>
		/// Creates and returns a new Entity Manager
		/// </summary>
		/// <param name="gameLogic">the logic to base the manager of</param>
		/// <param name="oldManager">the manager implementation that is currently used. If this is null your mod would be the first one to set the implementation</param>
		/// <returns>the created Manager</returns>
		IEntityManager? EntityManager(GameLogic gameLogic, IEntityManager? oldManager = null) => null;

		/// <summary>
		/// Creates and returns a new Upgrade Manager
		/// </summary>
		/// <param name="gameLogic">the logic to base the manager of</param>
		/// <param name="oldManager">the manager implementation that is currently used. If this is null your mod would be the first one to set the implementation</param>
		/// <returns>the created Manager</returns>
		IUpgradeManager? UpgradeManager(GameLogic gameLogic, IUpgradeManager? oldManager = null) => null;

		/// <summary>
		/// Creates and returns a new Programming Manager
		/// </summary>
		/// <param name="gameLogic">the logic to base the manager of</param>
		/// <param name="oldManager">the manager implementation that is currently used. If this is null your mod would be the first one to set the implementation</param>
		/// <returns>the created Manager</returns>
		IProgrammingManager? ProgrammingManager(GameLogic gameLogic, IProgrammingManager? oldManager = null) => null;
		
		/// <summary>
		/// Creates and returns a new Programming Manager
		/// </summary>
		/// <param name="gameLogic">the logic to base the manager of</param>
		/// <param name="oldManager">the manager implementation that is currently used. If this is null your mod would be the first one to set the implementation</param>
		/// <returns>the created Manager</returns>
		IMovementManager? MovementManager(GameLogic gameLogic, IMovementManager? oldManager = null) => null;
	}
}