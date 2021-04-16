#nullable enable
using System;
using Tgm.Roborally.Server.Engine.Abstraction.Adders;

namespace Tgm.Roborally.Server.Engine.Abstraction {
	/// <summary>
	/// Overwrite/Extend this class to create a mod. In order to do something overwrite the methods of this class
	/// </summary>
	public interface Mod : EngineImplementationProvider, IItemLoader {
		/// <summary>
		/// Creates and Returns the implementation of the first phase of the game. If NULL is returned the StartPhase stays unchanged
		/// </summary>
		/// <param name="arg">the previous game phase, if this is not null you will overwrite it</param>
		/// <returns>the GamePhase set at the beginning of the game this phase should remain dormant until the START_GAME command is given</returns>
		public GamePhase? StartingPhase(GamePhase? arg) => null;

		/// <summary>
		/// Called when a game was created and the mod was loaded into the game
		/// </summary>
		public void Loaded() {
		}

		/// <summary>
		/// Called when the mod was recocniced and addded and the API started
		/// </summary>
		public void Ready() {
			Console.Out.WriteLine($"\"{Name}\" enabled");
		}

		/// <summary>
		/// If this method returns anything else than null it will replace the LoadingStrategy of Previous mods/vanilla
		/// </summary>
		/// <param name="prev">The previous Strategy that would be overwritten</param>
		/// <returns>the Loading Strategy that shall be used</returns>
		public ILoadingStartegy? GetCustomLoadingStrategy(ILoadingStartegy? prev) => null;

		public void BevoreLoad() {
		}

		public string Name { get; }

		/// <summary>
		/// Called by the game in the case of an event
		/// </summary>
		/// <param name="game">the game that called the function and the event appeared</param>
		/// <param name="event">the fired event</param>
		public virtual void OnEvent(GameLogic game, Event @event) {
		}
	}
}