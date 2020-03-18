using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json.Serialization;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine
{
	public class GameLogic
	{
		public EventManager EventManager { get; } = new EventManager();
		public HardwareManager Hardware { get; }

		public GameActionHandler ActionHandler { get; }
		
		public Game Game { get; }
		public GameInfo Info { get; }
		public GameState LastState;

		public string Password;

		private GameState _state;
		public GameState State
		{
			get => _state;
			set
			{
				LastState = State;
				_state = value;
			}
		}

		public int playerOnTurn;
		public int Id;
		public string Name; 
		public List<string> Players = new List<string>();


		public GameLogic()
		{
			ActionHandler = new GameActionHandler(this);
			Hardware = new HardwareManager(this);
			Info = new GameInfo(this);
			Game = new Game(this);
		}
	}

}