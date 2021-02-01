/*
 * Robot Rally Game logic engine
 *
 * This api controlls the flow of a game and provides it's data. It is desiged to be RESTfull so the structure works simmilar as file system. The service will run and only work in a local network, `game.host` is the IP of the Computer hosting the game and will be found via a IP scan
 *
 * The version of the OpenAPI document: 0.1.0
 * Contact: nbrugger@student.tgm.ac.at
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Tgm.Roborally.Server.Attributes;
using Tgm.Roborally.Server.Engine;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Controllers {
	/// <summary>
	/// 
	/// </summary>
	[ApiController]
	public class PlayersApiController : ControllerBase {
		/// <summary>
		/// Set Robots
		/// </summary>
		/// <remarks># DEPRECATET &gt; This feature is useless in this version. It will be usefull in newer versions  Sets the type of robot(s) the player is controlling</remarks>
		/// <param name="gameId"></param>
		/// <param name="playerId"></param>
		/// <param name="robots">The robots assigned to the player</param>
		/// <response code="200">OK</response>
		/// <response code="404">Not Found</response>
		[HttpPatch]
		[Route("/v1/games/{game_id}/players/{player_id}")]
		[ValidateModelState]
		[SwaggerOperation("ChooseRobot")]
		[SwaggerResponse(statusCode: 404, type: typeof(ErrorMessage), description: "Not Found")]
		public virtual IActionResult ChooseRobot([FromRoute(Name = "game_id")] [Required] [Range(0, 2048)]
												 int gameId,   [FromRoute(Name = "player_id")] [Required] [Range(0, 8)]
												 int playerId, [FromBody] List<Robots> robots) {
			throw new NotImplementedException("Not implemented in this version");
		}

		/// <summary>
		/// Get all players
		/// </summary>
		/// <remarks>Returns the index of all players</remarks>
		/// <param name="gameId"></param>
		/// <response code="200">OK</response>
		/// <response code="404">Not Found</response>
		[HttpGet]
		[Route("/v1/games/{game_id}/players/")]
		[ValidateModelState]
		[SwaggerOperation("GetAllPlayers")]
		[SwaggerResponse(statusCode: 200, type: typeof(List<int>), description: "OK")]
		[SwaggerResponse(statusCode: 404, type: typeof(ErrorMessage), description: "Not Found")]
		public virtual IActionResult GetAllPlayers([FromRoute(Name = "game_id")] [Required] [Range(0, 2048)]
												   int gameId) {
			return new GameRequestPipeline()
				   .game(gameId)
				   .compute(c => c.Response = new ObjectResult(c.Game.PlayerIds))
				   .executeAction();
		}

		/// <summary>
		/// Get player
		/// </summary>
		/// <remarks>Get closer information about the player</remarks>
		/// <param name="gameId"></param>
		/// <param name="playerId"></param>
		/// <response code="200">OK</response>
		/// <response code="404">Not Found</response>
		[HttpGet]
		[Route("/v1/games/{game_id}/players/{player_id}")]
		[ValidateModelState]
		[SwaggerOperation("GetPlayer")]
		[SwaggerResponse(statusCode: 200, type: typeof(Player), description: "OK")]
		[SwaggerResponse(statusCode: 404, type: typeof(ErrorMessage), description: "Not Found")]
		public virtual IActionResult GetPlayer([FromRoute(Name = "game_id")] [Required] [Range(0, 2048)]
											   int gameId, [FromRoute(Name = "player_id")] [Required] [Range(0, 8)]
											   int playerId) {
			return new GameRequestPipeline()
				   .game(gameId)
				   .player(playerId)
				   .compute(c => c.Response = new ObjectResult(c.Player))
				   .executeAction();
		}

		/// <summary>
		/// Join game
		/// </summary>
		/// <remarks>Join the given game. You will get your ID by doing this, if you already in the game you can get your ID again if you lost it.&lt;br&gt; The id is neccessary for any further API calls</remarks>
		/// <param name="gameId"></param>
		/// <param name="password">The password of the game if the lobby is password protected</param>
		/// <param name="name">The name to be displayed as username</param>
		/// <response code="200">Joined</response>
		/// <response code="401">Wrong/No password</response>
		/// <response code="404">Not Found</response>
		/// <response code="409">Not Joinable</response>
		[HttpPost]
		[Route("/v1/games/{game_id}/players/")]
		[ValidateModelState]
		[SwaggerOperation("Join")]
		[SwaggerResponse(statusCode: 200, type: typeof(JoinResponse), description: "Joined")]
		[SwaggerResponse(statusCode: 401, type: typeof(ErrorMessage), description: "Wrong/No password")]
		[SwaggerResponse(statusCode: 404, type: typeof(ErrorMessage), description: "Not Found")]
		[SwaggerResponse(statusCode: 409, type: typeof(ErrorMessage), description: "Not Joinable")]
		public virtual IActionResult Join([FromRoute(Name = "game_id")] [Required] [Range(0, 2048)]
										  int gameId, [FromQuery] string password,
										  [FromQuery]
										  [RegularExpression("[A-Za-z0-9_-]+")]
										  [StringLength(30, MinimumLength = 3)]
										  string name) {
			return new GameRequestPipeline()
				   .game(gameId)
				   .compute(c => c.Response = new ObjectResult(new JoinResponse(c.Game.Join(password, name))))
				   .executeAction();
		}

		/// <summary>
		/// Remove Player
		/// </summary>
		/// <remarks>Removes a player from the game. This can be done by the player itsself or by the host.</remarks>
		/// <param name="gameId"></param>
		/// <param name="playerId"></param>
		/// <response code="200">OK</response>
		/// <response code="404">Not Found</response>
		[HttpDelete]
		[Route("/v1/games/{game_id}/players/{player_id}")]
		[ValidateModelState]
		[SwaggerOperation("KickPlayer")]
		[SwaggerResponse(statusCode: 404, type: typeof(ErrorMessage), description: "Not Found")]
		public virtual IActionResult KickPlayer([FromRoute(Name = "game_id")] [Required] [Range(0, 2048)]
												int gameId, [FromRoute(Name = "player_id")] [Required] [Range(0, 8)]
												int playerId) {
			return new GameRequestPipeline().game(gameId).compute(context => context.Game.RemovePlayer(playerId))
											.executeAction();
		}
	}
}