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
		/// <returns>the created Manager</returns>
		IEventManager       EventManager(GameLogic                 gameLogic);
		/// <summary>
		/// Creates and returns a new GameAction Manager
		/// </summary>
		/// <param name="gameLogic">the logic to base the manager of</param>
		/// <returns>the created Manager</returns>
		IGameActionHandler  GameActionHandler(GameLogic            gameLogic);
		/// <summary>
		/// Creates and returns a new Hardware Manager
		/// </summary>
		/// <param name="gameLogic">the logic to base the manager of</param>
		/// <returns>the created Manager</returns>
		IHardwareManager    HardwareManager(GameLogic              gameLogic);
		/// <summary>
		/// Creates and returns a new Entity Manager
		/// </summary>
		/// <param name="gameLogic">the logic to base the manager of</param>
		/// <returns>the created Manager</returns>
		IEntityManager      EntityManager(GameLogic                gameLogic);
		/// <summary>
		/// Creates and returns a new Upgrade Manager
		/// </summary>
		/// <param name="gameLogic">the logic to base the manager of</param>
		/// <returns>the created Manager</returns>
		IUpgradeManager     UpgradeManager(GameLogic               gameLogic);
		/// <summary>
		/// Creates and returns a new Programming Manager
		/// </summary>
		/// <param name="gameLogic">the logic to base the manager of</param>
		/// <returns>the created Manager</returns>
		IProgrammingManager ProgrammingManager(GameLogic           gameLogic);
	}
}