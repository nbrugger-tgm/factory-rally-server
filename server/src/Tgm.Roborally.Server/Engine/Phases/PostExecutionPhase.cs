using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class PostExecutionPhase : GamePhase {

		protected override object Information => new {
			register = Game.executionState.CurrentRegister
		};

		public override GameState NewState => GameState.PLAYING;

		protected override GamePhase Run(GameLogic game) {
			int register = game.executionState.CurrentRegister++;
			if (register >= 9) return new GameEndPhase();
			return new PreExecutionPhase();
		}

		public override void Notify(ActionType action) {
			//TODO
		}

		public override bool Notify(GenericEvent ev) => false;

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) =>
			throw new NotImplementedException();
	}
}