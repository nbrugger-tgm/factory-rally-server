using System;

namespace Tgm.Roborally.Server.Engine.Exceptions {
	/// <summary>
	/// Exception that prevents a tried action
	/// </summary>
	public class ActionException : Exception{
		public ActionException(string msg):base(msg) {
			
		}
	}
}