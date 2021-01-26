using System.Threading;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases
{
	public class LobbyPhase : GamePhase
	{
		private bool started;
		protected override GamePhase Run(GameLogic game){
			while (!started){
				Thread.Yield();
			}
			return new RobotPickingPhase();
		}
		
		public override GameState NewState => GameState.LOBBY;
		public override void Notify(ActionType action)
		{
			started = action == ActionType.STARTGAME;
		}

		public override bool Notify(GenericEvent action) {
			return action.Data is JoinEvent;
		}
	}
}