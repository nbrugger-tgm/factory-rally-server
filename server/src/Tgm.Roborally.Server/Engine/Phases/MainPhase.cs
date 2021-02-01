using System.Threading;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class MainPhase : GamePhase {
		protected override GamePhase Run(GameLogic game) {
			while (true) {
				Thread.Yield(); //implement
				Thread.Sleep(10);
			}

			//throw new System.NotImplementedException();
		}

		public override GameState NewState { get; }

		public override void Notify(ActionType action) {
			throw new System.NotImplementedException();
		}

		public override bool Notify(GenericEvent action) => throw new System.NotImplementedException();
	}
}