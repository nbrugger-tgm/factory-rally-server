using Tgm.Roborally.Server.Engine.Abstraction.Managers;

namespace Tgm.Roborally.Server.Engine.Managers {
	/// <inheritdoc />
	public class HardwareManager : IHardwareManager {
		private readonly GameLogic _game;

		public HardwareManager(GameLogic game) {
			_game = game;
		}

		/// <summary>
		/// True if the game can be used with a board
		/// </summary>
		public bool Compatible => _game.MaxPlayers <= 4 && _game.Rules.RobotsPerPlayer == 1;
	}
}