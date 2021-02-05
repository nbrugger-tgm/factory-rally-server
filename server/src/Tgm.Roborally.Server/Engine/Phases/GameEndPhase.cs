using System.Collections.Generic;
using System.Threading;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class GameEndPhase : GamePhase {
		protected override GamePhase Run(GameLogic game) {
			Thread.Sleep(20000);
			return null;
		}

		public override GameState NewState => GameState.FINISHED;
		public override void Notify(ActionType action) => throw new BadEventException(action.ToString(), "GAME_END");

		public override bool Notify(GenericEvent action) => false;

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) =>
			new List<EntityEventOportunity>();
	}
}