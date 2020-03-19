using System.Threading;

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
		}
	}
}