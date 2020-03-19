using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine
{
	public class HardwareManager
	{
		public string ip = null;
		private GameLogic _game;

		public HardwareManager(GameLogic game)
		{
			_game = game;
		}

		public bool HardwareConnected => ip != null && Compatible;
		public bool Compatible => _game.MaxPlayers <= 4;
	}
}