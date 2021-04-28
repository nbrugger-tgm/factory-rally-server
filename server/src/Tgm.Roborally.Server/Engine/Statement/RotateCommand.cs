using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Statement {
	public class RotateCommand : RobotCommand {
		private const   string      DIRECTION = "direction";
		public const    string      TIMES     = "times";
		public override Instruction Type => Instruction.Rotate;

		public override string Description => $"The robot rotates {{{DIRECTION}}} {{{TIMES}}} times";

		public override int Times => this[TIMES];

		public RotateCommand(Rotation dir, int times) {
			this[TIMES]     = times;
			this[DIRECTION] = (int) dir;
		}

		public override void Do(GameLogic game, int robotId) {
			game.Movement.Rotate(robotId, (Rotation) this[DIRECTION], 1);
		}
	}
}