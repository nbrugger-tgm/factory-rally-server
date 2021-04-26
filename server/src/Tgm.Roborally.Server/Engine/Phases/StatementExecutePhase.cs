using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class StatementExecutePhase : GamePhase {
		protected override object Information => new {
			robot = Game.executionState.CurrentRobot,
			register = Game.executionState.CurrentRegister
		};

		public override GameState NewState => GameState.PLAYING;

		protected override GamePhase Run(GameLogic game) {
			int commandId = game.Programming.GetRegister(game.executionState.CurrentRobot)[game.executionState.CurrentRegister];
			if (commandId != -1) {//-1 is an empty register
				RobotCommand cmd = game.Programming[commandId];
				cmd.Execute(game, game.executionState.CurrentRobot);
			}
			return new PostStatementPhase();
		}

		public override void Notify(ActionType action) => throw new NotImplementedException();

		public override bool Notify(GenericEvent ev) => true; //todo Be more restrictive

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) => new List<EntityEventOportunity>();
	}
}