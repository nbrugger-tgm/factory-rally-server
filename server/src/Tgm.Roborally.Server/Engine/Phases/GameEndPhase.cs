using System.Collections.Generic;
using System.Threading;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class GameEndPhase : GamePhase {
		protected override object Information => new Dictionary<string, object> {
			{"winner", 0},
			{"THIS_IS_BAD_INFO_ITS_ALWAYS_ZERO_JUST_YOU_KNOW_IT_WILL_BE_HERE", true}
		};

		public override GameState NewState => GameState.FINISHED;

		protected override GamePhase Run(GameLogic game) {
			Thread.Sleep(20000);
			return null;
		}

		public override void Notify(ActionType action) => throw new BadEventException(action.ToString(), "GAME_END");

		public override bool Notify(GenericEvent action) => false;

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) =>
			new List<EntityEventOportunity>();
	}
}