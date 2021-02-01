using System;

namespace Tgm.Roborally.Server.Engine.Exceptions {
	public class PlayerCountException : Exception {
		public PlayerCountException(string expextedCout, int actual, string actionName = "") : base(
			"The action " + actionName + " requires " + expextedCout + " players but there were " + actual) {
		}
	}
}