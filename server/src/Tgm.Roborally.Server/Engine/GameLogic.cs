using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Engine.KI;
using Tgm.Roborally.Server.Engine.Managers;
using Tgm.Roborally.Server.Models;

//#pragma warning disable 1591

namespace Tgm.Roborally.Server.Engine {
	public class GameLogic {
		private readonly GameThread                            _thread;
		private          GameState                             _state = GameState.LOBBY;
		private readonly Dictionary<string, int>               _consumerKeys = new Dictionary<string, int>();
		private readonly Dictionary<int, ConsumerRegistration> _consumers = new Dictionary<int, ConsumerRegistration>();
		public           int                                   id;
		public           int                                   playerOnTurn;


		public GameLogic(GameRules r) {
			Rules         = r;
			EventManager  = new EventManager(this);
			ActionHandler = new GameActionHandler(this);
			Hardware      = new HardwareManager(this);
			Info          = new GameInfo(this);
			Game          = new Game(this);
			_thread       = new GameThread(this);
			Entitys       = new EntityManager(this);
			Upgrades      = new UpgradeManager(this);
			Programming   = new ProgrammingManager(this);
			_thread.Start();
		}

		public readonly ProgrammingManager Programming;

		public GameState LastState { get; private set; }

		public string            Password      => Rules.Password;
		public List<Player>      Players       { get; } = new List<Player>();
		public Map               Map           { get; set; }
		public EventManager      EventManager  { get; }
		public HardwareManager   Hardware      { get; }
		public EntityManager     Entitys       { get; }
		public GameActionHandler ActionHandler { get; }
		public Game              Game          { get; }
		public GameInfo          Info          { get; }
		public UpgradeManager    Upgrades      { get; }

		public GameState State {
			get => _state;
			set {
				LastState = State;
				_state    = value;
			}
		}


		public GameRules Rules { get; }

		public bool      PlayerNamesVisible => Rules.PlayerNamesVisible;
		public string    Name               => Rules.Name;
		public int       MaxPlayers         => Rules.MaxPlayers;
		public List<int> PlayerIds          => Players.Select(selector: e => e.Id).ToList();

		public bool Joinable => State == GameState.LOBBY && Players.Count < MaxPlayers;

		public Dictionary<int, ConsumerRegistration> Consumers   => _consumers;
		public int                                   PlayerCount => Players.Count;

		public IList<EntityEventOportunity> PossibleEntityActions(int robot, int player) =>
			_thread.PossibleEntityActions(robot, player);


		public void CommitEvent(Event e) {
			EventManager.Notify(e);
			_thread.Notify(new GenericEvent(e));
		}


		private int NewPlayerId() {
			int newId;
			do {
				newId = new Random().Next(1, MaxPlayers + 1);
			} while (GetPlayer(newId) != null);

			return newId;
		}

		public Player GetPlayer(int playerId) {
			try {
				return Players.Find(match: e => e.Id == playerId);
			}
			catch (ArgumentNullException e) {
				return null;
			}
		}

		public Player Join(string name) => Join(null, name);

		public Player Join(string password, string name) {
			if (!Joinable) throw new GameNotJoinableException("The game cannot be joined at the moment");

			if (password       == null && Rules.Password != null ||
				Rules.Password != null && password       != null && !password.Equals(Password))
				throw new AuthenticationException("The provided password was wrong or null");

			Player p = new Player {Id = NewPlayerId(), DisplayName = name};
			Players.Add(p);

			CommitEvent(new JoinEvent {
				JoinedId = p.Id,
				Unjoin   = false
			});
			return p;
		}

		public void RemovePlayer(int playerId) {
			if (_state == GameState.LOBBY) {
				Players.Remove(GetPlayer(playerId));
				CommitEvent(new JoinEvent {
					JoinedId = playerId,
					Unjoin = true
				});
			}
			else if (_state == GameState.PLAYING || _state == GameState.PLANNING) {
				Player p = GetPlayer(playerId);
				p.Active = false;
			}
			else
				throw new PlayerNotRemoveableException("The player is not removeable in this state of the game");
		}

		private static String[] kis = {"Jarvis", "ExMachina", "Ultron", "Vision", "Ordis", "Suda", "Simaris"};

		public void StartGame() {
			if (_state != GameState.LOBBY) throw new WrongStateException(GameState.LOBBY, _state, "Start Game");

			if (Players.Count == 0) throw new PlayerCountException(">0", Players.Count, "Start Game");

			int    kiCount = 1;
			Random rng     = new Random();
			while (Players.Count < MaxPlayers && Rules.FillWithBots) {
				int botId = NewPlayerId();
				RobotKI ki = new RobotKI {
					Id          = botId,
					DisplayName = kis[rng.Next(kis.Length)] + " #" + kiCount++
				};
				Players.Add(ki);
			}

			Map = new Map(20, 20);
			CommitEvent(new ActionEvent(EventType.GameStart));
		}

		public void NotifyThread(ActionType type) => _thread.Notify(type);

		public Player AuthPlayer(string authKey) => Players.Find(match: p => p.auth.Equals(authKey));

		public JoinResponse RegisterConsumer(ConsumerRegistration consumerRegistration) {
			int consumerId;
			do {
				consumerId = new Random().Next(100, 9999);
			} while (_consumers.ContainsKey(consumerId));

			if (_consumers.Count >= 200)
				return null;

			_consumers[consumerId] = consumerRegistration;
			Player pseudo = new Player {
				Id = consumerId
			};
			_consumerKeys[pseudo.auth] = consumerId;
			return new JoinResponse(pseudo);
		}

		public (bool, int) IsConsumer(string authKey) {
			bool a;
			return (a = _consumerKeys.ContainsKey(authKey), a ? _consumerKeys[authKey] : -1);
		}

		public ConsumerRegistration GetConsumer(int resItem2) => _consumers[resItem2];

		/// Thrown when the game cant be joined (eg. it has reached max players
		private class GameNotJoinableException : Exception {
			/// <inheritdoc />
			public GameNotJoinableException(string theGameCannotBeJoinedAtTheMoment) : base(
				theGameCannotBeJoinedAtTheMoment) {
			}
		}

		/// Thrown when a player cant leave the game
		private class PlayerNotRemoveableException : Exception {
			/// <inheritdoc />
			public PlayerNotRemoveableException(string message) : base(message) {
			}
		}

		public void BuyUpgrade(int playerId, int upgrade, int exchange) {
			Player p = GetPlayer(playerId);
			if (p.ControlledEntities.Count != 1) {
				throw new ActionException("Multiple Robots per player are not implemented.");
			}

			int       roboId = p.ControlledEntities[0];
			RobotInfo robo   = (RobotInfo) Entitys[roboId];
			ActionCheck(playerId, roboId, EntityActionType.BuyUpgrade);
			if (Upgrades.GetEntityUpgrades(roboId).Count >= 3) {
				ExchangeUpgrade(roboId, upgrade, exchange);
				return;
			}

			Upgrades.Buy(upgrade, p.ControlledEntities[0]);
		}

		/// <summary>
		/// Checks if the action is excecutable and throws an Exception to be returned if not
		/// </summary>
		/// <param name="robot">the id of the robot</param>
		/// <param name="act">The action to check for</param>
		/// <param name="player">the id of the player</param>
		private void ActionCheck(int player, int robot, EntityActionType act) {
			if (!_thread.PossibleEntityActions(robot, player).Select(eop => eop.Type).Contains(act))
				throw new ActionException("This action is not available right now");
		}

		private void ExchangeUpgrade(int playerId, int upgrade, int exchange) {
			Upgrades.Exchange(playerId, upgrade, exchange);
		}
	}
}