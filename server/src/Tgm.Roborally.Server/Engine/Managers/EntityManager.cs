using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	/// <inheritdoc />
	public class EntityManager : IEntityManager {
		private static readonly Random       Rng   = new();
		private readonly        List<Entity> _ents = new();
		private readonly        GameLogic    _game;

		public EntityManager(GameLogic gameLogic) {
			_game = gameLogic;
		}

		/// <summary>
		/// The ammount of entities (alive or dead)
		/// </summary>
		public int Count => _ents.Count;

		/// <summary>
		/// Each entity has an ID, all IDs can be found in this Property
		/// </summary>
		public IImmutableSet<int> Ids => _ents.Select(selector: e => e.Id).ToImmutableHashSet();

		/// <summary>
		/// Contains the id of each robot 
		/// </summary>
		public IImmutableList<int> Robots =>
			_ents.Where(predicate: e => e is RobotInfo).Select(selector: e => e.Id).ToImmutableList();

		/// <summary>
		/// All entities in the game (alive and dead)
		/// </summary>
		public IEnumerable<Entity> List => _ents.ToImmutableList();

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
				Id = NextFreeId(),
				Type = type
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

		/// <inheritdoc />
		public void PlaceRobotsOnSpawn() {
			for (int i = 0; i < Robots.Count; i++) {
				Position spawn = _game.Map.Spawns[i];
				_ents[Robots[i]].Location = spawn;
			}
		}


		private int NextFreeId() {
			int r;

			do {
				r = Rng.Next(16) + 1;
			} while (Ids.Contains(r));

			return r;
		}

		public void Setup() {
		}
	}
}