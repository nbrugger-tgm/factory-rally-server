using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Engine.Abstraction.Adders;
using Tgm.Roborally.Server.Engine.Managers;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.GameItems {
	/// <inheritdoc />
	public class VanillaItemLoader : ILoadingStartegy {
		/// <inheritdoc />
		public void LoadAllItems(
			Dictionary<string, IItemLoader> itemLoaders,
			Add<Upgrade>                    upgradeAdder,
			IItemLoader.AddCommands         commandAdder,
			count                           upgradeCount,
			count                           programmingCardCount) {
			foreach (KeyValuePair<string, IItemLoader> loader in itemLoaders) {
				int bevUpgrades = upgradeCount();
				int bevProgr    = programmingCardCount();
				loader.Value.AddUpgrades(upgradeAdder);
				loader.Value.AddProgrammingCards(commandAdder);
				bool upsChanged  = bevUpgrades < upgradeCount();
				bool progChanged = bevProgr    < programmingCardCount();
				if (upsChanged && progChanged) {
					Console.Out.WriteLine($"Loaded Upgrades and Commands from \"{loader.Key}\"");
				}
				else if (upsChanged) {
					Console.Out.WriteLine($"Loaded Upgrades from \"{loader.Key}\"");
				}
				else if (progChanged) {
					Console.Out.WriteLine($"Loaded Commands from \"{loader.Key}\"");
				}
			}
		}
	}
}