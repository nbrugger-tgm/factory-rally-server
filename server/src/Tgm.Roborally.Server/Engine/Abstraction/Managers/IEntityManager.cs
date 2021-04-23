using System.Collections.Generic;
using System.Collections.Immutable;
using Tgm.Roborally.Server.Engine.Abstraction.Managers;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	/// <summary>
	/// Manages all entities including robots of the game. Does NOT manages their movement or programming
	/// </summary>
	public interface IEntityManager : IManager {
		/// <summary>
		/// The ammount of entities (alive or dead)
		/// </summary>
		int Count { get; }

		string IManager.Name => "Entity Manager";

		/// <summary>
		/// Each entity has an ID, all IDs can be found in this Property
		/// </summary>
		IImmutableSet<int> Ids { get; }

		/// <summary>
		/// Contains the id of each robot 
		/// </summary>
		IImmutableList<int> Robots { get; }

		/// <summary>
		/// All entities in the game (alive and dead)
		/// </summary>
		IEnumerable<Entity> List { get; }

		/// <summary>
		///     Return the entity wth the matching ID
		/// </summary>
		/// <param name="entId">The id to search for</param>
		Entity this[int entId] { get; }

		/// <summary>
		/// Assigns the robot to a player
		/// </summary>
		/// <param name="type">the type of robot to pick</param>
		/// <param name="gamePlayer">the picking player</param>
		/// <returns>the generated event</returns>
		void PickRobo(Robots type, Player gamePlayer);

		/// <summary>
		/// Setts the location of the robots to the spawn positions randomly
		/// </summary>
		void PlaceRobotsOnSpawn();
	}
}