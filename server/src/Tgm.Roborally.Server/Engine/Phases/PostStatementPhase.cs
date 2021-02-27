using System;
using System.Collections.Generic;
using System.Transactions;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class PostStatementPhase : GamePhase {
		private GameLogic _game;

		protected override object Information => new {
			roboIndex = _game.executionState.CurrentRobot
		};

		protected override GamePhase Run(GameLogic game) {
			this._game = game;
			//TODO robo priority
			
			int roboIndex = game.executionState.CurrentRobot++;
			
			if (roboIndex >= game.Entitys.Robots.Count) 
				return new PostExecutionPhase();
			
			//else /*makes no difference if added*/
			return new PreStatementPhase();
		}

		public override GameState NewState => GameState.PLAYING;

		public override void Notify(ActionType action) { //TODO
		}

		public override bool Notify(GenericEvent action) => false;

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) => throw new NotImplementedException();
	}
}