using System.Collections.Generic;

namespace Tgm.Roborally.Server.Engine.Abstraction.Managers {
	/// <summary>
	/// Handles event stacks and reacting. Each player has his own event Stack to consume from. Used to add/commit and consume events
	/// </summary>
	public interface IEventManager {
		/// <summary>
		///     Adds the event to the stack and notifies waiting clients
		/// </summary>
		/// <param name="e"></param>
		void Notify(Event e);

		/// <summary>
		///     Removes and recives the next event for this player
		/// </summary>
		/// <param name="player">the player to get the event for</param>
		/// <returns></returns>
		Event Pop(int player);

		/// <summary>
		///     Wait for the next event
		/// </summary>
		void Await();
		/// <summary>
		/// Look at the next event for an specific player without popping it
		/// </summary>
		/// <param name="player">the player to get the next event for</param>
		/// <returns>the peeked event</returns>
		Event Peek(int player);

		/// <summary>
		/// Checks if the player has a queue or not
		/// </summary>
		/// <param name="playerId">the player to check</param>
		/// <returns>true if the player has a queue</returns>
		bool               HasQueue(int playerId);
		/// <summary>
		/// Returns the Queue of the
		/// </summary>
		/// <param name="objPlayerId"></param>
		/// <returns></returns>
		Queue<Event> GetQueue(int objPlayerId);
	}
}