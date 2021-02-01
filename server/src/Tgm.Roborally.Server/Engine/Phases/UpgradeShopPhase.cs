using System;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class UpgradeShopPhase : GamePhase {
		private GameLogic game;

		protected override GamePhase Run(GameLogic game) {
			this.game = game;
			game.Upgrades.drawShop();
			return null; //todo
		}

		public override GameState NewState => GameState.PLAYING;

		public override void Notify(ActionType action) {
		}

		public override bool Notify(GenericEvent action) {
			if (action.GetEventType() == EventType.UpgradePurchase) {
				//TODO handle data
				return true;
			}
			else if (action.GetEventType() == EventType.ActivateUpgrade) {
				//TODO handle
				return true;
			}

			return false;
		}
	}
}