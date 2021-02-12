using System;
using System.Collections.Generic;
using System.Threading;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases {
	public class UpgradeShopPhase : GamePhase {
		private          GameLogic _game;
		private          int       _activePlayer = 0;
		private          bool      _shopFilled;
		private          long      _endTime;
		private const    int       TIME      = 20000;
		private readonly object    _listener = new object();
		private          bool      _executed;

		protected override GamePhase Run(GameLogic game) {
			this._game = game;
			game.Upgrades.fillShop();
			_shopFilled = true;
			//TODO calculate order based on prio beacon
			for (_activePlayer = 0; _activePlayer < game.PlayerCount; _activePlayer++) {
				_endTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + TIME;
				lock (_listener) {
					_executed = false;
					Monitor.Wait(_listener, TIME);
					if (!_executed) {
						game.CommitEvent(new TimeElapsedEvent() {
							OriginalDuration = TIME,
							Context          = ("player:" + _activePlayer, "action:buy_upgrade")
						});
					}
				}
			}

			return new PreProgrammingPhase(); //todo
		}

		public override GameState NewState => GameState.PLAYING;

		public override void Notify(ActionType action) {
		}

		public override bool Notify(GenericEvent action) {
			if (action.GetEventType() == EventType.UpgradePurchase) {
				PurchaseEvent @event = action.Data as PurchaseEvent;
				if (@event.Player != _activePlayer)
					throw new GameFlowException(GameFlowException.BAD_EVENT);
				lock (_listener) {
					_executed = true;
					Monitor.Pulse(_listener);
				}

				return true;
			}

			if (action.GetEventType() == EventType.ActivateUpgrade) {
				//TODO handle
				return true;
			}

			return action.GetEventType() == EventType.TimeElapsed;
		}

		public override IList<EntityEventOportunity> GetPossibleActions(int robot, int player) {
			if (_game.State == GameState.BREAK || player != _activePlayer || !_shopFilled)
				return new List<EntityEventOportunity>();

			return new List<EntityEventOportunity>() {
				new EntityEventOportunity() {
					EndTime  = _endTime,
					TimeLeft = _endTime - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - 30,
					Type     = EntityActionType.BuyUpgrade
				},
				new EntityEventOportunity() {
					EndTime = _endTime,
					TimeLeft = _endTime - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() -
							   30, //-30 to correct timing with ping
					Type = EntityActionType.Pass
				}
			};
		}
	}
}