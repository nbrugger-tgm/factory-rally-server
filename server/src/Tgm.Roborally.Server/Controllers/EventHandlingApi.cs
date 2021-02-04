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
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
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
	public class EventHandlingApiController : ControllerBase {
		/// <summary>
		/// Get next event
		/// </summary>
		/// <remarks>Returns the next unfetched event of the ***any*** type.</remarks>
		/// <param name="gameId"></param>
		/// <response code="200">OK</response>
		/// <response code="404">No unfetched event</response>
		[HttpGet]
		[Route("/v1/games/{game_id}/events/head")]
		[GameAuth(Role.PLAYER, allowConsumer: true)]
		[ValidateModelState]
		[SwaggerOperation("FetchNextEvent")]
		[SwaggerResponse(statusCode: 200, type: typeof(GenericEvent), description: "OK")]
		public virtual IActionResult FetchNextEvent([FromRoute(Name = "game_id")] [Required]
													int gameId) {
			return new GameRequestPipeline()
				   .Game(gameId)
				   .Player(((Player) HttpContext.Items[GameAuth.PLAYER]).Id)
				   .NextEvent(false)
				   .Compute(e => {
					   if (e.Event == null) {
						   e.Response = new NotFoundObjectResult(new ErrorMessage {
							   Error   = "Event not found",
							   Message = "There is no unfetched event"
						   });
					   }
				   })
				   .Compute(e => e.Response = new OkObjectResult(new GenericEvent(e.Event.GetEventType()) {
					   Data = e.Event
				   }))
				   .ExecuteSecure();
		}


		/// <summary>
		/// trace event
		/// </summary>
		/// <remarks>All events needed by the client are accessible here. (Usefull for animations) More about this function is found in the [regarding Github Issue](https://github.com/FactoryRally/game-controller/issues/6)  **This function only returns the type of the event you need to fetch the data seperately** &gt; Read more at [api-usage.md](https://github.com/FactoryRally/game-controller/blob/master/documentation/rest/api-usage.md#events- -updates)</remarks>
		/// <param name="gameId"></param>
		/// <param name="batch">If true you will get all past events at once</param>
		/// <param name="wait">If true the server will not responde until a event is added to the queue  Rrequires less traffic but might impacts the servers performance or cause timeouts at the client</param>
		/// <response code="200">OK</response>
		/// <response code="404">Not Found</response>
		[HttpGet]
		[Route("/v1/games/{game_id}/events/type")]
		[ValidateModelState]
		[SwaggerOperation("TraceEvent")]
		[GameAuth(Role.PLAYER, allowConsumer: true)]
		[SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "OK")]
		[SwaggerResponse(statusCode: 404, type: typeof(ErrorMessage), description: "Not Found")]
		public virtual IActionResult TraceEvent([FromRoute(Name = "game_id")] [Required] [Range(0, 2048)]
												int gameId, [FromQuery] bool batch, [FromQuery] bool wait) {
			if (wait && batch)
				return new BadRequestObjectResult(new ErrorMessage() {
					Error   = "Invalid Arguments",
					Message = "Due to logical reasons wait and batch are not allowed to both be true"
				});
			List<EventType>     events = new List<EventType>();
			GameRequestPipeline pip    = new GameRequestPipeline();
			pip.Game(gameId)
			   .Player(((Player) HttpContext.Items[GameAuth.PLAYER]).Id);
			if (batch) {
				pip.Compute(c => {
					if (c.Game.EventManager.queues.ContainsKey(c.Player.Id)) {
						events = c.Game.EventManager.queues[c.Player.Id].Select(e => e.GetEventType()).ToList();
					}
				});
			}
			else {
				pip
					.PeekNextEvent(wait)
					.Compute(e => events.Add(e.Event.GetEventType()));
			}

			return pip
				   .Compute(e => e.Response = new OkObjectResult(events))
				   .ExecuteSecure();
		}
	}
}