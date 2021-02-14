using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class PreProgrammingPhase : GamePhase {
		protected override object Information => null;

		protected override GamePhase Run(GameLogic game) {
			foreach (int r in game.Entitys.Robots) {
				game.Programming.Draw(r);
			}
			return new ProgrammingPhase();
		}

		public override GameState NewState                  => GameState.PLAYING;
		public override void Notify(ActionType action) { //TODO
		}

		public override bool Notify(GenericEvent action) => action.Data is DrawCardEvent;

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) => new List<EntityEventOportunity>();
	}
}