using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases
{
	public class LobbyPhase : GamePhase
	{
		protected override void Run(GameLogic game)
		{
			
		}
		
		public override GameState NewState => GameState.LOBBY;
	}
}