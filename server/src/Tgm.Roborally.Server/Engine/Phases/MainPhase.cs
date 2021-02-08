using System;
using System.Collections.Generic;
using System.Threading;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class MainPhase : GamePhase {
		protected override GamePhase Run(GameLogic game) {
			game.Map = new Map();
			game.Map[1, 1] = new Tile() {
				Type = TileType.PrioCore
			};
			game.CommitEvent(new GenericEvent(EventType.Map) {
				Data = "Map Created"
			});
			return new UpgradeShopPhase();
			//throw new System.NotImplementedException();
		}

		public override GameState NewState { get; }

		public override void Notify(ActionType action) {//TODO proper reaction
			Console.Out.WriteLine("Unexpected Action "+action+" in MainPhase");
		}

		public override bool Notify(GenericEvent action) {
			bool allow = action.Type == EventType.Map;
			if (!allow)
				Console.Out.WriteLine("Unexpected "+action.Data+" Event in Main phase");//TODO: proper implementation
			return allow;
		}

		public override IList<EntityEventOportunity> GetPossibleActions(int a, int b) {
			return new List<EntityEventOportunity>();
		}
	}
}