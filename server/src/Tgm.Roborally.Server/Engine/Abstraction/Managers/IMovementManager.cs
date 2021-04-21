using System.Collections.Generic;
using Tgm.Roborally.Server.Engine.Statement;
using Tgm.Roborally.Server.Models;
using Action = System.Action;

namespace Tgm.Roborally.Server.Engine.Abstraction.Managers {
	/// <inheritdoc />
	public interface IMovementManager : IManager {

		/// <summary>
		/// Moves an robot in an straight line. Applies collisions
		/// </summary>
		/// <param name="robotId">the id of the robot to move</param>
		/// <param name="amount">the number of fields to move</param>
		/// <param name="forward">the relative direction to move to</param>
		/// <returns>the new position of the robot after the movement executed</returns>
		public Position Move(int robotId, int amount, RelativeDirection forward);

		/// <summary>
		/// Moves an robot in an straight line. Applies collisions
		/// </summary>
		/// <param name="robotInfo">the robot to move</param>
		/// <param name="amount">the number of fields to move</param>
		/// <param name="resultDirection">the absolute direction to move towards (absolute to the player but relative to the map)</param>
		/// <returns>the new position of the robot after the movement executed</returns>
		Position Move(RobotInfo robotInfo, int amount, Direction resultDirection);

		/// <summary>
		/// Damages an robot and emits the coresponding event
		/// </summary>
		/// <param name="robotInfo">the robot to damage</param>
		/// <param name="i">the dmg amount</param>
		public void Damage(RobotInfo robotInfo, int i);

		/// <summary>
		/// Performs the movement on the map and emitts an event
		/// </summary>
		/// <param name="entity">The entity to move</param>
		/// <param name="actualAmount">the fields to move</param>
		/// <param name="resultDirection">the direction to move into</param>
		/// <param name="actions"></param>
		void PerformMove(Entity entity, int actualAmount, Direction resultDirection, List<Action> actions);

		/// <summary>
		/// Rotates the robot into the direction i times
		/// </summary>
		/// <param name="robotId">the id of the robot</param>
		/// <param name="rotationDirection">the direction to rotate in</param>
		/// <param name="i">the count of 90° turns to perform</param>
		public void Rotate(int robotId, Rotation rotationDirection, int i);

		/// <summary>
		/// Make a robot shooting a laser
		/// </summary>
		/// <param name="robotId">the robot to shoot from</param>
		public void Shoot(int robotId);

		/// <summary>
		/// Shoots a ray into the given direction
		/// </summary>
		/// <param name="shooter">The entity who fired the shot (used for emiting the event/s</param>
		/// <param name="pos">the position to fire from</param>
		/// <param name="direction">the direction of the raycast</param>
		/// <param name="penentration">if the laser does shoot through the object to hit another</param>
		void Shoot(int shooter, Position pos, Direction direction, bool penentration);


	}
}
