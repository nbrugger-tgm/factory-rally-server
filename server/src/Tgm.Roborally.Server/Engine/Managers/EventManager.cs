using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Threading;

namespace Tgm.Roborally.Server.Engine
{
	public class EventManager
	{
		private readonly GameLogic           game;
		private object                       locker = new object();
		public  Dictionary<int,Queue<Event>> queues = new Dictionary<int, Queue<Event>>();

		public EventManager(GameLogic game)
		{
			this.game = game;
		}

		/// <summary>
		/// Adds the event to the stack and notifies waiting clients
		/// </summary>
		/// <param name="e"></param>
		public void Notify(Event e)
		{
			foreach (int player in game.PlayerIds)
			{
				if(queues[player] == null)
					queues[player] = new Queue<Event>();
				queues[player].Enqueue(e);
			}
			Monitor.PulseAll(locker);
		}

		/// <summary>
		/// Removes and recives the next event for this player
		/// </summary>
		/// <param name="player">the player to get the event for</param>
		/// <returns></returns>
		public Event Pop(int player) {
			return queues[player].Count >0 ? queues[player].Dequeue() : null;
		}

		/// <summary>
		/// Wait for the next event
		/// </summary>
		public void Await() {
			Monitor.Wait(locker);
		}

		public Event Peek(int player) {
			return queues[player].Count >0 ? queues[player].Peek() : null;
		}
	}
}