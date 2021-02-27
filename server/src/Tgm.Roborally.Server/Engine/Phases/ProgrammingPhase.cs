using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class ProgrammingPhase : GamePhase {
		/// <inheritdoc />
		protected override object Information => null;
		private object _lock          = new object();
		private int    _timerDuration = 30000;
		private bool   timerUp        = false;
		private long   end;
		protected override GamePhase Run(GameLogic game) {
			lock (_lock) {
				#region start timer
				game.CommitEvent(new ProgrammingTimerStartEvent{
					End     = end = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() +_timerDuration,
					Seconds = (int) (_timerDuration /1000)
				});
				timerUp = true;
				Monitor.Wait(_lock,_timerDuration);
				#endregion

				#region stop timer

				game.CommitEvent(new EmptyEvent(EventType.ProgrammingTimerStop));
				timerUp = false;

				#endregion

				#region Random card distribution

				foreach (int rid in game.Entitys.Robots) {
					int[] registers = game.Programming.GetRegister(rid);
					int[] empty     = registers.Where(i => i == -1).Select(selector: (value, index) => index).ToArray();
					if(empty.Length > 0)
						game.CommitEvent(new DummyEvent(EventType.RandomCardDistribution, $"Affected robot {rid} will get {empty.Length} cards randomly distributed to registers"));
					
					foreach (int register in empty) {
						int[] hand = game.Programming.GetHandCards(rid);
						if(hand.Length>0)
							game.Programming.SetRegister(rid, register, hand[0]);
					}
				}

				#endregion
				
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
		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) {
			List<EntityEventOportunity> list = new List<EntityEventOportunity>();
			if(timerUp)
				list.Add(new EntityEventOportunity() {
					EndTime = end,
					TimeLeft = end-DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					Type = EntityActionType.RegisterRefresh
				});
			return list;
		}
	}
}