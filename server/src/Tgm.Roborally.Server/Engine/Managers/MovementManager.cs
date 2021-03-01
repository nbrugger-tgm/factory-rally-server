using System;
using System.Collections.Generic;
using System.Drawing;
using Tgm.Roborally.Server.Engine.Statement;
using Tgm.Roborally.Server.Models;
using Action = System.Action;

namespace Tgm.Roborally.Server.Engine.Managers {
	public class MovementManager {
		private readonly GameLogic _game;

		public MovementManager(GameLogic game) {
			_game = game;
		}

		public Position Move(int robotId, int amount, RelativeDirection forward) {
			Console.Out.WriteLine("Robot : " + robotId + " moves " + amount + " fields " + forward);
			if (_game.Entitys[robotId] is RobotInfo { } robotInfo) {
				Direction resultDirection = DirectionExtension.ResolveDirection(forward, robotInfo.Direction);
				return Move(robotInfo, amount, resultDirection);
			}
			else
				throw new ArgumentOutOfRangeException($"There is no robot with the id {robotId}");
		}

		private Position Move(RobotInfo robotInfo, int amount, Direction resultDirection) {
			for (int actualAmount = 0; actualAmount < amount; actualAmount++) {
				Position    newPos = robotInfo.Location.Translate( 1, resultDirection);
				List<Action> events = new List<Action>();
				if (!robotInfo.Virtual && robotInfo.Health <= 0)
					break;
				//FALL OF MAP
				if (!_game.Map.IsWithin(newPos)) {;
					events.Add(()=> {
						Damage(robotInfo, 20);
					});
				}
				Tile tile = _game.Map[newPos.X, newPos.Y];
				//HEIGHT DIFFERENCE BLOCK
				bool onRamp = tile.Type == TileType.Ramp; //todo proper implementation
				if (tile.Level > robotInfo.Attitude && !onRamp)
					break;
				//FALL ONE LEVEL DOWN
				if (tile.Level < robotInfo.Attitude && !onRamp) {
					events.Add(()=>_game.CommitEvent(new DummyEvent(EventType.Movement,$"Robot {robotInfo.Id} change level. From {robotInfo.Attitude} to {tile.Level}")));
					events.Add(()=> {
						Damage(robotInfo, (robotInfo.Attitude - tile.Level)*2);
					});
				}else if (tile.Level + 1 == robotInfo.Attitude && onRamp) {
					//todo elevate it
					events.Add(()=>_game.CommitEvent(new DummyEvent(EventType.Movement,$"Robot {robotInfo.Id} change level. From {robotInfo.Attitude} to {tile.Level}")));
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
	                            Ammount = 1,
	                            PushDirecton = resultDirection,
	                            PushedId = roboId,
	                            PusherId = robotInfo.Id
	                        });
							Move(robo, 1, resultDirection);
						}
						if(robo.Location.Equals(newPos))
							break;
					}
				}
				PerformMove(robotInfo, actualAmount, resultDirection);
			}
			return robotInfo.Location;
		}

		private void Damage(RobotInfo robotInfo, int i) {
			robotInfo.Health -= i;
			_game.CommitEvent(new DamageEvent() {
				Ammount = i,
				Entity = robotInfo.Id
			});
			if(robotInfo.Health <= 0)
				_game.CommitEvent(new EmptyEvent(EventType.Shutdown));
		}


		/// <summary>
		///     Performs the movement on the map and emitts an event
		/// </summary>
		/// <param name="entity">The entity to move</param>
		/// <param name="actualAmount">the fields to move</param>
		/// <param name="resultDirection">the direction to move into</param>
		private void PerformMove(
			Entity                                     entity, 
			int                                        actualAmount, 
			Direction                                  resultDirection
			){
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
			
		}



		public void Rotate(int robotId, Rotation rotationDirection, int i) {
			Entity ent = _game.Entitys[robotId];
			for (int j = 0; j < i; j++) {
				ent.Direction = ent.Direction.Rotate(rotationDirection);
			}
			_game.CommitEvent(new MovementEvent() {
				Entity = robotId,
				From = ent.Location,
				To = ent.Location,
				MovementAmmount = 0,
				Rotation = rotationDirection,
				RotationTimes = i
			});
		}
	}
}