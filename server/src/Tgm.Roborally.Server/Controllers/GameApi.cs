/*
 * Robot Rally Game logic engine
 *
 * This api controlls the flow of a game and provides it's data. It is desiged to be RESTfull so the structure works simmilar as file system. The service will run and only work in a local network, `game.host` is the IP of the Computer hosting the game and will be found via a IP scan
 *
 * The version of the OpenAPI document: 0.1.0
 * Contact: nbrugger@student.tgm.ac.at
 * Generated by: https://openapi-generator.tech
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Tgm.Roborally.Server.Attributes;
using Tgm.Roborally.Server.Authentication;
using Tgm.Roborally.Server.Engine;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Controllers {
	/// <summary>
	/// 
	/// </summary>
	[ApiController]
	public class GameApiController : ControllerBase {
		/// <summary>
		/// Commit Action
		/// </summary>
		/// <remarks>Queues an action to be executed</remarks>
		/// <param name="gameId"></param>
		/// <param name="action"></param>
		/// <response code="200">OK</response>
		/// <response code="404">Not Found</response>
		/// <response code="409">Conflict</response>
		[HttpPut]
		[Route("/v1/games/{game_id}/actions")]
		//[GameAuth(Role.ADMIN)]
		[ValidateModelState]
		[SwaggerOperation("CommitAction")]
		[SwaggerResponse(statusCode: 404, type: typeof(ErrorMessage), description: "Not Found")]
		[SwaggerResponse(statusCode: 409, type: typeof(ErrorMessage), description: "Conflict")]
		public virtual IActionResult CommitAction([FromRoute(Name = "game_id")] [Required] [Range(0, 2048)]
												  int gameId, [FromQuery] [Required()] ActionType action) {
			return
				new GameRequestPipeline()
					.Game(gameId)
					.Compute(c => {
						c.Game.ActionHandler.Add(action);
						c.Game.ActionHandler.ExecuteNext();
					})
					.ExecuteAction();
		}

		/// <summary>
		/// Create Game
		/// </summary>
		/// <remarks>Creates a random game by your defined rules</remarks>
		/// <param name="gameRules">*Optional* This rules define how your game will behave</param>
		/// <response code="200">OK</response>
		[HttpPost]
		[Route("/v1/games/")]
		//[GameAuth(Role.ADMIN)] todo: enable wen kalian is ready
		[ValidateModelState]
		[SwaggerOperation("CreateGame")]
		public virtual IActionResult CreateGame([FromBody] GameRules gameRules) {
			GameRequestPipeline pip = new GameRequestPipeline();
			pip.Compute(c => c.Response = new OkObjectResult(GameManager.instance.startGame(gameRules)));
			return pip.ExecuteSecure();
		}

		/// <summary>
		/// Get games actions
		/// </summary>
		/// <remarks>Get all (**not robot related**) actions comitted to this game.</remarks>
		/// <param name="gameId"></param>
		/// <param name="mode">Defines wich entries to return</param>
		/// <response code="200">OK</response>
		/// <response code="201">Created</response>
		/// <response code="404">Not Found</response>
		[HttpGet]
		[Route("/v1/games/{game_id}/actions")]
		[GameAuth(Role.ADMIN)]
		[ValidateModelState]
		[SwaggerOperation("GetActions")]
		[SwaggerResponse(statusCode: 200, type: typeof(List<Action>), description: "OK")]
		[SwaggerResponse(statusCode: 404, type: typeof(ErrorMessage), description: "Not Found")]
		public virtual IActionResult GetActions([FromRoute(Name = "game_id")] [Required] [Range(0, 2048)]
												int gameId, [FromQuery] string mode) {
			return
				new GameRequestPipeline()
					.Game(gameId)
					.Compute(c => c.Response = new OkObjectResult(c.Game.ActionHandler.Queue))
					.ExecuteAction();
		}

		/// <summary>
		/// Get game status
		/// </summary>
		/// <remarks>Returns the status of a game</remarks>
		/// <param name="gameId"></param>
		/// <response code="200">OK</response>
		/// <response code="404">Not Found</response>
		[HttpGet]
		[Route("/v1/games/{game_id}/status")]
		[ValidateModelState]
		[SwaggerOperation("GetGameState")]
		[SwaggerResponse(statusCode: 200, type: typeof(GameInfo), description: "OK")]
		[SwaggerResponse(statusCode: 404, type: typeof(ErrorMessage), description: "Not Found")]
		public virtual IActionResult GetGameState(
			[FromRoute(Name = "game_id")] [Required] [Range(0, 2048)]
			int gameId
		) {
			return new GameRequestPipeline()
				   .Game(gameId)
				   .Compute(ctx => ctx.Response = new OkObjectResult(ctx.Game.Info))
				   .ExecuteAction();
		}

		/// <summary>
		/// Get all games
		/// </summary>
		/// <remarks>Returns a list of all hosted games</remarks>
		/// <param name="joinable">true: only return joinable games</param>
		/// <param name="unprotected">true: only display games with no password set</param>
		/// <response code="200">OK</response>
		[HttpGet]
		[Route("/v1/games/")]
		[ValidateModelState]
		[SwaggerOperation("GetGames")]
		[SwaggerResponse(statusCode: 200, type: typeof(List<int>), description: "OK")]
		public virtual IActionResult GetGames([FromQuery] bool joinable, [FromQuery] bool unprotected) {
			return new ObjectResult(
				GameManager.instance.games
						   .Where(pair => (!joinable    || pair.Value.Joinable) &&
										  (!unprotected || pair.Value.Password == null))
						   .Select(e => e.Key)
						   .ToList()
			);
		}

		/// <summary>
		/// Get Programming Card
		/// </summary>
		/// <remarks>Get the programming card by id</remarks>
		/// <param name="gameId"></param>
		/// <param name="statementId">The id of the programming card</param>
		/// <response code="200">OK</response>
		[HttpGet]
		[Route("/v1/games/{game_id}/statements/{statement_id}")]
		[GameAuth(Role.PLAYER)]
		[ValidateModelState]
		[SwaggerOperation("GetProgrammingCard")]
		[SwaggerResponse(statusCode: 200, type: typeof(RobotCommand), description: "OK")]
		public virtual IActionResult GetProgrammingCard([FromRoute] [Required] [Range(0, 2048)]
														int gameId, [FromRoute] [Required] [Range(0, 10000)]
														int statementId) {
			return new GameRequestPipeline()
				   .Game(gameId)
				   .ProgrammingCard(statementId)
				   .Compute(c => c.Response = new OkObjectResult(c.Command))
				   .ExecuteSecure();
		}

		/// <summary>
		/// Get Programming Card IDs
		/// </summary>
		/// <param name="gameId"></param>
		/// <response code="200">OK</response>
		[HttpHead]
		[Route("/v1/games/{game_id}/statements")]
		[GameAuth(Role.PLAYER)]
		[ValidateModelState]
		[SwaggerOperation("GetProgrammingCardIds")]
		[SwaggerResponse(statusCode: 200, type: typeof(List<int>), description: "OK")]
		public virtual IActionResult GetProgrammingCardIds([FromRoute] [Required] [Range(0, 2048)]
														   int gameId) {
			return new GameRequestPipeline()
				   .Game(gameId)
				   .Compute(c => c.Response = new OkObjectResult(c.Game.Programming.IDs))
				   .ExecuteSecure();
		}

		/// <summary>
		/// Get Programming cards
		/// </summary>
		/// <remarks>Returns the Programming cards in this game</remarks>
		/// <param name="gameId"></param>
		/// <response code="200">OK</response>
		[HttpGet]
		[Route("/v1/games/{game_id}/statements")]
		[GameAuth(Role.PLAYER)]
		[ValidateModelState]
		[SwaggerOperation("GetProgrammingCards")]
		[SwaggerResponse(statusCode: 200, type: typeof(List<RobotCommand>), description: "OK")]
		public virtual IActionResult GetProgrammingCards([FromRoute] [Required] [Range(0, 2048)]
														 int gameId) {
			return new GameRequestPipeline()
				   .Game(gameId)
				   .Compute(c => c.Response = new OkObjectResult(c.Game.Programming.Cards))
				   .ExecuteSecure();
		}
	}
}