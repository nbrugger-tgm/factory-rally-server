using System;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Tgm.Roborally.Test {
	class Program {
		static void Main(string[] args) {
			Configuration c       = new Configuration { BasePath = "http://localhost:5050/v1" };
			GameApi       api     = new GameApi(c);
			PlayersApi    players = new PlayersApi(c); 
			GameRules     rules   = new GameRules(true, 4, "Test", 1,password:null);
			
			int game = api.CreateGame(rules);
			Console.Out.WriteLine(game);
			Console.Out.WriteLine(api.GetGameState(game));
			Console.Out.WriteLine(players.Join(gameId:game));
			JoinResponse response = players.Join(game);
			Console.Out.WriteLine($"joint response : {response}");
			Console.Out.WriteLine($"id : {response.Id}");
			Console.Out.WriteLine($"pat : {response.Pat}");
			c.ApiKey["pat"] = response.Pat;
			Console.Out.WriteLine(response);
			Console.Out.WriteLine(players.GetPlayer(game,response.Id));
		}
	}
}