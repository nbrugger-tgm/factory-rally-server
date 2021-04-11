using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Abstraction.Managers {
	/// <summary>
	/// Handles Actions that can happen all the time. Kepps a history of all comitted events
	/// </summary>
	public interface IGameActionHandler : IManager{
		/// <summary>
		/// The index of the last executed Action
		/// </summary>
		int QueuePos { get; }

		/// <summary>
		/// A list of all Actions that still need to be executed
		/// </summary>
		List<ActionType> Pending { get; }

		/// <summary>
		/// A list of all actions that were executed in the past
		/// </summary>
		List<ActionType> Executed { get; }

		/// <summary>
		/// The Action Queue with all data at once
		/// </summary>
		List<Models.Action> Queue { get; }

		/// <summary>
		/// Execute the next pending action
		/// </summary>
		void ExecuteNext();

		/// <summary>
		/// Add an action to be executed FIFO
		/// </summary>
		/// <param name="t">the action to add</param>
		void Add(ActionType t);
	}
}