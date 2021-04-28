using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class ProgrammingPhase : GamePhase {
		private readonly object _lock          = new object();
		private readonly int    _timerDuration = 30000;
		private          long   end;
		private          bool   timerUp;

		/// <inheritdoc />
		protected override object Information => null;

		public override GameState NewState => GameState.PLAYING;

		protected override GamePhase Run(GameLogic game) {
			lock (_lock) {
				#region start timer
				Thread.Sleep(game.AnimationDelay);
				game.CommitEvent(new ProgrammingTimerStartEvent {
					End     = end = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + _timerDuration,
					Seconds = _timerDuration / 1000
				});
				timerUp = true;
				Monitor.Wait(_lock, _timerDuration);

				#endregion

				#region stop timer

				game.CommitEvent(new EmptyEvent(EventType.ProgrammingTimerStop));
				timerUp = false;

				#endregion

				#region Random card distribution

				foreach (int rid in game.Entitys.Robots) {
					int[] registers = game.Programming.GetRegister(rid);
					int[] empty = registers.Where(predicate: i => i == -1).Select(selector: (_, index) => index)
										   .ToArray();
					if (empty.Length > 0)
						game.CommitEvent(new DummyEvent(EventType.RandomCardDistribution,
														$"Affected robot {rid} will get {empty.Length} cards randomly distributed to registers"));

					foreach (int register in empty) {
						int[] hand = game.Programming.GetHandCards(rid);
						if (hand.Length > 0)
							game.Programming.SetRegister(rid, register, hand[0]);
						Thread.Sleep(game.AnimationDelay/empty.Length);
					}
				}

				#endregion
			}

			return new PostProgrammingPhase();
		}

		public override void Notify(ActionType action) {
			//TODO
		}

		public override bool Notify(GenericEvent ev) =>
			ev.Type == EventType.ProgrammingTimerStart  ||
			ev.Type == EventType.ProgrammingTimerStop   ||
			ev.Type == EventType.TimeElapsed            ||
			ev.Type == EventType.ChangeRegister         ||
			ev.Type == EventType.RandomCardDistribution ||
			ev.Type == EventType.ClearRegister;

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) {
			List<EntityEventOportunity> list = new List<EntityEventOportunity>();
			if (timerUp) {
				list.Add(new EntityEventOportunity {
					EndTime  = end,
					TimeLeft = end - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					Type     = EntityActionType.RegisterRefresh
				});
			}

			return list;
		}
	}
}