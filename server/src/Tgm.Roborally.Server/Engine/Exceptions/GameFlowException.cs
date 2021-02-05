using System;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class GameFlowException : Exception {
		public const string BAD_EVENT = "The game died due to an illegal event";

		public GameFlowException(String badEvent) : base(badEvent){
		}
	}
}