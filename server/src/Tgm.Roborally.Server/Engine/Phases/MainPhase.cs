using System.Threading;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class MainPhase : GamePhase {
		protected override GamePhase Run(GameLogic game) {
			return new UpgradeShopPhase();

			//throw new System.NotImplementedException();
		}

		public override GameState NewState { get; }

		public override void Notify(ActionType action) {
			throw new System.NotImplementedException();
		}

		public override bool Notify(GenericEvent action) => throw new System.NotImplementedException();
	}
}