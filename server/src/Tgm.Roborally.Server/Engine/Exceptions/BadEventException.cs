using System;

namespace Tgm.Roborally.Server.Engine.Exceptions {
	/// <summary>
	/// Throw this event if an event occurs at an disallowed time
	/// </summary>
	public class BadEventException : Exception {
		public BadEventException(string ev_type, string? fullName) : base(
			"The event/action " + ev_type + " is not allowed in this state " + fullName) {
		}
	}
}