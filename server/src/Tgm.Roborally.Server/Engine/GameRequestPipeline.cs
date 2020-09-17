using System;
using System.Threading;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine
{
	/**
	 * This pipeline processes a request ro produce a result
	 */
	public class GameRequestPipeline
	{
		private readonly PipelineContext Context;

		private GameLogic _game;
		private IActionResult _response = null;

		public GameRequestPipeline()
		{
			Context = new PipelineContext(pipe: this);
		}

		private IActionResult response
		{
			get => _response;
			set { _response = value; }
		}

		private bool done => response != null;

		public GameRequestPipeline game(int game)
		{
			if (done)
			{
				return this;
			}

			_game = GameManager.instance.GetGame(game);
			if (_game == null)
			{
				response = new NotFoundObjectResult(new ErrorMessage()
				{
					Error = "Game not found",
					Message = "There is no game with the given ID"
				});
			}

			return this;
		}

		public GameRequestPipeline compute(Action<PipelineContext> code)
		{
			if (done)
				return this;
			try
			{
				code(Context);
			}
			catch (Exception ex)
			{
				ErrorMessage err = new ErrorMessage {Message = ex.Message, Error = ex.GetType().Name};
				response = new NotFoundObjectResult(err);
			}

			return this;
		}

		public IActionResult execute()
		{
			if (response == null)
				return new OkResult();
			return response;
		}

		public class PipelineContext
		{
			private GameRequestPipeline pipe;

			public PipelineContext(GameRequestPipeline pipe)
			{
				this.pipe = pipe;
			}

			public GameLogic Game => pipe._game;
			public Player Player => pipe.Player;

			public IActionResult Response
			{
				set => pipe.response = value;
			}
		}


		public GameRequestPipeline player(int playerId)
		{
			if (done)
				return this;
			Player = _game.GetPlayer(playerId);
			if(Player == null)
				response = new NotFoundObjectResult(new ErrorMessage()
				{
					Error = "Player not found",
					Message = "The id of the player is not correct"
				});
			return this;
		}

		public Player Player { get; private set; }
	}
}