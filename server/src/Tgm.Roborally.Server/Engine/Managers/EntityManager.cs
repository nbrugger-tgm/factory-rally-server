using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	/// <summary>
	/// Manages all entities including robots of the game. Does NOT manages their movement or programming
	/// </summary>
	public class EntityManager {
		private static readonly Random       Rng   = new Random();
		private readonly        List<Entity> _ents = new List<Entity>();
		private readonly        GameLogic    _game;

		public EntityManager(GameLogic gameLogic) {
			_game = gameLogic;
		}

		/// <summary>
		/// The ammount of entities (alive or dead)
		/// </summary>
		public int       Count => _ents.Count;
		/// <summary>
		/// Each entity has an ID, all IDs can be found in this Property
		/// </summary>
		public ISet<int> Ids   => _ents.Select(selector: e => e.Id).ToImmutableHashSet();

		/// <summary>
		/// Contains the id of each robot 
		/// </summary>
		public ISet<int> Robots =>
			_ents.Where(predicate: e => e is RobotInfo).Select(selector: e => e.Id).ToImmutableHashSet();

		/// <summary>
		/// All entities in the game (alive and dead)
		/// </summary>
		public ImmutableList<Entity> List => _ents.ToImmutableList();

		/// <summary>
		///     Return the entity wth the matching ID
		/// </summary>
		/// <param name="entId">The id to search for</param>
		public Entity this[int entId] => _ents.Find(match: e => e.Id == entId);

		/// <summary>
		/// Assigns the robot to a player
		/// </summary>
		/// <param name="type">the type of robot to pick</param>
		/// <param name="gamePlayer">the picking player</param>
		/// <returns>the generated event</returns>
		public void PickRobo(Robots type, Player gamePlayer) {
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
			_game.CommitEvent(ev);
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