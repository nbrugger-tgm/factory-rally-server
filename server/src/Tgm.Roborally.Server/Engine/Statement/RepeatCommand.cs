using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Statement {
	public class RepeatCommand : RobotCommand {

		public RepeatCommand() {

		}

		public override string Description => $"Repeats the command of the register before";

		/// <inheritdoc />
		public override int Times => 1;

		public override Instruction Type => Instruction.Repeat;

		public override void Do(GameLogic game, int robotId) {
			int currentRegister = game.executionState.CurrentRegister;
			int[] register = game.Programming.GetRegister(robotId);
			for(int i = 1; i <= currentRegister; i++) {
				int cardId = register[currentRegister - 1 * i];
				RobotCommand command = game.Programming[cardId];
				if(command.Type.Equals(Instruction.Repeat))
					continue;
				command.Execute(game, robotId);
				return;
			}
			game.Movement.Move(robotId, 1, RelativeDirection.Forward);
		}
	}
}