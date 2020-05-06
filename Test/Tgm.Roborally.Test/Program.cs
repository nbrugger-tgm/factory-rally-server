using System;
using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Tgm.Roborally.Test {
	class Program {
		static void Main(string[] args) {
			Configuration c = new Configuration { BasePath = "http://localhost:5050/v1" };

			GameApi api = new GameApi(c);
			GameRules r = new GameRules(default, 2, "Test", 1, null);

			Console.WriteLine("Create Game : " + r);

			api.CreateGame(r);

			List<int> games = api.GetGames();

			Console.WriteLine(games.Count == 1 ? "Success" : "Fail");

			Console.WriteLine("Created Game : " + games[0]);

			Console.WriteLine("Pause Lobby Phase : ");

			Console.WriteLine("  Bevore : " + api.GetGameState(games[0]).State);
			api.CommitAction(games[0], ActionType.PAUSE);
			Console.WriteLine("  After Change : " + api.GetGameState(games[0]).State);
			Console.WriteLine(api.GetGameState(games[0]).State == GameState.BREAK ? "Success" : "Fail");



			Console.WriteLine("Unpause Game ");
			Console.WriteLine("  Bevore : " + api.GetGameState(games[0]).State);
			api.CommitAction(games[0], ActionType.UNPAUSE);
			Console.WriteLine("  After Change : " + api.GetGameState(games[0]).State);
			Console.WriteLine(api.GetGameState(games[0]).State == GameState.LOBBY ? "Success" : "Fail");


			Console.WriteLine("--------[Player Join Test]---------");
			PlayersApi p = new PlayersApi(c);
			JoinResponse joined = p.Join(games[0]);
			Console.WriteLine("Join Response : " + joined);
			Console.WriteLine("Fetch Player Data : " + p.GetPlayer(gameId: games[0], joined.Id));

			JoinResponse toleave = p.Join(games[0]);
			Console.WriteLine("Join 2nd time (" + toleave + ")");
			Console.WriteLine("Players in Game : " + p.GetAllPlayers(games[0]));
			Console.WriteLine("Remove last player");
			p.KickPlayer(games[0], toleave.Id);

		}
	}
}