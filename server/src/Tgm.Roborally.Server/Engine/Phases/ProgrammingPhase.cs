using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class ProgrammingPhase : GamePhase {
		/// <inheritdoc />
		protected override object Information => null;
		private object _lock          = new object();
		private int   _timerDuration = 30000;
		protected override GamePhase Run(GameLogic game) {
			lock (_lock) {
				game.CommitEvent(new ProgrammingTimerStartEvent{
					End     = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() +_timerDuration,
					Seconds = (int) (_timerDuration /1000)
				});
				Monitor.Wait(_lock,_timerDuration);
				game.CommitEvent(new EmptyEvent(EventType.ProgrammingTimerStop));
			}
			return new PostProgrammingPhase();
		}

		public override GameState NewState => GameState.PLAYING;

		public override void Notify(ActionType action) { //TODO
		}

		public override bool Notify(GenericEvent action) => 
			action.Type == EventType.ProgrammingTimerStart ||
			action.Type == EventType.ProgrammingTimerStop ||
			action.Type == EventType.TimeElapsed ||
			action.Type == EventType.ChangeRegister ||
			action.Type == EventType.ClearRegister;
		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) => new List<EntityEventOportunity>();
	}
}