using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class ProgrammingPhase : GamePhase {
		protected override object Information => null;

		protected override GamePhase Run(GameLogic game) {
			return new PostProgrammingPhase();
		}

		public override GameState NewState => GameState.PLAYING;

		public override void Notify(ActionType action) { //TODO
		}

		public override bool Notify(GenericEvent action) => 
			action.Type == EventType.ProgrammingTimerStart ||
			action.Type == EventType.ProgrammingTimerStop ||
			action.Type == EventType.TimeElapsed ||
			action.Type == EventType.ChangeRegister ||
			action.Type == EventType.ClearRegister;
		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) => new List<EntityEventOportunity>();
	}
}