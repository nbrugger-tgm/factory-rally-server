using Tgm.Roborally.Server.Engine.Abstraction.Managers;
using System.Collections.Generic;
using System.Collections.Immutable;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	/// <summary>
	/// The interface to create Upgrade Manager implementations. An upgrade manager manages the upgrade deck as well as
	/// the shop and the upgrades in the hand of players
	/// </summary>
	public interface IUpgradeManager : IManager {
		List<int> Ids  { get; }
		List<int> Shop { get; }
		List<int> Deck { get; }

		/// <summary>
		///     Get the upgrade with the matching ID
		/// </summary>
		/// <param name="id">the id of the upgrade to get</param>
		Upgrade this[int id] { get; }

		/// <summary>
		/// Refills the shop with random cards from the deck
		/// </summary>
		void FillShop();

		/// <summary>
		///     Does all checks and throws exceptions
		/// </summary>
		/// <param name="id">The upgrade id</param>
		/// <param name="entId">the entity id</param>
		/// <exception cref="ActionException">In case the action is not allowed/bad</exception>
		void Buy(int id, int entId);

		/// <summary>
		/// Removes all upgrades from an entity
		/// </summary>
		/// <param name="robotId">
		/// the id of the entity.<br/>
		/// At the moment only robots can hold upgrades but the structure should also allow adding to other enitities
		/// </param>
		void DiscardEntityUpgrades(int robotId);

		/// <summary>
		/// Returns the IDs of the Upgrades an entity owns
		/// </summary>
		/// <param name="robotId">the id of the robot</param>
		/// <returns></returns>
		IImmutableSet<int> GetEntityUpgrades(int robotId);

		/// <summary>
		/// Discard a single upgrades from an entity
		/// </summary>
		/// <param name="robotId">the id of the robot</param>
		/// <param name="upgrade">the upgrade id to remove</param>
		void DiscardEntityUpgrade(int robotId, int upgrade);

		/// <summary>
		/// Checks if the robot/entity owns the upgrade or not
		/// </summary>
		/// <param name="robotId">the id of the robot</param>
		/// <param name="upgrade">the id of the upgrade to check</param>
		/// <returns>true if the robot owns the upgrade</returns>
		bool IsUpgradeOnEntity(int robotId, int upgrade);

		/// <summary>
		///     Does all checks and throws exceptions and commits events
		/// </summary>
		/// <param name="playerId">the id of the players robot</param>
		/// <param name="upgrade">the upgrade to exchange wit</param>
		/// <param name="exchange"></param>
		/// <exception cref="ActionException"></exception>
		void Exchange(int playerId, int upgrade, int exchange);


		/// <summary>
		/// Adds an upgrade to the deck
		/// </summary>
		/// <param name="managedUpgrade">the upgrade to add</param>
		public void AddToDeck(Upgrade managedUpgrade);
	}
}