using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	/// <inheritdoc />
	public class UpgradeManager : IUpgradeManager {
		private readonly Dictionary<int, ISet<int>> _entityUpgrades = new Dictionary<int, ISet<int>>();

		private readonly GameLogic                       _game;
		private readonly Dictionary<int, ManagedUpgrade> _pool = new Dictionary<int, ManagedUpgrade>();

		public UpgradeManager(GameLogic game) {
			_game = game;
		}

		public List<int> Ids => new List<int>(_pool.Keys);

		public List<int> Shop => _pool
								 .Where(predicate: u => u.Value.Location == UpgradeLocation.Shop)
								 .Select(selector: p => p.Value.Id)
								 .ToList();

		public List<int> Deck => _pool
								 .Where(predicate: u => u.Value.Location == UpgradeLocation.Deck)
								 .Select(selector: p => p.Value.Id)
								 .ToList();

		/// <summary>
		///     Get the upgrade with the matching ID
		/// </summary>
		/// <param name="id">the id of the upgrade to get</param>
		public Upgrade this[int id] => _pool.ContainsKey(id) ? _pool[id].Upgrade : null;


		/// <inheritdoc />
		public void AddToDeck(Upgrade managedUpgrade) {
			_pool[_pool.Count] = new ManagedUpgrade(managedUpgrade);
		}

		/// <inheritdoc />
		public void FillShop() {
			Random rng = new Random();
			if (Shop.Count >= _game.PlayerCount)
				discardShop();
			while (Deck.Count > 0 && Shop.Count < _game.PlayerCount)
				_pool[Shop[rng.Next(Shop.Count)]].Location = UpgradeLocation.Shop;
		}

		private void discardShop() {
			List<int> shop                            = Shop;
			foreach (int i in shop) _pool[i].Location = UpgradeLocation.Discarded;
		}

		/// <summary>
		///     Does all checks and throws exceptions
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
				if (robo.EnergyCubes < upgrade.Cost) throw new ActionException("Not enough energy");

				_pool[id].Location = UpgradeLocation.Robot;
				_entityUpgrades[id].Add(id);
				robo.EnergyCubes -= upgrade.Cost;
				_game.CommitEvent(new PurchaseEvent {
					Player  = entId,
					Upgrade = id
				});
			}
			else
				throw new ActionException("The ID does not represents a Robot");
		}

		/// <inheritdoc />
		public void DiscardEntityUpgrades(int robotId) {
			ISet<int> upgrades                                        = _entityUpgrades[robotId];
			foreach (int upgrade in upgrades) _pool[upgrade].Location = UpgradeLocation.Discarded;
			//Only commiting it once to prevent spam
			_game.CommitEvent(new DiscardUpgradesEvent {
				Robot    = robotId,
				Upgrades = upgrades.ToList()
			});
		}

		/// <inheritdoc />
		public IImmutableSet<int> GetEntityUpgrades(int robotId) => _entityUpgrades[robotId].ToImmutableHashSet();

		/// <inheritdoc />
		public void DiscardEntityUpgrade(int robotId, int upgrade) {
			_pool[upgrade].Location = UpgradeLocation.Discarded;
			_game.CommitEvent(new DiscardUpgradesEvent {
				Robot    = robotId,
				Upgrades = {upgrade}
			});
		}

		/// <inheritdoc />
		public bool IsUpgradeOnEntity(int robotId, int upgrade) => _entityUpgrades[robotId].Contains(upgrade);

		/// <summary>
		///     Does all checks and throws exceptions and commits events
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

		public void Setup() {
			_entityUpgrades.Clear();
			foreach (int id in _game.Entitys.Ids) _entityUpgrades[id] = new HashSet<int>();
		}
	}

	internal enum UpgradeLocation {
		Deck,
		Shop,
		Robot,
		Discarded
	}

	internal class ManagedUpgrade {
		public UpgradeLocation Location = UpgradeLocation.Deck;

		public ManagedUpgrade(Upgrade upgrade) {
			Upgrade = upgrade;
		}

		internal Upgrade Upgrade { get; }

		public int Id => Upgrade.Id;
	}
}