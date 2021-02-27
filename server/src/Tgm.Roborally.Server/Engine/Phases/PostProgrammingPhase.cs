using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class PostProgrammingPhase : GamePhase {
		protected override object Information => null;

		protected override GamePhase Run(GameLogic game) {
			game.executionState.CurrentRegister = 0;
			return new PreExecutionPhase();
		}

		public override GameState NewState => GameState.PLAYING;

		public override void Notify(ActionType action) { //TODO
		}

		public override bool Notify(GenericEvent action) => false;

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) => new List<EntityEventOportunity>();
	}
}