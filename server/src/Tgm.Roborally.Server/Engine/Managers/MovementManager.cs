using System;
using Tgm.Roborally.Server.Engine.Statement;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	public class MovementManager {
		private readonly GameLogic _game;

		public MovementManager(GameLogic game) {
			_game = game;
		}

		public int Move(int robotId, int ammount, RelativeDirection forward) {
			Console.Out.WriteLine("Robot : " + robotId + " moves " + ammount + " fields " + forward);
			RobotInfo robotInfo       = _game.Entitys[robotId] as RobotInfo;
			Direction resultDirection = ResolveDirection(forward, robotInfo.Direction);
			return Move(robotInfo, ammount, resultDirection);
		}

		private int Move(RobotInfo robotInfo, int ammount, Direction resultDirection) {
			int      actualAmmount;
			Position newPos;
			int      pushing = -1;
			for (actualAmmount = 0; actualAmmount <= ammount; actualAmmount++) {
				newPos = Translate(robotInfo.Location, actualAmmount + 1, resultDirection);
				Tile tile = _game.Map[newPos.X, newPos.Y];
				//HEIGHT DIFFERENCE BLOCK
				bool onRamp = false; //todo proper implementation
				if (tile.Level != robotInfo.Attitude && !onRamp)
					break;

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
							pushing = roboId;
							goto brk;
						}
					}
				}

				brk:
				{
				}
				;
			}

			if (actualAmmount != 0) PerformMove(robotInfo, actualAmmount, resultDirection);

			if (pushing != -1) {
				RobotInfo pushedRobot = (RobotInfo) _game.Entitys[pushing];
				while (ammount < actualAmmount) {
					_game.CommitEvent(new PushEvent() {
						Ammount = 1,
						PushDirecton = resultDirection,
						PushedId = pushing,
						PusherId = robotInfo.Id
					});
					bool successfullPush = Move(pushedRobot, 1, resultDirection) == 1;
					if (successfullPush) {
						actualAmmount++;
						PerformMove(robotInfo, 1, resultDirection);
					}
					else break;
				}
			}

			return actualAmmount;
		}


		/// <summary>
		///     Performs the movement on the map and emitts an event
		/// </summary>
		/// <param name="robotInfo"></param>
		/// <param name="actualAmmount"></param>
		/// <param name="resultDirection"></param>
		private void PerformMove(RobotInfo robotInfo, int actualAmmount, Direction resultDirection) {
			Position newPos = Translate(robotInfo.Location, actualAmmount, resultDirection);
			_game.CommitEvent(new MovementEvent {
				Direction       = resultDirection,
				Entity          = robotInfo.Id,
				From            = robotInfo.Location,
				MovementAmmount = actualAmmount,
				Rotation        = Rotation.Left,
				RotationTimes   = 0,
				To              = newPos
			});
			robotInfo.Location = newPos;
		}

		private static Position Translate(Position robotInfoLocation, int ammount, Direction resultDirection) {
			Position endPos = new Position(robotInfoLocation.X, robotInfoLocation.Y);
			switch (resultDirection) {
				case Direction.Up:
					endPos.Y -= ammount;
					break;
				case Direction.Down:
					endPos.Y += ammount;
					break;
				case Direction.Left:
					endPos.X -= ammount;
					break;
				case Direction.Right:
					endPos.X += ammount;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(resultDirection), resultDirection, null);
			}

			return endPos;
		}

		private static Direction ResolveDirection(RelativeDirection direction, Direction orientation) =>
			direction switch {
				RelativeDirection.Forward   => orientation,
				RelativeDirection.Backwards => inverse(orientation),
				RelativeDirection.Right     => roateRight(orientation),
				_ /*Left*/                  => rotateLeft(orientation)
			};

		private static Direction inverse(Direction dir) =>
			dir switch {
				Direction.Up    => Direction.Down,
				Direction.Left  => Direction.Right,
				Direction.Right => Direction.Left,
				Direction.Down  => Direction.Up,
				//should not be reachable
				_ => default
			};

		private static Direction roateRight(Direction dir) =>
			dir switch {
				Direction.Up    => Direction.Right,
				Direction.Left  => Direction.Up,
				Direction.Right => Direction.Down,
				Direction.Down  => Direction.Left,
				//should not be reachable
				_ => default
			};

		private static Direction rotateLeft(Direction dir) =>
			dir switch {
				Direction.Up    => Direction.Left,
				Direction.Left  => Direction.Down,
				Direction.Right => Direction.Up,
				Direction.Down  => Direction.Right,
				//should not be reachable
				_ => default
			};
	}
}