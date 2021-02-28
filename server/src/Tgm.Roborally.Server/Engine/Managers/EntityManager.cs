using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	public class EntityManager {
		private static readonly Random       Rng   = new Random();
		private readonly        List<Entity> _ents = new List<Entity>();
		private readonly        GameLogic    _game;

		public EntityManager(GameLogic gameLogic) {
			_game = gameLogic;
		}

		public int       Count => _ents.Count;
		public ISet<int> Ids   => _ents.Select(selector: e => e.Id).ToImmutableHashSet();

		public ISet<int> Robots =>
			_ents.Where(predicate: e => e is RobotInfo).Select(selector: e => e.Id).ToImmutableHashSet();

		public ImmutableList<Entity> List => _ents.ToImmutableList();

		/// <summary>
		///     Return the entity wth the matching ID
		/// </summary>
		/// <param name="entId">The id to search for</param>
		public Entity this[int entId] => _ents.Find(match: e => e.Id == entId);

		public RobotPickEvent PickRobo(Robots type, Player gamePlayer) {
			RobotInfo info = new RobotInfo {
				Id = NextFreeId()
			};
			info.Name = gamePlayer.DisplayName + " " + info.Id;
			_ents.Add(info);
			gamePlayer.ControlledEntities.Add(info.Id);
			RobotPickEvent ev = new RobotPickEvent {
				Player = gamePlayer.Id,
				Robot  = info.Id
			};
			return ev;
		}


		private int NextFreeId() {
			int r;

			do {
				r = Rng.Next(16) + 1;
			} while (_ents.Select(selector: e => e.Id).Contains(r));

			return r;
		}
	}
}