using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class StatementExecutePhase : GamePhase {
		protected override object Information => throw new NotImplementedException();

		public override GameState NewState => GameState.PLAYING;

		protected override GamePhase Run(GameLogic game) {
			int commandId =
				game.Programming.GetRegister(game.executionState.CurrentRobot)[game.executionState.CurrentRegister];
			RobotCommand cmd = game.Programming[commandId];
			cmd.Execute(game, game.executionState.CurrentRobot);
			return new PostStatementPhase();
		}

		public override void Notify(ActionType action) => throw new NotImplementedException();

		public override bool Notify(GenericEvent action) => true; //todo Be a little more restrictive

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) =>
			throw new NotImplementedException();
	}
}