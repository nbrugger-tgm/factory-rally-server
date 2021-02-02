using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using Tgm.Roborally.Server.Engine.Exceptions;
using Tgm.Roborally.Server.Engine.Managers;
using Tgm.Roborally.Server.Models;

#pragma warning disable 1591

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
		public Map               Map           { get; private set; }
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

		public void CommitEvent(GenericEvent e) {
			EventManager.Notify(e);
			_thread.Notify(e);
		}

		public void CommitEvent(Event e) => CommitEvent(new GenericEvent(e));

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

			GenericEvent e = new GenericEvent(EventType.Join);
			e.Data = new JoinEvent {
				JoinedId = p.Id,
				Unjoin   = false
			};
			CommitEvent(e);
			return p;
		}

		public void RemovePlayer(int playerId) {
			if (_state == GameState.LOBBY)
				Players.Remove(GetPlayer(playerId));
			else if (_state == GameState.PLAYING || _state == GameState.PLANNING) {
				Player p = GetPlayer(playerId);
				p.Active = false;
			}
			else
				throw new PlayerNotRemoveableException("The player is not removeable in this state of the game");
		}

		public void StartGame() {
			if (_state != GameState.LOBBY) throw new WrongStateException(GameState.LOBBY, _state, "Start Game");

			if (Players.Count == 0) throw new PlayerCountException(">0", Players.Count, "Start Game");

			if (Players.Count < MaxPlayers && Rules.FillWithBots) {
				//Todo fill with bots
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

		/// <inheritdoc />
		private class GameNotJoinableException : Exception {
			/// <inheritdoc />
			public GameNotJoinableException(string theGameCannotBeJoinedAtTheMoment) : base(
				theGameCannotBeJoinedAtTheMoment) {
			}
		}

		/// <inheritdoc />
		private class PlayerNotRemoveableException : Exception {
			/// <inheritdoc />
			public PlayerNotRemoveableException(string message) : base(message) {
			}
		}

		public void BuyUpgrade(int playerId, int upgrade) {
			//Todo: proper implementation
			Player p = GetPlayer(playerId);
			if (p.ControlledEntities.Count != 1) {
				throw new ActionException("Multiple Robots per player are not implemented.");
			}

			Upgrades.Buy(upgrade, p.ControlledEntities[0]);
			CommitEvent(new GenericEvent(EventType.UpgradePurchase) {
				Data = new Dictionary<string, object>() {
					{"player", playerId},
					{"upgrade", upgrade},
					{"importantMessage", "THIS IS NOT THE EVENT THAT WILL OCCUR IN PRODUCTION!!"}
				}
			});
		}
	}
}