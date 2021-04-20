using System;
using Microsoft.AspNetCore.Mvc;
using Tgm.Roborally.Server.Engine.Managers;
using Tgm.Roborally.Server.Models;
using Action = System.Action;

namespace Tgm.Roborally.Server.Engine {
	/**
	 * This pipeline processes a request to produce a result. More to read in the diploma thesis
	 */
	public class GameRequestPipeline {
		private readonly PipelineContext _context;
		private          RobotCommand    _command;
		private          Event           _event;

		private GameLogic _game;
		private Player    _player;
		private RobotInfo _robot;
		private Upgrade   _upgrade;

		/// <summary>
		///     Constructs an empty pipeline
		/// </summary>
		public GameRequestPipeline() {
			_context = new PipelineContext(this);
		}

		private IActionResult Response { get; set; }
		private bool          Done     => Response != null;


		/**
		 * Selects a game for further processing. If no game is found an error response is set
		 */
		public GameRequestPipeline Game(int game) {
			if (Done) return this;

			_game = GameManager.Instance.GetGame(game);
			if (_game == null) {
				Response = new NotFoundObjectResult(new ErrorMessage {
					Error   = "Game not found",
					Message = "There is no game with the given ID"
				});
			}

			return this;
		}

		/// <summary>
		///     Executes custom code to create and modify the response or handle the request.
		///     Exceptions are handled save.
		///     Use <code>executeAction</code> or <code>executeSecure</code> to send the response
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
				#if DEBUG
				Console.Error.WriteLine(ex.ToString());
				#endif
			}

			return this;
		}

		/**
		 * Executes the pipeline, produces and returns a result.
		 * Use this if the pipeline is meant to execute an action but not necessary produces a response other than 200
		 */
		public IActionResult ExecuteAction() => Execute(new OkResult());

		/// <summary>
		///     Ensures a result body or returns a Error Result if no body was available
		/// </summary>
		/// <returns></returns>
		public IActionResult ExecuteSecure() {
			ObjectResult result =
				new ObjectResult("The request was not processed properly and didn't produced a result");
			result.StatusCode = 500;
			return Execute(result);
		}

		/**
		 * Executes the pipeline and returns the produced result. If there is no result return the parameter as response ("Fallback Response")
		 */
		public IActionResult Execute(IActionResult defaultResult) => Response ?? defaultResult;


		/**
		 * Selects a player from the selected game
		 */
		public GameRequestPipeline Player(int playerId) {
			if (Done)
				return this;
			_player = _game.GetPlayer(playerId);
			if (_player == null) {
				if (_game.Consumers.ContainsKey(playerId)) {
					_player = new Player {
						Id          = playerId,
						DisplayName = "Dummy Controller Player"
					};
				}
				else {
					Response = new NotFoundObjectResult(new ErrorMessage {
						Error   = "Player not found",
						Message = "The id of the player is not correct or missing"
					});
				}
			}

			return this;
		}

		/// <summary>
		/// Fetchs and pops the next event into the pipeline. A player must be selected first
		/// </summary>
		/// <param name="wait">if true this method will wait/block until there is a new event</param>
		/// <returns>the pipeline itself</returns>
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

		/// <summary>
		/// Fetchs the next event BUT does NOT pop it. A player has to be selected first
		/// </summary>
		/// <param name="wait">if true this method will wait/block until there is a new event</param>
		/// <returns>the pipeline itself</returns>
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


		/// <summary>
		/// Selects a robot
		/// </summary>
		/// <param name="robotId">the id of the robot to select</param>
		/// <returns>the pipeline itself</returns>
		public GameRequestPipeline Robot(int robotId) {
			if (Done)
				return this;
			Entity e = _game.Entitys[robotId];
			if (e == null) {
				Response = new NotFoundObjectResult(new ErrorMessage {
					Error   = "Robot not found",
					Message = "The id of the robot is not correct or missing"
				});
			}
			else if (!(e is RobotInfo)) {
				Response = new ConflictObjectResult(new ErrorMessage {
					Error   = "Entity is not a robot",
					Message = "The given id referes to an entity that is not a robot"
				});
			}
			else
				_robot = (RobotInfo) e;

			return this;
		}

		/// <summary>
		/// Selects a programming card
		/// </summary>
		/// <param name="statementId">the id of the card</param>
		/// <returns>the pipeline</returns>
		public GameRequestPipeline ProgrammingCard(int statementId) {
			if (Done)
				return this;
			RobotCommand command = _game.Programming[statementId];
			if (command == null) {
				Response = new ConflictObjectResult(new ErrorMessage {
					Error   = "Not Found",
					Message = "No Programming card with matching ID"
				});
			}
			else
				_command = command;

			return this;
		}

		public GameRequestPipeline Upgrade(int upgradeId) {
			if (Done)
				return this;
			Upgrade command = _game.Upgrades[upgradeId];
			if (command == null) {
				Response = new ConflictObjectResult(new ErrorMessage {
					Error   = "Not Found",
					Message = "No Upgrade card with matching ID"
				});
			}
			else
				_upgrade = command;

			return this;
		}

		/// <summary>
		/// This context is used to read and manipulate the GRP. It limits access with r/w operations in an useful way
		/// </summary>
		public class PipelineContext {
			private readonly GameRequestPipeline pipe;

			public PipelineContext(GameRequestPipeline pipe) {
				this.pipe = pipe;
			}

			/// <summary>
			/// the selected game
			/// </summary>
			public GameLogic Game => pipe._game;

			/// <summary>
			/// the selected player
			/// </summary>
			public Player Player => pipe._player;

			/// <summary>
			/// the peeked/poped event
			/// </summary>
			public Event Event => pipe._event;

			/// <summary>
			/// The selected robot
			/// </summary>
			public RobotInfo Robot => pipe._robot;

			/// <summary>
			/// the selected command
			/// </summary>
			public RobotCommand Command => pipe._command;

			/// <summary>
			/// The selected Upgrade
			/// </summary>
			public Upgrade Upgrade => pipe._upgrade;

			/// <summary>
			/// The id of the selected Player
			/// </summary>
			public int PlayerID => Player.Id;

			/// <summary>
			/// Use this to  set the response of the GRP. For convenience use <code>SetResponse</code> and <code>SetNotFoundResponse</code>
			/// </summary>
			public IActionResult Response {
				set => pipe.Response = value;
			}

			/**
			 * Creates and sets an OkObjectResult
			 */
			public void SetResponse(object obj) => Response = new OkObjectResult(obj);

			/**
			 * Sends a 404 Response with an error message
			 */
			public void SetNotFoundResponse(ErrorMessage errorMessage) =>
				Response = new NotFoundObjectResult(errorMessage);
		}
		public GameRequestPipeline FailIfNull(Func<PipelineContext,object> code, ErrorMessage error) {
			if(code(_context) == null)
				_context.SetNotFoundResponse(error);
			return this;
		}
	}
}