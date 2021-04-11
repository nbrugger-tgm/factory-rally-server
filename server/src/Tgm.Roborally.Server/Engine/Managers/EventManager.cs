using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Tgm.Roborally.Server.Engine.Abstraction.Managers;

namespace Tgm.Roborally.Server.Engine.Managers {
	/// <inheritdoc />
	public class EventManager : IEventManager {
		private readonly GameLogic _game;
		private readonly object    _locker = new object();

		/// <summary>
		/// The event queues for each player
		/// </summary>
		public Dictionary<int, Queue<Event>> queues = new Dictionary<int, Queue<Event>>();

		public EventManager(GameLogic game) {
			_game = game;
		}

		/// <summary>
		///     Adds the event to the stack and notifies waiting clients
		/// </summary>
		/// <param name="e"></param>
		public void Notify(Event e) {
			foreach (int player in _game.PlayerIds) {
				if (!queues.ContainsKey(player))
					queues[player] = new Queue<Event>();
				queues[player].Enqueue(e);
			}

			if ( /*TODO: check for event consumer compatability*/true) {
				foreach ((int key, _) in _game.Consumers) {
					if (!queues.ContainsKey(key))
						queues[key] = new Queue<Event>();
					queues[key].Enqueue(e);
				}
			}

			lock (_locker) {
				Monitor.PulseAll(_locker);
			}
		}

		/// <summary>
		///     Removes and recives the next event for this player
		/// </summary>
		/// <param name="player">the player to get the event for</param>
		/// <returns></returns>
		public Event Pop(int player) => queues[player].Count > 0 ? queues[player].Dequeue() : null;

		/// <summary>
		///     Wait for the next event
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Await() {
			lock (_locker) {
				Monitor.Wait(_locker);
			}
		}


		/// <inheritdoc />
		public Event Peek(int player) => queues[player].Count > 0 ? queues[player].Peek() : null;

		/// <inheritdoc />
		public bool HasQueue(int playerId) => queues.ContainsKey(playerId);

		/// <inheritdoc />
		public Queue<Event> GetQueue(int playerId) => queues[playerId];

		public void Setup() {}
	}
}