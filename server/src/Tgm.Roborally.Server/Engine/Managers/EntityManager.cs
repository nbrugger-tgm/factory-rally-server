using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	public class EntityManager {
		private readonly GameLogic    _game;
		private readonly List<Entity> ents = new List<Entity>();
		public           int          Count => ents.Count;
		public           ISet<int>    Ids   => ents.Select(e => e.Id).ToHashSet();

		public RobotPickEvent PickRobo(Robots type, Player gamePlayer) {
			RobotInfo info = new RobotInfo() {
				Health = 100,
				Id     = NextFreeId()
			};
			info.Name = gamePlayer.DisplayName + " " + info.Id;
			ents.Add(info);
			gamePlayer.ControlledEntities.Add(info.Id);
			RobotPickEvent ev = new RobotPickEvent() {
				Player = gamePlayer.Id,
				Robot  = info.Id
			};
			return ev;
		}

		private static readonly Random Rng = new Random();

		public EntityManager(GameLogic gameLogic) {
			_game = gameLogic;
		}


		private int NextFreeId() {
			int r;

			do {
				r = Rng.Next(16)+1;
			} while (ents.Select(e => e.Id).Contains(r));

			return r;
		}

		/// <summary>
		/// Return the entity wth the matching ID
		/// </summary>
		/// <param name="entId">The id to search for</param>
		public Entity this[int entId] => ents.Find(match: e => e.Id == entId);
	}
}