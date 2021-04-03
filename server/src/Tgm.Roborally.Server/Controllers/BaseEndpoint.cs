using System;
using Microsoft.AspNetCore.Mvc;
using Tgm.Roborally.Server.Authentication;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Controllers {
	public static class BaseEndpoint {
		public static Player GetPlayer(this ControllerBase str) {
			return str.GetItem<Player>(GameAuth.PLAYER);
		}

		public static int GetPlayerID(this ControllerBase str) {
			return str.GetItem<int>(GameAuth.PLAYER_ID);
		}

		private static T GetItem<T>(this ControllerBase context, string name) {
			return (T) context.HttpContext.Items[name];
		}
	}
}