using System;
using Tgm.Roborally.Server.Engine.Statement;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	public class MovementManager {
		private readonly GameLogic _game;

		public MovementManager(GameLogic game) {
			_game = game;
		}

		public void Move(int robotId, int i, RelativeDirection forward) {
			Console.Out.WriteLine("Robot : "+robotId+" moves "+i+" fields "+forward);
			RobotInfo robotInfo       = _game.Entitys[robotId] as RobotInfo;
			Direction       resultDirection = ResolveDirection(forward, robotInfo.Direction);
			_game.CommitEvent(new MovementEvent() {
				Direction = resultDirection,
				Entity = robotId,
				From = robotInfo.Location,
				MovementAmmount = i,
				Rotation = Rotation.Left,
				RotationTimes = 0,
				To = Translate(robotInfo.Location,i,resultDirection)
			});
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

		private static Direction ResolveDirection(RelativeDirection direction, Direction orientation) => direction switch {
			RelativeDirection.Forward   => orientation,
			RelativeDirection.Backwards => inverse(orientation),
			RelativeDirection.Right     => roateRight(orientation),
			_ /*Left*/                  => rotateLeft(orientation)
		};

		static Direction inverse(Direction dir) {
			return dir switch {
				Direction.Up    => Direction.Down,
				Direction.Left  => Direction.Right,
				Direction.Right => Direction.Left,
				Direction.Down  => Direction.Up,
				//should not be reachable
				_ => default
			};
		}
		static Direction roateRight(Direction dir) {
			return dir switch {
				Direction.Up    => Direction.Right,
				Direction.Left  => Direction.Up,
				Direction.Right => Direction.Down,
				Direction.Down  => Direction.Left,
				//should not be reachable
				_ => default
			};
		}
		static Direction rotateLeft(Direction dir) {
			return dir switch {
				Direction.Up    => Direction.Left,
				Direction.Left  => Direction.Down,
				Direction.Right => Direction.Up,
				Direction.Down  => Direction.Right,
				//should not be reachable
				_ => default
			};
		}
	}
}