using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	/// <summary>
	///     A Task that controlls the flow of a Game Phase, should be used by a seperate Thread
	/// </summary>
	public abstract class GamePhase {
		protected GameLogic   Game;
		private RoundPhase? Cathegory => null;

		protected abstract object Information { get; }

		public abstract GameState NewState { get; }

		/// <summary>
		///     Starts the phase for the specific game
		/// </summary>
		/// <param name="game">The game to start the phase for</param>
		/// <returns>The GamePhase which should be activated next</returns>
		public GamePhase Start(GameLogic game) {
			Game = game;
			RoundPhase? newPhase = Cathegory;
			game.State = NewState;
			game.Phase = newPhase;
			game.CommitEvent(new GamePhaseChangedEvent {
				Information = Information,
				Phase       = newPhase,
				Step        = GetType().Name
			});
			return Run(game);
		}

		/// <summary>
		///     Executes the phase itself. Contains the implementation of the phase. Time based events go here
		/// </summary>
		/// <param name="game">The game "in" which the phase is running</param>
		/// <returns>the next phase</returns>
		protected abstract GamePhase Run(GameLogic game);

		/// <summary>
		/// Notifies the Phase about an submitted action command so the Phase can react to it. This should ether halt timers or pause them for example
		/// </summary>
		/// <param name="action">the type of the submitted command</param>
		public abstract void Notify(ActionType action);

		/// <summary>
		/// Notifies the Phase about an happend event and evaluates if the event was valid.<br/>
		/// This method should return false if the event was not supposed or allowed to happen at this stage.<br/><br/>
		/// The events for `GameRoundPhaseChanged`, `GamePhaseChanged`, `GameStart`, `Pause`, `Unpause` are NOT forwarded so there is no need to check for them
		/// </summary>
		/// <param name="ev">the event that happend</param>
		/// <returns>false if this event shouldn't have occured, true otherwise</returns>
		public abstract bool Notify(GenericEvent ev);

		/// <summary>
		/// Creates the list of possible actions that are executable at the moment of the call at this function.
		/// So this should not return a stsatic list but also be dynamic in regard to the state of the phase.
		/// This list can also be influenced by the robot that asks for the list as well as the requesting player
		/// </summary>
		/// <param name="robot">the id of the requesting robot</param>
		/// <param name="player">the id of the requesting player</param>
		/// <returns>a dynamic list of all exeuteable actions</returns>
		public abstract IList<EntityEventOportunity> GetPossibleActions(int robot, int player);
	}
}