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

namespace Tgm.Roborally.Server.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class EventHandlingApiController : ControllerBase
    { 
        /// <summary>
        /// Get next / last damage event
        /// </summary>
        /// <remarks>Returns the next unfetched event of the damage type.  If the event is not of the damage type you will get a &#x60;400&#x60; status and the event stays unfetched</remarks>
        /// <param name="gameId"></param>
        /// <response code="200">OK</response>
        /// <response code="404">No unfetched event</response>
        /// <response code="417">The next event is not a movement event</response>
        [HttpGet]
        [Route("/v1/games/{game_id}/events/damage")]
        [ValidateModelState]
        [SwaggerOperation("FetchNextDamageEvent")]
        [SwaggerResponse(statusCode: 200, type: typeof(DamageEvent), description: "OK")]
        public virtual IActionResult FetchNextDamageEvent([FromRoute(Name = "game_id")][Required][Range(0, 2048)]int gameId)
        {
            return typifiedEvent<DamageEvent>(gameId, false);
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(DamageEvent));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            //TODO: Uncomment the next line to return response 417 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(417);
        }
        
        private IActionResult typifiedEvent<T>(in int gameId, in bool wait) where T:Event{
            return new GameRequestPipeline()
                   .game(gameId)
                   .player(((Player) HttpContext.Items[GameAuth.PLAYER]).Id)
                   .nextEvent(wait)
                   .compute(e => {
                       if (e.Event == null) {
                           e.Response = new NotFoundObjectResult(new ErrorMessage {
                               Error = "Event not found",
                               Message = "There is no unfetched event"
                           }) ;
                       }
                   })
                   .compute(code: e => {
                       if (e.Event.GetType() != typeof(T)) {
                           Type actualType = e.Event.GetType();
                           e.Response = new ObjectResult(new ErrorMessage {
                               Error = "Event type error",
                               Message = "The expected Event was " + typeof(T) + " but the actual event was " +
                                         actualType
                           }) {
                               StatusCode = 417
                           };
                       }
                   })
                   .compute(e => new ObjectResult(e.Event))
                   .executeAction();
        }

        /// <summary>
        /// Get next event
        /// </summary>
        /// <remarks>Returns the next unfetched event of the ***any*** type.</remarks>
        /// <param name="gameId"></param>
        /// <response code="200">OK</response>
        /// <response code="404">No unfetched event</response>
        [HttpGet]
        [Route("/v1/games/{game_id}/events/head")]
        [GameAuth(Role.PLAYER,allowConsumer:true)]
        [ValidateModelState]
        [SwaggerOperation("FetchNextEvent")]
        [SwaggerResponse(statusCode: 200, type: typeof(GenericEvent), description: "OK")]
        public virtual IActionResult FetchNextEvent([FromRoute(Name = "game_id")] [Required] int gameId) {
            return new GameRequestPipeline()
                   .game(gameId)
                   .player(((Player) HttpContext.Items[GameAuth.PLAYER]).Id)
                   .nextEvent(false)
                   .compute(e => {
                       if (e.Event == null) {
                           e.Response = new NotFoundObjectResult(new ErrorMessage {
                               Error   = "Event not found",
                               Message = "There is no unfetched event"
                           }) ;
                       }
                   })
                   .compute(e => e.Response = new OkObjectResult(new GenericEvent(e.Event.GetEventType()) {
                       Data = e.Event
                   }))
                   .executeSecure();
        }

        /// <summary>
        /// Get next / last Lazer hit event
        /// </summary>
        /// <remarks>Returns the next unfetched event of the lazer hit type.  If the event is not of the lazer hit type you will get a &#x60;400&#x60; status and the event stays unfetched</remarks>
        /// <param name="gameId"></param>
        /// <response code="200">OK</response>
        /// <response code="404">No unfetched event</response>
        /// <response code="417">The next event is not a movement event</response>
        [HttpGet]
        [Route("/v1/games/{game_id}/events/lazer-hit")]
        [ValidateModelState]
        [SwaggerOperation("FetchNextLazerHitEvent")]
        [SwaggerResponse(statusCode: 200, type: typeof(LazerHitEvent), description: "OK")]
        public virtual IActionResult FetchNextLazerHitEvent([FromRoute(Name = "game_id")][Required][Range(0, 2048)]int gameId)
        { 

            return typifiedEvent<LazerHitEvent>(gameId, false);
        }

        /// <summary>
        /// Get next / last map event
        /// </summary>
        /// <remarks>Returns the next unfetched event of the  Map Event type. Map Events activeata all active components of a type at once  If the event is not of the map event type you will get a &#x60;400&#x60; status and the event stays unfetched</remarks>
        /// <param name="gameId"></param>
        /// <response code="200">OK</response>
        /// <response code="404">No unfetched event</response>
        /// <response code="417">The next event is not a movement event</response>
        [HttpGet]
        [Route("/v1/games/{game_id}/events/map")]
        [ValidateModelState]
        [SwaggerOperation("FetchNextMapEvent")]
        [SwaggerResponse(statusCode: 200, type: typeof(MapEvent), description: "OK")]
        public virtual IActionResult FetchNextMapEvent([FromRoute(Name = "game_id")][Required][Range(0, 2048)]int gameId)
        { 

            return typifiedEvent<MapEvent>(gameId, false);
        }

        /// <summary>
        /// Get next / last movement event
        /// </summary>
        /// <remarks>Returns the next unfetched event of the movement type.  If the event is not of the movement type you will get a &#x60;400&#x60; status and the event stays unfetched</remarks>
        /// <param name="gameId"></param>
        /// <response code="200">OK</response>
        /// <response code="404">No unfetched event</response>
        /// <response code="417">The next event is not a movement event</response>
        [HttpGet]
        [Route("/v1/games/{game_id}/events/movement")]
        [ValidateModelState]
        [SwaggerOperation("FetchNextMovementEvent")]
        [SwaggerResponse(statusCode: 200, type: typeof(MovementEvent), description: "OK")]
        public virtual IActionResult FetchNextMovementEvent([FromRoute(Name = "game_id")][Required][Range(0, 2048)]int gameId)
        { 

            return typifiedEvent<MovementEvent>(gameId, false);
        }

       
        /// <summary>
        /// Get next / last push event
        /// </summary>
        /// <remarks>Returns the next unfetched event of the push type.  If the event is not of the push  type you will get a &#x60;400&#x60; status and the event stays unfetched</remarks>
        /// <param name="gameId"></param>
        /// <response code="200">OK</response>
        /// <response code="404">No unfetched event</response>
        /// <response code="417">The next event is not a movement event</response>
        [HttpGet]
        [Route("/v1/games/{game_id}/events/push")]
        [ValidateModelState]
        [SwaggerOperation("FetchNextPushEvent")]
        [SwaggerResponse(statusCode: 200, type: typeof(PushEvent), description: "OK")]
        public virtual IActionResult FetchNextPushEvent([FromRoute(Name = "game_id")][Required][Range(0, 2048)]int gameId)
        { 

            
            return typifiedEvent<PushEvent>(gameId, false);
        }

        /// <summary>
        /// Get next / last shoot event
        /// </summary>
        /// <remarks>Returns the next unfetched event of the movement type.  If the event is not of the movement type you will get a &#x60;400&#x60; status and the event stays unfetched</remarks>
        /// <param name="gameId"></param>
        /// <response code="200">OK</response>
        /// <response code="404">No unfetched event</response>
        /// <response code="417">The next event is not a movement event</response>
        [HttpGet]
        [Route("/v1/games/{game_id}/events/shoot")]
        [ValidateModelState]
        [SwaggerOperation("FetchNextShootEvent")]
        [SwaggerResponse(statusCode: 200, type: typeof(ShootEvent), description: "OK")]
        public virtual IActionResult FetchNextShootEvent([FromRoute(Name = "game_id")][Required][Range(0, 2048)]int gameId)
        { 

           
            return typifiedEvent<ShootEvent>(gameId, false);
        }

        /// <summary>
        /// Get next / last shutdown event
        /// </summary>
        /// <remarks>Returns the next unfetched event of the movement type.  If the event is not of the movement type you will get a &#x60;400&#x60; status and the event stays unfetched</remarks>
        /// <param name="gameId"></param>
        /// <response code="200">OK</response>
        /// <response code="404">No unfetched event</response>
        /// <response code="417">The next event is not a movement event</response>
        [HttpGet]
        [Route("/v1/games/{game_id}/events/shutdown")]
        [ValidateModelState]
        [SwaggerOperation("FetchNextShutdownEvent")]
        [SwaggerResponse(statusCode: 200, type: typeof(ShutdownEvent), description: "OK")]
        public virtual IActionResult FetchNextShutdownEvent([FromRoute(Name = "game_id")][Required][Range(0, 2048)]int gameId)
        {
            return typifiedEvent<ShutdownEvent>(gameId, false);
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
        [GameAuth(Role.PLAYER,allowConsumer:true)]
        [SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "OK")]
        [SwaggerResponse(statusCode: 404, type: typeof(ErrorMessage), description: "Not Found")]
        public virtual IActionResult TraceEvent([FromRoute(Name = "game_id")][Required][Range(0, 2048)]int gameId, [FromQuery]bool batch, [FromQuery]bool wait) {
            if(wait && batch)
                return new BadRequestObjectResult(new ErrorMessage() {
                    Error = "Invalid Arguments",
                    Message = "Due to logical reasons wait and batch are not allowed to both be true"
                });
            List<EventType>    events = new List<EventType>();
            GameRequestPipeline pip    = new GameRequestPipeline();
            pip.game(gameId)
               .player(((Player) HttpContext.Items[GameAuth.PLAYER]).Id);
            if (batch) {
                pip.compute(c => {
                    if (c.Game.EventManager.queues.ContainsKey(c.Player.Id)) {
                        events = c.Game.EventManager.queues[c.Player.Id].Select(e => e.GetEventType()).ToList();
                    }
                });
            }
            else {
                pip
                    .peekNextEvent(wait)
                    .compute(e => events.Add(e.Event.GetEventType()));
            }

            return pip
                   .compute(e => e.Response = new OkObjectResult(events))
                   .executeSecure();
        }
    }
}
