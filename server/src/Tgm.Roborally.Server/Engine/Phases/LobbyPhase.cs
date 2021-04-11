using System.Collections.Generic;
using System.Threading;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class LobbyPhase : GamePhase {
		private object started = new object();

		protected override object Information => null;

		public override GameState NewState => GameState.LOBBY;

		protected override GamePhase Run(GameLogic game) {
			lock (started) {
				Monitor.Wait(started);
			}

			return new RobotPickingPhase();
		}

		public override void Notify(ActionType action) {
			if (action == ActionType.STARTGAME)
				lock (started) {
					Monitor.Pulse(started);
				}
		}

		public override bool Notify(GenericEvent ev) => ev.Data is JoinEvent;

		public override IList<EntityEventOportunity> GetPossibleActions(int a, int b) =>
			new List<EntityEventOportunity>();
	}
}