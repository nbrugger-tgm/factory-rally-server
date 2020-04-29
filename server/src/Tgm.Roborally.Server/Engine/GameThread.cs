using System.Threading;
using Tgm.Roborally.Server.Engine.Phases;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine
{
	public class GameThread
	{
		public GameThread(GameLogic game)
		{
			this.game = game;
			Thread = new Thread(run);
		}

		private GameLogic game { get; }
		private GamePhase currentPhase;
		private Thread Thread { get; }
		public void Start() => Thread.Start();

		private void run()
		{
			currentPhase = new LobbyPhase();
			while (currentPhase != null)
			{
				//TODO game.ComittEvent(new GamePhaseChangedEvent());
				currentPhase = currentPhase.Start(game);
			}
		}

		public void Notify(ActionType action)
		{
			currentPhase.Notify(action);
		}
	}
}