using System.Collections.Generic;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	public interface IUpgradeManager {
		List<int> Ids  { get; }
		List<int> Shop { get; }
		List<int> Deck { get; }

		/// <summary>
		///     Get the upgrade with the matching ID
		/// </summary>
		/// <param name="id">the id of the upgrade to get</param>
		Upgrade this[int id] { get; }

		void initUpgrades();
		void fillShop();

		/// <summary>
		///     Does all checks and throws exceptions
		/// </summary>
		/// <param name="id">The upgrade id</param>
		/// <param name="entId">the entity id</param>
		/// <exception cref="ActionException">In case the action is not allowed/bad</exception>
		void Buy(int id, int entId);

		void      DiscardEntityUpgrades(int robotId);
		ISet<int> GetEntityUpgrades(int     robotId);
		void      DiscardEntityUpgrade(int  robotId, int upgrade);
		bool      IsUpgradeOnEntity(int     robotId, int upgrade);

		/// <summary>
		///     Does all checks and throws exceptions and commits events
		/// </summary>
		/// <param name="playerId">the id of the players robot</param>
		/// <param name="upgrade">the upgrade to exchange wit</param>
		/// <param name="exchange"></param>
		/// <exception cref="ActionException"></exception>
		void Exchange(int playerId, int upgrade, int exchange);
	}
}