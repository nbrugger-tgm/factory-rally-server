using System;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class GameFlowException : Exception {
		public const string NO_DECK   = "There are no discarded cards and the Deck is empty";
		public const string BAD_EVENT = "The game died due to an illegal event";

		public GameFlowException(string badEvent) : base(badEvent) {
		}
	}
}