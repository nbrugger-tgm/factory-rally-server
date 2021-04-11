using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Engine.Abstraction.Adders;
using Tgm.Roborally.Server.Engine.Abstraction.Managers;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.GameItems {
	public class VanillaCards : IItemLoader {
		public void AddUpgrades(Add<Upgrade> add) {
			add(new Upgrade {
				Cost        = 2,
				Description = "Move an additional {fields} fields into a direction of your choice",
				Id          = 0,
				Values      = new List<Pair> {new Pair("fields", 2)},
				Type        = UpgradeType.Generator,
				Permanent   = false,
				Name        = "Lets muuuv",
				Rounds      = 1
			});
			add(new Upgrade {
				Cost        = 3,
				Description = "Move an additional {fields} fields into a direction of your choice",
				Id          = 1,
				Values      = new List<Pair> {new Pair("fields", 4)},
				Type        = UpgradeType.Generator,
				Permanent   = false,
				Name        = "Lets muuuv",
				Rounds      = 1
			});
		}

		public void AddProgrammingCards(IItemLoader.AddCommands add) {
		}
	}
}