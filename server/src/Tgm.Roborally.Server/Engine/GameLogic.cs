using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine
{
	public class GameLogic
	{
		private GameState _state = GameState.LOBBY;
		public int Id;
		public GameState LastState => _lastState;
		private GameState _lastState;
		public readonly string Password;
		public int playerOnTurn;
		private readonly List<Player> Players = new List<Player>();
		private readonly GameThread thread;

		public GameLogic(GameRules r)
		{
			CreatedBy = r;
			EventManager = new EventManager(this);
			ActionHandler = new GameActionHandler(this);
			Hardware = new HardwareManager(this);
			Info = new GameInfo(this);
			Game = new Game(this);
			thread = new GameThread(this);
			thread.Start();
		}

		public void ComittEvent(Event e) => EventManager.notify(e);

		public EventManager EventManager { get; }
		public HardwareManager Hardware { get; }

		public GameActionHandler ActionHandler { get; }

		public Game Game { get; }
		public GameInfo Info { get; }

		public GameState State
		{
			get => _state;
			set
			{
				_lastState = State;
				_state = value;
			}
		}


		private GameRules CreatedBy { get; }
		public bool PlayerNamesVisible => CreatedBy.PlayerNamesVisible;
		public string Name => CreatedBy.Name;
		public int MaxPlayers => CreatedBy.MaxPlayers;
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

		public void Join(string password, ref IActionResult response)
		{
			if (response != null)
				return;
			if (!Joinable)
			{
				response = new ConflictResult();
				return;
			}

			if (CreatedBy.Password != null && !password.Equals(CreatedBy.Password))
			{
				response = new ForbidResult();
			}

			Player p = new Player {Id = NextPlayerID()};
			Players.Add(p);
			response = new ObjectResult(new JoinResponse(p));
		}

		public void RemovePlayer(int playerId, ref IActionResult response)
		{
			if (_state == GameState.LOBBY)
				Players.Remove(GetPlayer(playerId));
			else if (_state == GameState.PLAYING || _state == GameState.PLANNING)
			{
				Player p = GetPlayer(playerId);
				p.Active = false;
			}
			else
			{
				//Player not removable
				response = new ConflictResult();
				return;
			}

			response = new OkResult();
		}

		public void StartGame()
		{
			if (_state != GameState.LOBBY)
			{
				throw new WrongStateException(GameState.LOBBY, _state, "Start Game");
			}

			if (Players.Count == 0)
			{
				throw new PlayerCountException(">0",Players.Count,"Start Game");
			}

			if (Players.Count < MaxPlayers && CreatedBy.FillWithBots)
			{
				//Todo fill with bots
			}
			thread.Notify(ActionType.STARTGAME);
		}

		public void NotifyThread(ActionType type) => thread.Notify(type);
	}
}