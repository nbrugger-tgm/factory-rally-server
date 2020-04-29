using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases
{
	public class MainPhase : GamePhase 
	{
		protected override GamePhase Run(GameLogic game)
		{
			throw new System.NotImplementedException();
		}

		public override GameState NewState { get; }
		public override void Notify(ActionType action)
		{
			throw new System.NotImplementedException();
		}
	}
}