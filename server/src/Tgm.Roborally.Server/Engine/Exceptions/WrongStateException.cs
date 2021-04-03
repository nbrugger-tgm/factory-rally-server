using System;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Exceptions {
	/// <summary>
	/// If an action is executed in the wrong state of the game
	/// </summary>
	public class WrongStateException : Exception {
		public WrongStateException(GameState expected, GameState actual, string action = "") : base(
			"The action " + action + " could not be executed. The needed state is \"" + expected + "\" but was \"" +
			actual        + '\"') {
		}
	}
}