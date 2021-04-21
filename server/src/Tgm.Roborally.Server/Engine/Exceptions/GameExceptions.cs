using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Exceptions {
	public class GameExceptions {
		public static ErrorMessage MAP_NOT_EXISTING = new ErrorMessage() {
			Error   = "Map doesn't exists",
			Message = "The map does not exists at the moment"
		};
	}
}