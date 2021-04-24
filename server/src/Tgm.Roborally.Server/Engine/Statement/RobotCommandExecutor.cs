namespace Tgm.Roborally.Server.Engine.Statement {
	/// <summary>
	/// The logic of a command, implement this interface to implement a new command
	/// </summary>
	public interface RobotCommandExecutor {
		/// <summary>
		///     Do whatever shuld happen when this card is executed
		/// </summary>
		/// <param name="game"> the contectual game</param>
		/// <param name="robotId"></param>
		public void Do(GameLogic game, int robotId);
	}
}