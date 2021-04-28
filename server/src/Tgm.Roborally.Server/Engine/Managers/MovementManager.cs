using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tgm.Roborally.Server.Engine.Abstraction.Managers;
using Tgm.Roborally.Server.Engine.Statement;
using Tgm.Roborally.Server.Models;
using Action = System.Action;

namespace Tgm.Roborally.Server.Engine.Managers {
	/// <inheritdoc />
	public class MovementManager : IMovementManager {
		private readonly GameLogic _game;

		public MovementManager(GameLogic game) {
			_game = game;
		}

		/// <summary>
		/// Moves an robot in an straight line. Applies collisions
		/// </summary>
		/// <param name="robotId">the id of the robot to move</param>
		/// <param name="amount">the number of fields to move</param>
		/// <param name="forward">the relative direction to move to</param>
		/// <returns>the new position of the robot after the movement executed</returns>
		/// <exception cref="ArgumentOutOfRangeException">If the robot is not fetchable</exception>
		public Position Move(int robotId, int amount, RelativeDirection forward) {
#if DEBUG
			Console.Out.WriteLine("Robot : " + robotId + " moves " + amount + " fields " + forward);
#endif
			if (_game.Entitys[robotId] is RobotInfo { } robotInfo) {
				Direction resultDirection = DirectionExtension.ResolveDirection(forward, robotInfo.Direction);
				return Move(robotInfo, amount, resultDirection);
			}
			else
				throw new ArgumentOutOfRangeException($"There is no robot with the id {robotId}");
		}

		/// <summary>
		/// Moves an robot in an straight line. Applies collisions
		/// </summary>
		/// <param name="robotInfo">the robot to move</param>
		/// <param name="amount">the number of fields to move</param>
		/// <param name="resultDirection">the absolute direction to move towards (absolute to the player but relative to the map)</param>
		/// <returns>the new position of the robot after the movement executed</returns>
		/// <exception cref="ArgumentOutOfRangeException">If the robot is not fetchable</exception>
		public Position Move(RobotInfo robotInfo, int amount, Direction resultDirection) {
			for (int actualAmount = 0; actualAmount < amount; actualAmount++) {
				Position     newPos = robotInfo.Location.Translate(1, resultDirection);
				List<Action> events = new List<Action>();
				if (!robotInfo.Virtual && robotInfo.Health <= 0)
					break;

				//FALL OF MAP
				if (!_game.Map.IsWithin(newPos)) {
					events.Add(() => Damage(robotInfo, 20));
					foreach (Action e in events) e();
					break;
				}

				Tile tile = _game.Map[newPos.X, newPos.Y];

				//HEIGHT DIFFERENCE BLOCK
				bool onRamp = tile.Type == TileType.Ramp; //todo proper implementation (respect rotation)
				if (tile.Level > robotInfo.Attitude && !onRamp)
					break;

				//FALL ONE LEVEL DOWN
				if (tile.Level < robotInfo.Attitude && !onRamp) {
					//todo actual events
					events.Add(() => _game.CommitEvent(new DummyEvent(EventType.Movement,
																	  $"Robot {robotInfo.Id} change level. From {robotInfo.Attitude} to {tile.Level}")));
					events.Add(() => Damage(robotInfo, (robotInfo.Attitude - tile.Level) * 2));
				}
				else if (tile.Level + 1 == robotInfo.Attitude && onRamp) {
					//todo elevate it
					events.Add(() => _game.CommitEvent(new DummyEvent(EventType.Movement,
																	  $"Robot {robotInfo.Id} change level. From {robotInfo.Attitude} to {tile.Level}")));
				}

				//wall block
				if (tile.Type == TileType.Wall)
					break;

				//One way wall block
				if (tile.Type == TileType.OneWayWall && tile.Direction != resultDirection)
					break;

				//Robot Collission check
				if (!tile.Empty) {
					foreach (int roboId in _game.Entitys.Robots) {
						RobotInfo robo = _game.Entitys[roboId] as RobotInfo;
						if (robo.Location.Equals(newPos) && !robo.Virtual && robo.Attitude == robotInfo.Attitude) {
							_game.CommitEvent(new PushEvent() {
								Ammount      = 1,
								PushDirecton = resultDirection,
								PushedId     = roboId,
								PusherId     = robotInfo.Id
							});
							Move(robo, 1, resultDirection);
						}

						if (robo.Location.Equals(newPos))
							break;
					}
				}

				PerformMove(robotInfo, 1, resultDirection, events);
			}

			return robotInfo.Location;
		}

		/// <summary>
		/// Damages an robot and emits the coresponding event
		/// </summary>
		/// <param name="robotInfo">the robot to damage</param>
		/// <param name="i">the dmg amount</param>
		public void Damage(RobotInfo robotInfo, int i) {
			robotInfo.Health -= i;
			_game.CommitEvent(new DamageEvent() {
				Ammount = i,
				Entity  = robotInfo.Id
			});
			if (robotInfo.Health <= 0)
				_game.CommitEvent(new EmptyEvent(EventType.Shutdown));
		}


		/// <summary>
		///     Performs the movement on the map and emitts an event
		/// </summary>
		/// <param name="entity">The entity to move</param>
		/// <param name="actualAmount">the fields to move</param>
		/// <param name="resultDirection">the direction to move into</param>
		/// <param name="actions"></param>
		public void PerformMove(Entity    entity,
								 int       actualAmount,
								 Direction resultDirection, List<Action> actions) {
			for (int i = 0; i < actualAmount; i++) {
				Position newPos = entity.Location.Translate(1, resultDirection);
				_game.CommitEvent(new MovementEvent {
					Direction       = resultDirection,
					Entity          = entity.Id,
					From            = entity.Location,
					MovementAmmount = 1,
					Rotation        = Rotation.Left,
					RotationTimes   = 0,
					To              = newPos
				});
				entity.Location = newPos;
			}

			foreach (Action action in actions) action();
		}


		/// <summary>
		/// Rotates the robot into the direction i times
		/// </summary>
		/// <param name="robotId">the id of the robot</param>
		/// <param name="rotationDirection">the direction to rotate in</param>
		/// <param name="i">the count of 90° turns to perform</param>
		public void Rotate(int robotId, Rotation rotationDirection, int i) {
#if DEBUG
			Console.Out.WriteLine("Robot : " + robotId + " rotates " + i + " times " + rotationDirection);
#endif
			Entity ent = _game.Entitys[robotId];
			for (int j = 0; j < i; j++) {
				ent.Direction = ent.Direction.Rotate(rotationDirection);
			}

			_game.CommitEvent(new MovementEvent() {
				Entity          = robotId,
				From            = ent.Location,
				To              = ent.Location,
				MovementAmmount = 0,
				Rotation        = rotationDirection,
				RotationTimes   = i
			});
		}

		/// <summary>
		/// Make a robot shooting a laser
		/// </summary>
		/// <param name="robotId">the robot to shoot from</param>
		public void Shoot(int robotId) {
			Entity e = _game.Entitys[robotId];

			Position pos = e.Location;
			Shoot(robotId, pos, e.Direction);
		}

		/// <summary>
		/// Shoots a ray into the given direction
		/// </summary>
		/// <param name="shooter">The entity who fired the shot (used for emiting the event/s</param>
		/// <param name="pos">the position to fire from</param>
		/// <param name="direction">the direction of the raycast</param>
		/// <param name="penentration">if the laser does shoot through the object to hit another</param>

		public void Shoot(int shooter, Position pos, Direction direction, bool penentration = false) {
#if DEBUG
			Console.Out.WriteLine("Robot : " + shooter + " shoots from (" + pos.X + "|" + pos.Y + ") to direction " + direction);
#endif
			Tile t;
			List<int> hitEntities = new List<int>();
			_game.Map.CalculateEmpty();
			do {
				pos = pos.Translate(1, direction);
				if (!_game.Map.IsWithin(pos))
					break;
				t = _game.Map[pos.X, pos.Y];
				hitEntities.AddRange(_game.Entitys.List.Where(entity => entity.Location.Equals(pos))
										  .Select(entity => entity.Id).ToList());
			} while ((!t.Empty && !penentration) || t.Type == TileType.Wall);

			_game.CommitEvent(new ShootEvent() {
				Direction  = direction,
				HitEntitys = hitEntities,
				Shooter    = shooter,
				To         = pos
			});
			foreach (int hitEntity in hitEntities) {
				Damage((RobotInfo) _game.Entitys[hitEntity], 1);
			}
		}

		public void Setup() {
		}
	}
}