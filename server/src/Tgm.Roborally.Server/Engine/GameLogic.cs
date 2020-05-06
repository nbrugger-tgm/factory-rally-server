using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Engine.Managers;
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
		public Map Map => _map;
		private Map _map;

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
		public EntityManager Entitys { get; }

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

		public Player GetPlayer(int id)
		{
			try
			{
				return Players.Find(e => e.Id == id);
			}
			catch (ArgumentNullException e)
			{
				return null;
			}
		}

		public Player Join(string password)
		{
			if (!Joinable)
			{
				throw new GameNotJoinableException("The game cannot be joined at the moment");
			}

			if (CreatedBy.Password != null && !password.Equals(CreatedBy.Password))
			{
				throw new AuthenticationException("The provided password was wrong");
			}

			Player p = new Player {Id = NextPlayerID()};
			Players.Add(p);
			return p;
		}

		public class GameNotJoinableException : Exception
		{
			public GameNotJoinableException(string theGameCannotBeJoinedAtTheMoment) : base(
				theGameCannotBeJoinedAtTheMoment)
			{
			}
		}

		public void RemovePlayer(int playerId)
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
				throw new PlayerNotRemoveableException("The player is not removeable in this state of the game");
			}
		}

		public class PlayerNotRemoveableException : Exception
		{
			public PlayerNotRemoveableException(string? message) : base(message)
			{
			}
		}

		public void StartGame()
		{
			if (_state != GameState.LOBBY)
			{
				throw new WrongStateException(GameState.LOBBY, _state, "Start Game");
			}

			if (Players.Count == 0)
			{
				throw new PlayerCountException(">0", Players.Count, "Start Game");
			}

			if (Players.Count < MaxPlayers && CreatedBy.FillWithBots)
			{
				//Todo fill with bots
			}

			_map = new Map(20, 20);
			thread.Notify(ActionType.STARTGAME);
		}

		public void NotifyThread(ActionType type) => thread.Notify(type);
	}
}