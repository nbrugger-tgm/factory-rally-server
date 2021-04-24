using System;
using Tgm.Roborally.Server.Engine.Statement;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Abstraction.Managers {
	/// <summary>
	/// Manages and controls the movement of entities
	/// </summary>
	public interface IMovementManager : IManager {
		string IManager.Name => "Movement Manager";

		/// <summary>
		/// Moves an robot in an straight line. Applies collisions
		/// </summary>
		/// <param name="robotId">the id of the robot to move</param>
		/// <param name="amount">the number of fields to move</param>
		/// <param name="forward">the relative direction to move to</param>
		/// <returns>the new position of the robot after the movement executed</returns>
		/// <exception cref="ArgumentOutOfRangeException">If the robot is not fetchable</exception>
		Position Move(int robotId, int amount, RelativeDirection forward);

		/// <summary>
		/// Damages an robot and emits the corresponding event
		/// </summary>
		/// <param name="robotInfo">the robot to damage</param>
		/// <param name="i">the dmg amount</param>
		void Damage(RobotInfo robotInfo, int i);

		/// <summary>
		/// Rotates the robot into the direction i times
		/// </summary>
		/// <param name="robotId">the id of the robot</param>
		/// <param name="rotationDirection">the direction to rotate in</param>
		/// <param name="i">the count of 90° turns to perform</param>
		void Rotate(int robotId, Rotation rotationDirection, int i);

		/// <summary>
		/// Make a robot shooting a laser
		/// </summary>
		/// <param name="robotId">the robot to shoot from</param>
		void Shoot(int robotId);
	}
}