using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Phases
{
	public class RobotPickingPhase : GamePhase
	{
		protected override GamePhase Run(GameLogic game)
		{
			//TODO Later on maybe a more intelligent algorithm will take place here
			return new MainPhase();
		}

		public override GameState NewState => GameState.PLANNING;
		public override void Notify(ActionType action)
		{
		}

		public override void Notify(GenericEvent action) => throw new System.NotImplementedException();
	}
}