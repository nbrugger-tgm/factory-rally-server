using System;
using System.Collections.Generic;
using System.Linq;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	public class UpgradeManager {
		private readonly Dictionary<int, ManagedUpgrade> _pool           = new Dictionary<int, ManagedUpgrade>();
		private readonly Dictionary<int, ISet<int>>      _entityUpgrades = new Dictionary<int, ISet<int>>();
		public           List<int>                       Ids => new List<int>(_pool.Keys);

		public List<int> Shop => _pool
								 .Where(u => u.Value.location == UpgradeLocation.Shop)
								 .Select(p => p.Value.Id)
								 .ToList();

		public List<int> Deck => _pool
								 .Where(u => u.Value.location == UpgradeLocation.Deck)
								 .Select(p => p.Value.Id)
								 .ToList();

		/// <summary>
		/// Get the upgrade with the matching ID
		/// </summary>
		/// <param name="id">the id of the upgrade to get</param>
		public Upgrade this[int id] => _pool.ContainsKey(id) ? _pool[id] : null;

		private readonly GameLogic _game;

		public UpgradeManager(GameLogic game) {
			_game = game;
			_entityUpgrades.Clear();
			foreach (int id in _game.Entitys.Ids) {
				_entityUpgrades[id] = new HashSet<int>();
			}
		}

		public void initUpgrades() {
			_pool[0] = new ManagedUpgrade() {
				Cost        = 2,
				Description = "Move an additional {fields} fields into a direction of your choice",
				Id          = 0,
				Values      = new List<Pair> {new Pair("fields", 2)},
				Type        = UpgradeType.Generator,
				Permanent   = false,
				Name        = "Lets muuuv",
				Rounds      = 1
			};
			_pool[1] = new ManagedUpgrade() {
				Cost        = 3,
				Description = "Move an additional {fields} fields into a direction of your choice",
				Id          = 1,
				Values      = new List<Pair> {new Pair("fields", 4)},
				Type        = UpgradeType.Generator,
				Permanent   = false,
				Name        = "Lets muuuv",
				Rounds      = 1
			};
		}

		public void fillShop() {
			Random rng = new Random();
			if (Shop.Count >= _game.PlayerCount)
				discardShop();
			while (Deck.Count > 0 && Shop.Count < _game.PlayerCount) {
				_pool[Shop[rng.Next(Shop.Count)]].location = UpgradeLocation.Shop;
			}
		}

		private void discardShop() {
			List<int> shop = Shop;
			foreach (int i in shop) {
				_pool[i].location = UpgradeLocation.Discarded;
			}
		}

		/// <summary>
		/// Does all checks and throws exceptions
		/// </summary>
		/// <param name="id">The upgrade id</param>
		/// <param name="entId">the entity id</param>
		/// <exception cref="ActionException">In case the action is not allowed/bad</exception>
		public void Buy(int id, int entId) {
			Entity  ent     = _game.Entitys[entId];
			Upgrade upgrade = this[id];
			if (!Shop.Contains(id))
				throw new ActionException("This upgrade is not in the shop and therefore not buy able");
			if (ent is RobotInfo) {
				RobotInfo robo = new RobotInfo();
				if (robo.EnergyCubes < upgrade.Cost) {
					throw new ActionException("Not enough energy");
				}

				_pool[id].location = UpgradeLocation.Robot;
				_entityUpgrades[id].Add(id);
				robo.EnergyCubes -= upgrade.Cost;
				_game.CommitEvent(new PurchaseEvent() {
					Player  = entId,
					Upgrade = id
				});
			}
			else {
				throw new ActionException("The ID does not represents a Robot");
			}
		}

		public void DiscardEntityUpgrades(int robotId) {
			ISet<int> upgrades = _entityUpgrades[robotId];
			foreach (int upgrade in upgrades) {
				_pool[upgrade].location = UpgradeLocation.Discarded;
			}
			//Only commiting it once to prevent spam
			_game.CommitEvent( new DiscardUpgradesEvent() {
				Robot = robotId,
				Upgrades = upgrades.ToList()
			});
		}

		public ISet<int> GetEntityUpgrades(int robotId) => _entityUpgrades[robotId];

		public void DiscardEntityUpgrade(int robotId, int upgrade) {
			_pool[upgrade].location = UpgradeLocation.Discarded;
			_game.CommitEvent(new DiscardUpgradesEvent() {
				Robot = robotId,
				Upgrades = {upgrade}
			});
		}

		public bool IsUpgradeOnEntity(int robotId, int upgrade) => _entityUpgrades[robotId].Contains(upgrade);

		/// <summary>
		/// Does all checks and throws exceptions and commits events
		/// </summary>
		/// <param name="playerId">the id of the players robot</param>
		/// <param name="upgrade">the upgrade to exchange wit</param>
		/// <param name="exchange"></param>
		/// <exception cref="ActionException"></exception>
		public void Exchange(int playerId, int upgrade, int exchange) {
			if (!GetEntityUpgrades(playerId).Contains(exchange))
				throw new ActionException("The upgrade to exchange with is not owned by the robot");
			DiscardEntityUpgrade(playerId, upgrade);
			Buy(upgrade, playerId);
		}
	}

	internal enum UpgradeLocation {
		Deck,
		Shop,
		Robot,
		Discarded
	}

	internal class ManagedUpgrade : Upgrade {
		public UpgradeLocation location = UpgradeLocation.Deck;
	}
}