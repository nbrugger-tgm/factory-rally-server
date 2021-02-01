using Microsoft.AspNetCore.Routing;
using Tgm.Roborally.Server.Engine;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Authentication {
	public class RobotOwnerShipEnsurance : OwnershipEnsurance {
		public bool DoesOwn(int playerId, RouteValueDictionary path, GameLogic gameLogic) {
			int    roboId = (int) path["robot_id"];
			Player p      = gameLogic.GetPlayer(playerId);
			return p.ControlledEntities.Contains(roboId);
		}
	}
}