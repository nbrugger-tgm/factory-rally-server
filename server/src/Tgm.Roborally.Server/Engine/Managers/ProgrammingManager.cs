using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	public class ProgrammingManager {
		private GameLogic                    _game;
		private Dictionary<int,RobotCommand> _pool = new Dictionary<int, RobotCommand>();
		public ProgrammingManager(GameLogic game) {
			_game = game;
		}

		public ISet<int>              IDs       => _pool.Keys.ToHashSet();
		public IList<RobotCommand>    Cards     => _pool.Values.ToList();
		private Dictionary<int, int[]> Registers => new Dictionary<int, int[]>();

		public void Clear(int robotId) {
			throw new System.NotImplementedException();
		}

		public RobotCommand this[int robotId] => !_pool.ContainsKey(robotId) ? null : _pool[robotId];

		public int[] GetRegister(int robotId) {
			if (!Registers.ContainsKey(robotId))
				Registers[robotId] = new int[5];
			return Registers[robotId];
		}
	}
}