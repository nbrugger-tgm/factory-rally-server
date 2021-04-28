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
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Tgm.Roborally.Server.Attributes;
using Tgm.Roborally.Server.Authentication;
using Tgm.Roborally.Server.Engine;
using Tgm.Roborally.Server.Engine.Managers;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Controllers {
	/// <summary>
	/// </summary>
	[ApiController]
	public class MapRepoApiController : ControllerBase {
		private MapManager Manager => MapManager.Instance;
		/// <summary>
		///     Delete Map
		/// </summary>
		/// <remarks>Delete a map by its name</remarks>
		/// <param name="mapName"></param>
		/// <response code="204">Deleted</response>
		/// <response code="404">Not Found</response>
		/// <response code="500">Internal Server Error</response>
		[HttpDelete]
		[Route("/v1/maps/{map_name}")]
		[GameAuth(Role.ADMIN)]
		[ValidateModelState]
		[SwaggerOperation("DeleteMap")]
		[SwaggerResponse(404, type: typeof(ErrorMessage), description: "Not Found")]
		[SwaggerResponse(500, type: typeof(ErrorMessage), description: "Internal Server Error")]
		public virtual IActionResult DeleteMap([FromRoute(Name = "map_name")] [Required]
											   string mapName) {
			try {
				if (Manager.Get(mapName) == null)
					return new NotFoundObjectResult("The requested map does not exists");
				MapManager.Instance.RemoveMap(mapName);
				return new OkResult();
			}
			catch (IOException e) {
				return HandleIOException(e);
			}
		}

		private static IActionResult HandleIOException(IOException e) {
			Console.WriteLine(e);
			return new ObjectResult($"The map repo is not accessible. ({e.GetType().Name} : {e.Message}");
		}

		/// <summary>
		///     Get map
		/// </summary>
		/// <remarks>Get a map by its name</remarks>
		/// <param name="mapName"></param>
		/// <response code="200">OK</response>
		/// <response code="404">Not Found</response>
		/// <response code="500">Internal Server Error</response>
		[HttpGet]
		[Route("/v1/maps/{map_name}")]
		[GameAuth(Role.ADMIN)]
		[ValidateModelState]
		[SwaggerOperation("GetMap")]
		[SwaggerResponse(200, type: typeof(MapInfo), description: "OK")]
		[SwaggerResponse(404, type: typeof(ErrorMessage), description: "Not Found")]
		[SwaggerResponse(500, type: typeof(ErrorMessage), description: "Internal Server Error")]
		public virtual IActionResult GetMap([FromRoute(Name = "map_name")] [Required]
											string mapName) {
			try {
				Map m = Manager.Get(mapName);
				if (m == null)
					return new NotFoundResult();

				return new OkObjectResult(m);
			}
			catch (IOException e) {
				return HandleIOException(e);
			}
		}

		/// <summary>
		///     Get Map Names
		/// </summary>
		/// <remarks>Returns a list of all map names</remarks>
		/// <response code="200">OK</response>
		[HttpGet]
		[Route("/v1/maps/")]
		[GameAuth(Role.ADMIN)]
		[ValidateModelState]
		[SwaggerOperation("GetMaps")]
		[SwaggerResponse(200, type: typeof(List<string>), description: "OK")]
		public virtual IActionResult GetMaps() {
			try {
				return new OkObjectResult(Manager.Names);
			}
			catch (IOException e) {
				return HandleIOException(e);
			}
		}

		/// <summary>
		///     Save Map
		/// </summary>
		/// <remarks>Saves a map to the repository</remarks>
		/// <param name="map">The map to save</param>
		/// <response code="200">OK</response>
		[HttpPost]
		[Route("/v1/maps/")]
		[GameAuth(Role.ADMIN)]
		[ValidateModelState]
		[SwaggerOperation("SaveMap")]
		public virtual IActionResult SaveMap([FromBody] Map map,[FromQuery]string name) {
			try {
				Manager.Add(map,name);
				return new OkResult();
			}
			catch (IOException e) {
				return HandleIOException(e);
			}
		}
	}
}