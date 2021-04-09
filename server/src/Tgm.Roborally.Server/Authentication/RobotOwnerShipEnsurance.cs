using Microsoft.AspNetCore.Routing;
using Tgm.Roborally.Server.Engine;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Authentication {
	/// <summary>
	/// Checks if the requested robot is owned by the player
	/// </summary>
	public class RobotOwnerShipEnsurance : OwnershipEnsurance {
		private string robotIdPathVariable;

		public RobotOwnerShipEnsurance(string robotIdPathVariable) {
			this.robotIdPathVariable = robotIdPathVariable;
		}

		public RobotOwnerShipEnsurance():this("robot_id") {}

		public bool DoesOwn(int playerId, RouteValueDictionary path, GameLogic gameLogic) {
			int    roboId = (int) path[robotIdPathVariable];
			Player p      = gameLogic.GetPlayer(playerId);
			return p.ControlledEntities.Contains(roboId);
		}
	}
}