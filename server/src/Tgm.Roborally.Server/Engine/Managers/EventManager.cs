using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics.X86;

namespace Tgm.Roborally.Server.Engine
{
	public class EventManager
	{
		private readonly GameLogic game;
		public Dictionary<int,Queue<Event>> queues = new Dictionary<int, Queue<Event>>();

		public EventManager(GameLogic game)
		{
			this.game = game;
		}

		public void notify(Event e)
		{
			foreach (int player in game.PlayerIds)
			{
				if(queues[player] == null)
					queues[player] = new Queue<Event>();
				queues[player].Enqueue(e);
			}
		}

		public Event Pop(int player)
		{
			return queues[player].Dequeue();
		}
	}
}