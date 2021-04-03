using Microsoft.AspNetCore.Routing;
using Tgm.Roborally.Server.Engine;

namespace Tgm.Roborally.Server.Authentication {
	
	/// <summary>
	/// Implement this interface in order to add an ownership check option for GameAuth
	/// </summary>
	public interface OwnershipEnsurance {
		/// <summary>
		///     This method ensures that a specific player is allowed to access a certain path/resource.
		/// </summary>
		/// <param name="playerId">the players id to test the permissions against</param>
		/// <param name="path">the path to test if the player has permissions on</param>
		/// <param name="gameLogic"></param>
		/// <returns>true if the player owns the resource</returns>
		public bool DoesOwn(int playerId, RouteValueDictionary path, GameLogic gameLogic);
	}
}