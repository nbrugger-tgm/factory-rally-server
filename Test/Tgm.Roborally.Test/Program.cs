using System;
using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Tgm.Roborally.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Configuration c = new Configuration {BasePath = "http://localhost:5050/v1"};
			GameApi api = new GameApi(c);
			GameRules r = new GameRules(default,2,"Test",1,null);

			Console.WriteLine("Create Games : ");
			
			api.CreateGame(r);

			List<int> games = api.GetGames();
			
			Console.WriteLine(games.Count == 1 ? "Success" : "Fail");
			
			
			Console.WriteLine("Change Game State (Pause) : ");
			
			Console.WriteLine("  Bevore : "+api.GetGameState(games[0]).State);
			api.CommitAction(games[0],ActionType.PAUSE);
			Console.WriteLine("  After Change : "+api.GetGameState(games[0]).State);
			Console.WriteLine(api.GetGameState(games[0]).State == GameState.BREAK ? "Success" : "Fail");



			Console.WriteLine("Unpause Game ");
			Console.WriteLine("  Bevore : "+api.GetGameState(games[0]).State);
			api.CommitAction(games[0],ActionType.UNPAUSE);
			Console.WriteLine("  After Change : "+api.GetGameState(games[0]).State);
			Console.WriteLine(api.GetGameState(games[0]).State == GameState.LOBBY ? "Success" : "Fail");
			
			
			Console.WriteLine("--------[Player Join Test]---------");
			PlayersApi p = new PlayersApi(c);
			Console.WriteLine(p.Join(games[0]));
		}
	}
}