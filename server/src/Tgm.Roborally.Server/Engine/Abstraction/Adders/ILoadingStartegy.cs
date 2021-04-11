using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Abstraction.Adders {
	/// <summary>
	/// This function counts something and returns the result. The counted thing depends on the use case and should be explained in the documentation of the parameter
	/// </summary>
	public delegate int count();

	/// <summary>
	/// Defines a strategy to load items from different mods
	/// </summary>
	public interface ILoadingStartegy {
		/// <summary>
		/// Loads the items from some or all mods
		/// </summary>
		/// <param name="itemLoaders">the loaders to add the items mapped to the name of the module that adds them</param>
		/// <param name="upgradeAdder">the function to use in the IItemLoader</param>
		/// <param name="commandAdder">the function to use in the IItemLoader</param>
		/// <param name="upgradeCount">a function to get the current amount of added Upgrades</param>
		/// <param name="programmingCardCount">a function to get the current amount of added Programming Cards</param>
		public void LoadAllItems(
			Dictionary<string, IItemLoader> itemLoaders,
			Add<Upgrade>                    upgradeAdder,
			IItemLoader.AddCommands         commandAdder,
			count                           upgradeCount,
			count                           programmingCardCount);
	}
}