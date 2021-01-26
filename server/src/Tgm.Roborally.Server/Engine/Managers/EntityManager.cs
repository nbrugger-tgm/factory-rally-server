using System;
using System.Collections.Generic;
using System.Linq;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine
{
	public class EntityManager {
		private readonly List<Entity> ents = new List<Entity>();
		public RobotPickEvent PickRobo(Robots type, Player gamePlayer) {
			RobotInfo info = new RobotInfo() {
				Health = 100,
				Id     = NextFreeId()
			};
			info.Name = gamePlayer.DisplayName +" "+ info.Id;
			ents.Add(info);
			gamePlayer.ControlledEntities.Add(info.Id);
			RobotPickEvent ev = new RobotPickEvent() {
				Player = gamePlayer.Id,
				Robot  = info.Id
			};
			return ev;
		}

		private static readonly Random Rng = new Random();


		private int NextFreeId() {
			int r;
			
			do {
				r = Rng.Next(15);
			} while (ents.Select(e => e.Id).Contains(r));

			return r;
		}
	}
}