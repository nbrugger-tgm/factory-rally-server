using System;

namespace Tgm.Roborally.Server.Engine.Exceptions {
	public class BadEventException : Exception {
		public BadEventException(string ev_type) : base(
			"The event/action " + ev_type + " is not allowed in this state ") {
		}
	}
}