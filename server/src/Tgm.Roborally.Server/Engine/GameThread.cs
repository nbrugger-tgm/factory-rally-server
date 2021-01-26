using System.Threading;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Engine.Phases;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	public class GameThread {
		private GamePhase currentPhase;

		public GameThread(GameLogic game) {
			this.game = game;
			Thread    = new Thread(run);
		}

		private GameLogic game    { get; }
		private Thread    Thread  { get; }
		public  void      Start() => Thread.Start();

		private void run() {
			currentPhase = new LobbyPhase();
			while (currentPhase != null){
				currentPhase = currentPhase.Start(game);
			}
		}

		public void Notify(ActionType action) {
			currentPhase.Notify(action);
		}

		public void Notify(GenericEvent action) {
			if(action.GetEventType() != EventType.GamePhaseChanged)
				if (!currentPhase.Notify(action))
					throw new BadEventException(action.GetEventType().ToString());
		}
	}
}