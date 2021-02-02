using Microsoft.AspNetCore.Mvc;

namespace Tgm.Roborally.Server.Engine.Managers {
	public class ProgrammingManager {
		private GameLogic _game;

		public ProgrammingManager(GameLogic game) {
			_game = game;
		}

		public void Clear(int robotId) {
			throw new System.NotImplementedException();
		}

		public IActionResult this[int robotId] {
			get { throw new System.NotImplementedException(); }
		}
	}
}