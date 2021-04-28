using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Statement {
	public class EnergyCommand : RobotCommand {
		private const string ENERGY = "energy";


		public EnergyCommand(int energy) {
			this[ENERGY] = energy;
		}

		public override string Description => $"Receive {{{ENERGY}}} energy";

		/// <inheritdoc />
		public override int Times => 1;

		public override Instruction Type => Instruction.Energy;

		public override void Do(GameLogic game, int robotId) {
			RobotInfo robot = game.Entitys[robotId] as RobotInfo;
			robot.EnergyCubes += this[ENERGY];
			game.CommitEvent(new EnergyGainEvent() {  Robot = robotId, Ammount = this[ENERGY] });
		}
	}
}