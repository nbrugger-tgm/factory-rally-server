using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class PreExecutionPhase : GamePhase {
		private GameLogic _game;

		protected override object Information => new {
			register = _game.executionState.CurrentRegister
		};

		public override GameState NewState => GameState.PLAYING;

		protected override GamePhase Run(GameLogic game) {
			_game = game;
			return new PreStatementPhase();
		}

		public override void Notify(ActionType action) {
			//TODO
		}

		public override bool Notify(GenericEvent ev) => false;

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) =>
			throw new NotImplementedException();
	}
}