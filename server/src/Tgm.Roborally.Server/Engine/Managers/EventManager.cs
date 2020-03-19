using System.Collections.Generic;
using System.IO;

namespace Tgm.Roborally.Server.Engine
{
	public class EventManager
	{
		public Dictionary<int,Queue<Event>> queues = new Dictionary<int, Queue<Event>>();

		public void notify(Event e,int game)
		{
			foreach (int player in GameManager.instance.games[game].PlayerIds)
			{
				if(queues[player] == null)
					queues[player] = new Queue<Event>();
				queues[player].Enqueue(e);
			}
		}
	}
}