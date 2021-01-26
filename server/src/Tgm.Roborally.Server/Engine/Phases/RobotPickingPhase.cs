using System;
using System.Collections.Generic;
using System.Linq;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases
{
	public class RobotPickingPhase : GamePhase
	{
		protected override GamePhase Run(GameLogic game) {
			//TODO: Later on maybe a more intelligent algorithm will take place here
			
			List<Robots> available = Enum.GetValues(typeof(Robots)).Cast<Robots>().ToList();
			foreach (Player gamePlayer in game.Players) {
				Robots type = available[0];
				Event e = game.Entitys.PickRobo(type,gamePlayer);
				game.CommitEvent(e);
				available.Remove(type);
			}

			return new MainPhase();
		}

		public override GameState NewState => GameState.PLANNING;
		public override void Notify(ActionType action) {
			throw new NotImplementedException("This phase cant be paused (received: "+action+")");
		}

		public override bool Notify(GenericEvent action) {
			return action.GetEventType() == EventType.LockIn;
			throw new NotImplementedException("The picking phase works automaticaly at the moment, no need for events (recived event: "+action.GetEventType()+")");
		}
	}
}