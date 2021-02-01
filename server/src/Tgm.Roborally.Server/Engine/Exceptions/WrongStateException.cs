using System;
using System.Transactions;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Exceptions {
	public class WrongStateException : Exception {
		public WrongStateException(GameState expected, GameState actual, String action = "") : base(
			"The action " + action + " could not be executed. The needed state is \"" + expected + "\" but was \"" +
			actual        + '\"') {
		}
	}
}