using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Statement {
	public class MoveCommand : RobotCommand {
		public MoveCommand(int fields) {
			this[FIELDS] = fields;
		}

		public const string FIELDS = "fields";

		public override string Description => $"Move {FIELDS} fields forward";

		/// <inheritdoc />
		public override int Times => this[FIELDS];

		public override void Do(GameLogic game, int robotId) {
			game.Movement.Move(robotId, this[FIELDS], RelativeDirection.Forward);
		}

		public override Instruction Type => Instruction.Move;

	}
}