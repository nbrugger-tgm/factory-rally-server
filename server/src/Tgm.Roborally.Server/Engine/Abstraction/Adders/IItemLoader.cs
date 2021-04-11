using Tgm.Roborally.Server.Engine.Abstraction.Managers;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Abstraction.Adders {
	/// <summary>
	/// Adds items relevant to the game that are not set in stone
	/// </summary>
	public interface IItemLoader {
		/// <summary>
		/// Add additional Upgrades to the deck
		/// </summary>
		/// <param name="add">the method to add them</param>
		public virtual void AddUpgrades(Add<Upgrade> add) {
		}

		/// <inheritdoc cref="IProgrammingManager.AddCard(int,RobotCommand)"/>
		/// <seealso cref="Add{T}"/>
		public delegate void AddCommands(int amount, RobotCommand command);

		/// <summary>
		/// Add additional Upgrades to the deck
		/// </summary>
		/// <param name="add">the method to add them</param>
		public virtual void AddProgrammingCards(AddCommands add) {
		}
	}
}