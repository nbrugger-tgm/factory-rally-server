using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class PreStatementPhase : GamePhase {

		protected override object Information => new {
			robotId = Game.executionState.CurrentRobot
		};


		public override GameState NewState => GameState.PLAYING;

		protected override GamePhase Run(GameLogic game) {
			return new StatementExecutePhase();
		}

		public override void Notify(ActionType action) {
			//TODO
		}

		public override bool Notify(GenericEvent ev) => false;

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) =>
			new List<EntityEventOportunity>();
	}
}