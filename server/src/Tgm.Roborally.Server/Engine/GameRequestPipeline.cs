using System;
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
		private IActionResult _response = null;

		private IActionResult response
		{
			get => _response;
			set
			{
				_response = value;
			}
		}
		private GameLogic _game;
		private bool done => response == null;
		private readonly PipelineContext Context;

		public GameRequestPipeline()
		{
			Context = new PipelineContext(pipe: this);
		}

		public void game(int game)
		{
			if (done)
			{
				return;
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
		}

		public class PipelineContext
		{
			private GameRequestPipeline pipe;

			public PipelineContext(GameRequestPipeline pipe)
			{
				this.pipe = pipe;
			}

			private GameLogic Game => pipe._game;
		}

		public void compute(IPipelineComputing code)
		{
			if (done)
				return;
			try
			{
				code.execute(Context);
			}
			catch (Exception ex)
			{
				ErrorMessage err = new ErrorMessage {Message = ex.Message, Error = ex.GetType().Name};
				response = new NotFoundObjectResult(err);
			}
		}

		public IActionResult execute()
		{
			return response;
		}
		public interface IPipelineComputing
		{
			public void execute(PipelineContext context);
		}
	}
}