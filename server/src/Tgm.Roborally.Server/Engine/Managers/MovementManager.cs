using Tgm.Roborally.Server.Engine.Statement;

namespace Tgm.Roborally.Server.Engine {
	public class MovementManager {
		private readonly GameLogic _game;

		public MovementManager(GameLogic game) {
			_game = game;
		}

		public void Move(int robotId, int i, RelativeDirection forward) {
			throw new System.NotImplementedException();
		}
	}
}