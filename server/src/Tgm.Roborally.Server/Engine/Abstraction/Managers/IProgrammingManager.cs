using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Abstraction.Managers {
	public interface IProgrammingManager {
		ISet<int>           IDs   { get; }
		IList<RobotCommand> Cards { get; }
		ISet<int>           Deck  { get; }

		/// <summary>
		/// Get the Command for the regarding id
		/// </summary>
		/// <param name="robotId">the id of the command</param>
		RobotCommand this[int robotId] { get; }

		/// <summary>
		/// Clears the registers of the robot
		/// </summary>
		/// <param name="robotId">the id of the robot</param>
		void  Clear(int        robotId);
		/// <summary>
		/// Get all the regisers of the specified robot
		/// </summary>
		/// <param name="robotId">the id of the robot</param>
		/// <returns>the ids of the cards in his registers</returns>
		int[] GetRegister(int  robotId);
		/// <summary>
		/// Make a robot draw his cards<br/>
		/// According to the game rules the amount of cards drawn is related to the health of the robot
		/// </summary>
		/// <param name="robot">the robot that should draw the card</param>
		void  Draw(int         robot);
		/// <summary>
		/// Return the programming cards in the hand of the robot
		/// </summary>
		/// <param name="rid">the ID of the robot</param>
		/// <returns>the ids of the cards as array</returns>
		int[] GetHandCards(int rid);
		/// <summary>
		/// Changes the content of a robots register
		/// </summary>
		/// <param name="rid">the id of the roboter</param>
		/// <param name="register">the index of the register to set</param>
		/// <param name="card">the id of the programming card to set it to</param>
		void  SetRegister(int  rid, int register, int card);
	}
}