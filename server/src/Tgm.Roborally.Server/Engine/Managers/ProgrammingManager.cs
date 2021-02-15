using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Tgm.Roborally.Server.Engine.Statement;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	public class ProgrammingManager {
		private          GameLogic                                                    _game;
		private readonly 
			Dictionary<int,(RobotCommand command,CardLocation location,int owner)> _pool = 
				new Dictionary<int, (RobotCommand, CardLocation,int)>();
		public ProgrammingManager(GameLogic game) {
			_game = game;

			AddCard(
				5,
			   new MoveCommand(3) {
				   Name        ="Sprint"
			   }
			);
		
			AddCard(
				10,
				new MoveCommand(2) {
					Name ="Move"
				}
			);
			
			AddCard(
				20,
				new MoveCommand(1) {
					Name ="Crawl"
				}
			);
		}

		private void AddCard(int number, RobotCommand moveCommand) {
			for (int i = 0; i < number; i++)
				_pool[_pool.Count] = (moveCommand, CardLocation.DECK,-1);
		}


		public  ISet<int>              IDs       => _pool.Keys.ToHashSet();
		public  IList<RobotCommand>    Cards     => _pool.Values.Select(e => e.command).ToList();
		private Dictionary<int, int[]> Registers => new Dictionary<int, int[]>();

		public IList<int> Deck => _pool.Where(e => e.Value.location == CardLocation.DECK).Select(e => e.Key).ToList();

		public void Clear(int robotId) {
			int[] regs = Registers[robotId];
			for (int i = 0; i < regs.Length; i++) {
				regs[i] = -1;
			}

			Registers[robotId] = regs;
		}

		public RobotCommand this[int robotId] => !_pool.ContainsKey(robotId) ? null : _pool[robotId].command;

		public int[] GetRegister(int robotId) {
			if (!Registers.ContainsKey(robotId))
				Registers[robotId] = new int[5];
			return Registers[robotId];
		}

		public void Draw(int robot) {
			RobotInfo robo = (RobotInfo) _game.Entitys[robot];

			Random    r     = new Random();
			List<int> cards = new List<int>();
			for (int i = 0; i < robo.Health - 1; i++) {
				int                                                      cardId = Deck[r.Next(Deck.Count)];
				(RobotCommand command, CardLocation location, int owner) elem   = _pool[cardId];
				elem.owner    = robot;
				elem.location = CardLocation.IN_HAND;
				_pool[cardId] = elem;
				cards.Add(cardId);
			}

			_game.CommitEvent(new DrawCardEvent {
            	Cards = cards,
            	Count = cards.Count,
				Player = robot//todo rename to robot
			});
		}
	}

	internal enum CardLocation {
		DECK = 0,
		DISCARDED,
		IN_HAND,
		IN_REGISTER
	}
}