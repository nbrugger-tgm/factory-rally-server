using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Statement {
	public class MoveCommand : RobotCommand {
		private const string FIELDS = "fields";
		private const string DIRECTION = "direction";


		public MoveCommand(int fields, RelativeDirection relativeDir = RelativeDirection.Forward) {
			this[FIELDS] = fields;
			this[DIRECTION] = (int) relativeDir;
		}

		public override string Description => $"Move {{{FIELDS}}} fields {{{DIRECTION}}}" ;

		/// <inheritdoc />
		public override int Times => this[FIELDS];

		public override Instruction Type => Instruction.Move;

		public override void Do(GameLogic game, int robotId) =>
			game.Movement.Move(robotId, 1, (RelativeDirection) this[DIRECTION]);
	}
}