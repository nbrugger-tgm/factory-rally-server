using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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


		private GameRules CreatedBy { get; }

		public int playerOnTurn;
		public int Id;
		public bool PlayerNamesVisible => CreatedBy.PlayerNamesVisible;
		public string Name => CreatedBy.Name;
		public int MaxPlayers => CreatedBy.MaxPlayers;
		private List<Player> Players = new List<Player>();
		public List<int> PlayerIds => Players.Select(e => e.Id).ToList();

		public bool Joinable => (State == GameState.LOBBY && Players.Count < MaxPlayers);

		public int NextPlayerID()
		{
			int id;
			do
			{
				id = new Random().Next(8);
			} while (GetPlayer(id) != null);

			return id;
		}
		
		public Player GetPlayer(int id, ref IActionResult response)
		{
			if (response != null)
				return null;
			Player p;
			if ((p = Players.Find(e => e.Id == id)) == null)
				response = new NotFoundResult();
			else
				response = null;
			return p;
		}

		public Player GetPlayer(int id)
		{
			IActionResult res = null;
			return GetPlayer(id, ref res);
		}

		public GameLogic(GameRules r)
		{
			ActionHandler = new GameActionHandler(this);
			Hardware = new HardwareManager(this);
			Info = new GameInfo(this);
			Game = new Game(this);
			CreatedBy = r;
		}

		public void Join(string password, ref IActionResult response)
		{
			if (response != null)
				return;
			if (!Joinable)
			{
				response = new ConflictResult();
				return;
			}

			Player p = new Player {Id = NextPlayerID()};
			Players.Add(p);
			
		}
	}
}