using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	public class UpgradeManager {
		private readonly Dictionary<int, Upgrade> _pool = new Dictionary<int, Upgrade>();
		public           List<int>                Ids  => new List<int>(_pool.Keys);
		public           List<int>                Shop { get; } = new List<int>();

		public Upgrade Get(int upgradeId) => _pool.ContainsKey(upgradeId) ? _pool[upgradeId] : null;

		public void initUpgrades() {
			_pool[0] = new Upgrade() {
				Cost        = 2,
				Description = "Move an additional {fields} fields into a direction of your choice",
				Id          = 0,
				Values      = new List<Pair> {new Pair("fields", 2)},
				Type        = UpgradeType.Generator,
				Permanent   = false,
				Name        = "Lets muuuv",
				Rounds      = 1
			};
			_pool[1] = new Upgrade() {
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

		public void drawShop() {
			Shop.Clear();
			Shop.AddRange(Ids);
		}

		public void Buy(int id) {
			Shop.Remove(id);
		}
	}
}