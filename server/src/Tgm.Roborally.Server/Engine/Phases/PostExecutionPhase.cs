using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class PostExecutionPhase : GamePhase {
		public static     int    register = 0;

		protected override object Information => new ExecutionInfo(register);

		protected override GamePhase Run(GameLogic game) {
			if (register >= 9) {
				register = 0;
				return new GameEndPhase();
			}
			register++;
			return new PreExecutionPhase();
		}

		public override GameState NewState => GameState.PLAYING;

		public override void Notify(ActionType action) {//TODO
		}

		public override bool Notify(GenericEvent action) => false;

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) => throw new NotImplementedException();
	}
}