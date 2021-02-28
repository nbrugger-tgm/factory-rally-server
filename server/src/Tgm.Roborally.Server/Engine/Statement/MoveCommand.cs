using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Statement {
	public class MoveCommand : RobotCommand {
		public const string FIELDS = "fields";

		public MoveCommand(int fields) {
			this[FIELDS] = fields;
		}

		public override string Description => $"Move {FIELDS} fields forward";

		/// <inheritdoc />
		public override int Times => this[FIELDS];

		public override Instruction Type => Instruction.Move;

		public override void Do(GameLogic game, int robotId) =>
			game.Movement.Move(robotId, this[FIELDS], RelativeDirection.Forward);
	}
}