using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	/// <summary>
	/// A Task that controlls the flow of a Game Phase, should be used by a seperate Thread
	/// </summary>
	public abstract class GamePhase {
		/// <summary>
		/// Starts the phase for the specific game
		/// </summary>
		/// <param name="game">The game to start the phase for</param>
		/// <returns>The GamePhase which should be activated next</returns>
		public GamePhase Start(GameLogic game) {
			game.State = NewState;
			game.CommitEvent(new GamePhaseChangeEvent() {
				NewStatus = NewState
			});
			return Run(game);
		}

		/// <summary>
		/// Executes the phase itself. Contains the implementation of the phase. Time based events go here
		/// </summary>
		/// <param name="game">The game "in" which the phase is running</param>
		/// <returns>the next phase</returns>
		protected abstract GamePhase Run(GameLogic game);

		public abstract GameState NewState { get; }
		public abstract void      Notify(ActionType   action);
		public abstract bool      Notify(GenericEvent action);
		public abstract IList<EntityEventOportunity> GetPossibleActions(int robot,int player);
	}
}