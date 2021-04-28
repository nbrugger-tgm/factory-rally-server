using System.Collections.Generic;
using System.Collections.Immutable;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Abstraction.Managers {
	/// <inheritdoc />
	public interface IProgrammingManager : IManager {
		/// <summary>
		/// The ids of ALL
		/// </summary>
		ISet<int> IDs { get; }

		IList<RobotCommand> Cards { get; }
		IImmutableSet<int>  Deck  { get; }

		/// <summary>
		/// Get the Command for the regarding id
		/// </summary>
		/// <param name="cardId">the id of the command</param>
		RobotCommand this[int cardId] { get; }

		string IManager.Name => "Programming Manager";

		/// <summary>
		/// Clears the registers of the robot
		/// </summary>
		/// <param name="robotId">the id of the robot</param>
		void Clear(int robotId);

		/// <summary>
		/// Get all the regisers of the specified robot
		/// </summary>
		/// <param name="robotId">the id of the robot</param>
		/// <returns>the ids of the cards in his registers</returns>
		int[] GetRegister(int robotId);

		/// <summary>
		/// Make a robot draw his cards<br/>
		/// According to the game rules the amount of cards drawn is related to the health of the robot
		/// </summary>
		/// <param name="robot">the robot that should draw the card</param>
		void Draw(int robot);

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
		void SetRegister(int rid, int register, int card);

		/// <summary>
		/// Adds 1..N duplicates of a card to the deck
		/// </summary>
		/// <param name="duplicates">the amount of cards of this type to add</param>
		/// <param name="command">the description of the command to add</param>
		public void AddCard(int duplicates, RobotCommand command);
	}
}