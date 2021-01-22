using System;
using System.Threading;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	/**
	 * This pipeline processes a request ro produce a result
	 */
	public class GameRequestPipeline {
		private readonly PipelineContext Context;

		private GameLogic     _game;
		private IActionResult _response = null;
		private Player        _player;
		private Event         _event;

		public GameRequestPipeline() {
			Context = new PipelineContext(pipe: this);
		}

		private IActionResult response {
			get => _response;
			set { _response = value; }
		}

		private bool done => response != null;


		public GameRequestPipeline game(int game) {
			if (done) {
				return this;
			}

			_game = GameManager.instance.GetGame(game);
			if (_game == null) {
				response = new NotFoundObjectResult(new ErrorMessage() {
					Error   = "Game not found",
					Message = "There is no game with the given ID"
				});
			}

			return this;
		}

		public GameRequestPipeline compute(Action<PipelineContext> code) {
			if (done)
				return this;
			try {
				code(Context);
			}
			catch (Exception ex) {
				ErrorMessage err = new ErrorMessage {Message = ex.Message, Error = ex.GetType().Name};
				response = new ObjectResult(err) {
					StatusCode = 500
				};
			}

			return this;
		}

		public IActionResult executeAction() {
			return execute(new OkResult());
		}
		/// <summary>
		/// Ensures a result body or returns a Error Result if no body was available
		/// </summary>
		/// <returns></returns>
		public IActionResult executeSecure() {
			ObjectResult result =
				new ObjectResult("The request was not processed properly and didn't produced a result");
			result.StatusCode = 500;
			return execute(result);
		}

		private IActionResult execute(IActionResult Default) {
			if (response == null) {
				return Default;
			}

			return response;
		}


		public GameRequestPipeline player(int playerId) {
			if (done)
				return this;
			_player = _game.GetPlayer(playerId);
			if (_player == null)
				response = new NotFoundObjectResult(new ErrorMessage() {
					Error   = "Player not found",
					Message = "The id of the player is not correct or missing"
				});
			return this;
		}

		public GameRequestPipeline nextEvent(bool wait) {
			if (!done) {
				_event = _game.EventManager.Pop(_player.Id);
				if (wait && _event == null) {
					_game.EventManager.Await();
					_event = _game.EventManager.Pop(_player.Id);
				}
			}

			return this;
		}

		public GameRequestPipeline peekNextEvent(bool wait) {
			if (!done) {
				_event = _game.EventManager.Peek(_player.Id);
				if (wait && _event == null) {
					_game.EventManager.Await();
					_event = _game.EventManager.Pop(_player.Id);
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

			public Event Event => pipe._event;

			public IActionResult Response {
				set => pipe.response = value;
			}
		}
	}
}