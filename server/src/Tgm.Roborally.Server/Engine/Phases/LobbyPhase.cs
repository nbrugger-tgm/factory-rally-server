using System.Collections.Generic;
using System.Threading;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class LobbyPhase : GamePhase {
		private bool started;

		protected override object Information => null;

		protected override GamePhase Run(GameLogic game) {
			while (!started) {
				Thread.Yield();
			}

			return new RobotPickingPhase();
		}

		public override GameState NewState => GameState.LOBBY;

		public override void Notify(ActionType action) {
			started = action == ActionType.STARTGAME;
		}

		public override bool Notify(GenericEvent action) {
			return action.Data is JoinEvent;
		}

		public override IList<EntityEventOportunity> GetPossibleActions(int a, int b) {
			return new List<EntityEventOportunity>();
		}
	}
}