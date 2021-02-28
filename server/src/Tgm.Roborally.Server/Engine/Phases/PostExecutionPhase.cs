using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class PostExecutionPhase : GamePhase {
		private GameLogic _game;

		protected override object Information => new {
			register = _game.executionState.CurrentRegister
		};

		public override GameState NewState => GameState.PLAYING;

		protected override GamePhase Run(GameLogic game) {
			_game = game;
			int register = game.executionState.CurrentRegister++;
			if (register >= 9) return new GameEndPhase();

			game.executionState.CurrentRobot = 0;
			return new PreExecutionPhase();
		}

		public override void Notify(ActionType action) {
			//TODO
		}

		public override bool Notify(GenericEvent action) => false;

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) =>
			throw new NotImplementedException();
	}
}