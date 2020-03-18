using System.Collections.Generic;
using System.IO;

namespace Tgm.Roborally.Server.Engine
{
	public class EventManager
	{
		public Dictionary<string,Queue<Event>> queues = new Dictionary<string, Queue<Event>>();

		public void notify(Event e,int game)
		{
			foreach (var player in GameManager.instance.games[game].Players)
			{
				if(queues[player] == null)
					queues[player] = new Queue<Event>();
				queues[player].Enqueue(e);
			}
		}
	}
}