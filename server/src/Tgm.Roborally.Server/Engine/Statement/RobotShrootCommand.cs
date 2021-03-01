using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Statement {
	public class RobotShrootCommand  : RobotCommand{
		public override Instruction Type => Instruction.Shroot;

		public override string Description => "Shrouutz a lazer forward";

		public override int Times => 1;

		public override void Do(GameLogic game, int robotId) => game.Movement.Shroot(robotId);
	}
}