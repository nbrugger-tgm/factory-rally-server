using System;
using System.Collections.Generic;
using System.Threading;
using Tgm.Roborally.Server.Engine.Managers;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class MainPhase : GamePhase {
		protected override object Information => null;

		public override GameState NewState => GameState.PLAYING;

		protected override GamePhase Run(GameLogic game) {
			Map toUse = MapManager.Instance.Get("Default");
			if (toUse == null) {
				toUse = new Map();
				toUse[1, 1] = new Tile {
					Type = TileType.PrioCore
				};
				for (int i = 0; i < game.Entitys.Robots.Count; i++) {
					toUse[2, i] = new Tile() {
						Type = TileType.Spawn
					};
				}
			}
			else {
				if (toUse.Find(TileType.Spawn).Count < game.Entitys.Robots.Count) {
					game.CommitEvent(new DummyEvent(EventType.GameEndEvent,"The game has ended due to an error : \"The choosen map has not enought spawn points\""));
				}
			}

			game.Map = toUse;

			game.Entitys.PlaceRobotsOnSpawn();
			
			game.CommitEvent(new EmptyEvent(EventType.MapCreated));
			Thread.Sleep(game.AnimationDelay);
			return new UpgradeShopPhase();
			//throw new System.NotImplementedException();
		}

		public override void Notify(ActionType action) => //TODO proper reaction
			Console.Out.WriteLine("Unexpected Action " + action + " in MainPhase");

		public override bool Notify(GenericEvent ev) {
			bool allow = ev.Type == EventType.MapCreated;
			if (!allow)
				Console.Out.WriteLine("Unexpected " + ev.Data +
									  " Event in Main phase"); //TODO: proper implementation
			return allow;
		}

		public override IList<EntityEventOportunity> GetPossibleActions(int a, int b) =>
			new List<EntityEventOportunity>();
	}
}