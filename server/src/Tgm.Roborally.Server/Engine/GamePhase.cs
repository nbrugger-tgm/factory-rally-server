using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine
{
	public abstract class GamePhase
	{
		public void Start(GameLogic game)
		{
			game.State = NewState;
			//TODO: game.ComittEvent(new GameActionEvent());
			Run(game);
		}
		protected abstract void Run(GameLogic game);
		public abstract GameState NewState { get; }
	}
}