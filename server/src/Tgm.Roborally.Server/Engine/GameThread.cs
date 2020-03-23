using System.Threading;
using Tgm.Roborally.Server.Engine.Phases;

namespace Tgm.Roborally.Server.Engine
{
	public class GameThread
	{
		private GameLogic game { get; }

		public GameThread(GameLogic game)
		{
			this.game = game;
			Thread = new Thread(run);
		}

		private Thread Thread { get; }
		public void Start() => Thread.Start();

		private void run()
		{
			LobbyPhase phase = new LobbyPhase();
			phase.Start(game);
		}
	}
}