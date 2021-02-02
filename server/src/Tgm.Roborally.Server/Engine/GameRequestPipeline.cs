using System;
using System.Threading;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	/**
	 * This pipeline processes a request to produce a result
	 */
	public class GameRequestPipeline {
		private readonly PipelineContext _context;

		private GameLogic     _game;
		private IActionResult _response = null;
		private Player        _player;
		private Event         _event;
		private RobotInfo     _robot;

		/// <summary>
		/// Constructs an empty pipeline
		/// </summary>
		public GameRequestPipeline() => _context = new PipelineContext(pipe: this);

		private IActionResult Response {
			get => _response;
			set => _response = value;
		}

		private bool Done => Response != null;


		public GameRequestPipeline Game(int game) {
			if (Done) {
				return this;
			}

			_game = GameManager.instance.GetGame(game);
			if (_game == null) {
				Response = new NotFoundObjectResult(new ErrorMessage() {
					Error   = "Game not found",
					Message = "There is no game with the given ID"
				});
			}

			return this;
		}

		/// <summary>
		/// Executes custom code to create and modify the response or handle the request
		///	Exceptions are handled save
		///
		/// use <code>executeAction</code> or <code>executeSecure</code> to send the response
		/// </summary>
		/// <param name="code">the runnable code</param>
		/// <returns>the pipeline itslef</returns>
		public GameRequestPipeline Compute(Action<PipelineContext> code) {
			if (Done)
				return this;
			try {
				code(_context);
			}
			catch (Exception ex) {
				ErrorMessage err = new ErrorMessage {Message = ex.Message, Error = ex.GetType().Name};
				Response = new ObjectResult(err) {
					StatusCode = 500
				};
			}

			return this;
		}

		public IActionResult ExecuteAction() {
			return Execute(new OkResult());
		}

		/// <summary>
		/// Ensures a result body or returns a Error Result if no body was available
		/// </summary>
		/// <returns></returns>
		public IActionResult ExecuteSecure() {
			ObjectResult result =
				new ObjectResult("The request was not processed properly and didn't produced a result");
			result.StatusCode = 500;
			return Execute(result);
		}

		private IActionResult Execute(IActionResult defaultResult) {
			if (Response == null) {
				return defaultResult;
			}

			return Response;
		}


		public GameRequestPipeline Player(int playerId) {
			if (Done)
				return this;
			_player = _game.GetPlayer(playerId);
			if (_player == null)
				if (_game.Consumers.ContainsKey(playerId))
					_player = new Player() {
						Id          = playerId,
						DisplayName = "Dummy Controller Player"
					};
				else
					Response = new NotFoundObjectResult(new ErrorMessage() {
						Error   = "Player not found",
						Message = "The id of the player is not correct or missing"
					});
			return this;
		}

		public GameRequestPipeline NextEvent(bool wait) {
			if (!Done) {
				_event = _game.EventManager.Pop(_player.Id);
				if (wait && _event == null) {
					_game.EventManager.Await();
					_event = _game.EventManager.Pop(_player.Id);
				}
			}

			return this;
		}

		public GameRequestPipeline PeekNextEvent(bool wait) {
			if (!Done) {
				_event = _game.EventManager.Peek(_player.Id);
				if (wait && _event == null) {
					_game.EventManager.Await();
					_event = _game.EventManager.Peek(_player.Id);
				}
			}

			return this;
		}

		public class PipelineContext {
			private GameRequestPipeline pipe;

			public PipelineContext(GameRequestPipeline pipe) {
				this.pipe = pipe;
			}

			public GameLogic Game   => pipe._game;
			public Player    Player => pipe._player;

			public Event     Event => pipe._event;
			public RobotInfo Robot => pipe._robot;

			public IActionResult Response {
				set => pipe.Response = value;
			}
		}

		public GameRequestPipeline Robot(int robotId) {
			if (Done)
				return this;
			Entity e = _game.Entitys[robotId];
			if (e == null)
				Response = new NotFoundObjectResult(new ErrorMessage() {
					Error   = "Robot not found",
					Message = "The id of the robot is not correct or missing"
				});
			else if (!(e is RobotInfo))
				Response = new ConflictObjectResult(new ErrorMessage() {
					Error   = "Entity is not a robot",
					Message = "The given id referes to an entity that is not a robot"
				});
			else
				_robot = (RobotInfo) e;
			return this;
		}
	}
}