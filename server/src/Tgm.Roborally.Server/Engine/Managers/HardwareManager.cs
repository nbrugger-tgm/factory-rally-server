namespace Tgm.Roborally.Server.Engine {
	public class HardwareManager {
		private readonly GameLogic _game;

		public HardwareManager(GameLogic game) {
			_game = game;
		}

		public bool Compatible => _game.MaxPlayers <= 4 && _game.Rules.RobotsPerPlayer == 1;
	}
}