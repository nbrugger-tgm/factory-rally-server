using System;
using System.Collections.Generic;
using System.Threading;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Engine.Managers;
using Tgm.Roborally.Server.Engine.Phases;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	public class GameThread {
		private GamePhase currentPhase;
		private Type      startPhase;

		public GameThread(GameLogic game) {
			this.game = game;
			Thread    = new Thread(run);
		}

		private GameLogic game   { get; }
		private Thread    Thread { get; }

		public IEnumerable<EntityEventOportunity> PossibleEntityActions(int robot, int player) =>
			currentPhase.GetPossibleActions(robot, player);

		public void Start() => Thread.Start();

		private void run() {
			currentPhase = game.StartingPhase;
			startPhase   = currentPhase.GetType();
			while (currentPhase != null) currentPhase = currentPhase.Start(game);

			Console.Out.WriteLine("\nGame " + game.id + " ended wait 10 seconds before deletion");
			Thread.Sleep(10000);
			Console.Out.WriteLine("Delete Game " + game.id + " to save memory\n");
			GameManager.Instance.Games.Remove(game.id);
		}

		public void Notify(ActionType action) {
			if (currentPhase.GetType() != startPhase && action == ActionType.STARTGAME)
				throw new WrongStateException(GameState.LOBBY, GameState.PLAYING, action.ToString());
			currentPhase.Notify(action);
		}

		public void Notify(GenericEvent action) {
			if (action.GetEventType() == EventType.GamePhaseChanged ||
				action.GetEventType() == EventType.GameRoundPhaseChanged)
				return;
			if (
				action.GetEventType() != EventType.GameStart &&
				action.GetEventType() != EventType.Pause     &&
				action.GetEventType() != EventType.Unpause) {
				if (!currentPhase.Notify(action))
					throw new BadEventException(action.GetEventType().ToString(), currentPhase.GetType().FullName);
			}
			else
				currentPhase.Notify(action);
		}
	}
}