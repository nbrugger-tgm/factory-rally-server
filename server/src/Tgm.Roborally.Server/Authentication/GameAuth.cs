using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tgm.Roborally.Server.Engine;
using Tgm.Roborally.Server.Engine.Managers;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Authentication {
	/// <summary>
	/// An attribute to add authentication to an Endpoint
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class GameAuth : Attribute, IAuthorizationFilter {
		public const string
			PLAYER_OWNS_RESOURCE = "PLAYER_ID_OWNS_RESOURCE",
			PLAYER_CALLS_HIMSELF = "PLAYER_SELF_CALL",
			IS_PLAYER            = "IS_PLAYER",
			IS_ADMIN             = "IS_ADMIN",
			PLAYER               = "PLAYER",
			PLAYER_ID            = "PLAYER_ID",
			IS_CONSUMER          = "IS_CONSUMER";

		private readonly bool _allowConsumer;

		/**
		 * If true the accessed object (e.g Robot) needs to be owned by the player
		 */
		private readonly bool _belongsTo;

		/**
		 * this is the name of the gameID field in the path, defaults to `game_id`
		 */
		private readonly string _gameIdPathName;

		/**
		 * This is the role that is needed to pass the filter
		 */
		private readonly Role _needed_role;

		private readonly OwnershipEnsurance _ownershipEnsurance;

		/**
		 * Defines the name of the Authentication ID in the querry (default `player_id`)
		 */
		private readonly string _playerIdPathName;

		/**
		 * When true the player must match `player_id`(customizeable) from path
		 */
		private readonly bool _playerSelf;

		/// <param name="neededRole">This is the role that is needed to pass the filter</param>
		/// <param name="gameIdPathName">this is the name of the gameID field in the path</param>
		/// <param name="playerSelf">When true the player must match `player_id`(customizeable) from path</param>
		/// <param name="playerIdPathName">Defines the name of the Authentication ID in the querry (default `player_id`)</param>
		/// <param name="allowConsumer">True if this action can be executed by consumers</param>
		public GameAuth(Role   neededRole,                 bool   playerSelf       = false, bool allowConsumer = false,
						string gameIdPathName = "game_id", string playerIdPathName = "player_id") {
			_needed_role      = neededRole;
			_gameIdPathName   = gameIdPathName;
			_playerSelf       = playerSelf;
			_playerIdPathName = playerIdPathName;
			_allowConsumer    = allowConsumer;
		}


		/// <inheritdoc />
		public GameAuth(
			Type   ownershipEnsurance,
			bool   playerSelf       = false,
			string playerIdPathName = "player_id",
			string gameIdPathName   = "game_id"
		) : this((OwnershipEnsurance) Activator.CreateInstance(ownershipEnsurance), playerSelf, playerIdPathName,
				 gameIdPathName) {
		}

		/// <param name="ownershipEnsurance">A method to determine if the accessed object is owned by the player</param>
		/// <param name="gameIdPathName">this is the name of the gameID field in the path</param>
		/// <param name="playerSelf">When true the player must match `player_id`(customizeable) from path</param>
		/// <param name="playerIdPathName">Defines the name of the Authentication ID in the querry (default `player_id`)</param>
		/// <param name="belongsTo">Only allows the player to pass if he owns the resource</param>
		public GameAuth(
			OwnershipEnsurance ownershipEnsurance,
			bool               playerSelf       = false,
			string             playerIdPathName = "player_id",
			string             gameIdPathName   = "game_id"
		) {
			_needed_role        = Role.PLAYER;
			_gameIdPathName     = gameIdPathName;
			_belongsTo          = true;
			_playerSelf         = playerSelf;
			_playerIdPathName   = playerIdPathName;
			_ownershipEnsurance = ownershipEnsurance;
		}

		/**
		 * The key to get admin access, initially randomly generated
		 */
		public static string AdminKey { get; private set; } = RandomString(256, false);


		/**
		 * Verifies the identity of a Request.
		 * results are stored in HttpContext.Items
		 */
		public void OnAuthorization(AuthorizationFilterContext context) {
			bool        isAdmin    = false;
			bool        isPlayer   = false;
			bool        isSelf     = false;
			bool        owns       = false;
			bool        isConsumer = false;
			Player      player     = null;
			HttpRequest request    = context.HttpContext.Request;


			if (_needed_role == Role.ADMIN || _needed_role == Role.ANYONE)
				isAdmin = VerifyAdmin(request);

			VerifyPlayer(request, ref player, ref isPlayer, ref isConsumer, ref isSelf, ref owns);

			#region processResult

			StoreInContext(context, isPlayer, isAdmin, owns, isSelf, player, isConsumer);

			SendResponse(context, isPlayer, isAdmin, isSelf, owns, isConsumer);

			#endregion
		}

		private static void StoreInContext(ActionContext context, bool   isPlayer, bool isAdmin, bool owns,
										   bool          isSelf,  Player player,   bool isConsumer) {
			context.HttpContext.Items[IS_PLAYER]            = isPlayer;
			context.HttpContext.Items[IS_ADMIN]             = isAdmin;
			context.HttpContext.Items[PLAYER_OWNS_RESOURCE] = owns;
			context.HttpContext.Items[PLAYER_CALLS_HIMSELF] = isSelf;
			context.HttpContext.Items[PLAYER]               = player;
			context.HttpContext.Items[PLAYER_ID]            = player == null ? -1 : player.Id;
			context.HttpContext.Items[IS_CONSUMER]          = isConsumer;
		}

		private void SendResponse(AuthorizationFilterContext context, bool isPlayer, bool isAdmin, bool isSelf,
								  bool                       owns,
								  bool                       isConsumer) {
			if (_needed_role == Role.ANYONE) {
				if (!(isPlayer || isAdmin)) {
					context.Result = new UnauthorizedObjectResult(new ErrorMessage {
						Error = "Authentication failed",
						Message =
							"You need to authenticate to access this resource. (As anyone, so you might be a player OR an admin)"
					});
				}
			}

			if (_needed_role == Role.ADMIN) {
				if (!isAdmin) {
					context.Result = new UnauthorizedObjectResult(new ErrorMessage {
						Error = "Authentication failed",
						Message = "You need admin access for this resource!" +
								  (isPlayer ? " (PLAYER privilege isn't sufficient)" : "")
					});
				}
			}

			if (_needed_role == Role.PLAYER) {
				if (isPlayer) {
					if (_playerSelf && !isSelf) {
						context.Result = new UnauthorizedObjectResult(new ErrorMessage {
							Error   = "Authentication failed",
							Message = "You are not allowed to access this resource, it does belong to another player"
						});
					}
					else if (_belongsTo && !owns) {
						context.Result = new UnauthorizedObjectResult(new ErrorMessage {
							Error   = "Authentication failed",
							Message = "You do not own this entity, you only can do this with your own resources"
						});
					}
				}
				else {
					context.Result = new UnauthorizedObjectResult(new ErrorMessage {
						Error   = "Authentication failed",
						Message = "You need player privileges to access this resource"
					});
				}
			}

			if (!_allowConsumer && isConsumer) {
				context.Result = new UnauthorizedObjectResult(new ErrorMessage {
					Error   = "Authentication failed",
					Message = "This action can only be used by real players. Consumers do not work here"
				});
			}
		}

		private void VerifyPlayer(HttpRequest request, ref Player player, ref bool isPlayer, ref bool isConsumer,
								  ref bool    isSelf,  ref bool   owns) {
			if (_needed_role == Role.PLAYER || _needed_role == Role.ANYONE) {
				GameLogic game    = DetectGame(request);
				string    authKey = request.Query["pat"].ToString();

				if (game != null) {
					player   = game.AuthPlayer(authKey);
					isPlayer = player != null;
					(bool, int) res = game.IsConsumer(authKey);
					if (!isPlayer && res.Item1) {
						isPlayer   = true;
						isConsumer = true;
						player = new Player {
							Id          = res.Item2,
							DisplayName = game.GetConsumer(res.Item2).Name,
							Active      = false,
							OnTurn      = false
						};
					}
					else if (player != null) {
						if (_playerSelf) {
							isSelf =
								Convert.ToInt32(request.RouteValues[_playerIdPathName].ToString()) ==
								player.Id;
						}

						if (_belongsTo)
							owns = _ownershipEnsurance.DoesOwn(player.Id, request.RouteValues, game);
					}
				}
			}
		}

		private GameLogic DetectGame(HttpRequest request) =>
			GameManager.Instance.GetGame(Convert.ToInt32(request.RouteValues[_gameIdPathName].ToString()));

		/**
		 * Verifies if the request is submitted by the admin of the server
		 */
		private bool VerifyAdmin(HttpRequest request) => request.Query["skey"].Equals(AdminKey);

		/**
		 * Changes the key tha admin needs to access the server
		 */
		public static void ChangeAdminKey(string key) => AdminKey = key;


		private static string RandomString(int size, bool lowerCase) {
			StringBuilder builder = new StringBuilder();
			Random        random  = new Random();
			char          ch;
			for (int i = 0; i < size; i++) {
				ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
				builder.Append(ch);
			}

			return lowerCase ? builder.ToString().ToLower() : builder.ToString();
		}
	}
}