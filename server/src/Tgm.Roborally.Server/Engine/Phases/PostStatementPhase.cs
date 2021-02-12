using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class PostStatementPhase : GamePhase {
		public static     int    roboIndex = 0;

		protected override object Information => new PostStatementInfo();

		public class PostStatementInfo {
			public int RobotIndex => roboIndex;
		}

		protected override GamePhase Run(GameLogic game) {
			
			//TODO 
			
			roboIndex++;
			if (roboIndex >= game.Entitys.Robots.Count) {
				roboIndex = 0;
				return new PostExecutionPhase();
			}
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