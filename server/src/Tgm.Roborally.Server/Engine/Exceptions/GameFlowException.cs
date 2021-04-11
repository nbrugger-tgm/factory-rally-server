using System;

namespace Tgm.Roborally.Server.Engine.Phases {
	/// <summary>
	/// Throw this if the game comes in an unstable flow state -> when an state is reached where the game cannot perceed because of the circumstancs
	/// </summary>
	public class GameFlowException : Exception {
		public const string NO_DECK   = "There are no discarded cards and the Deck is empty";
		public const string BAD_EVENT = "The game died due to an illegal event";

		/// <inheritdoc />
		public GameFlowException(string badEvent) : base(badEvent) {
		}
	}
}