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
			//Configuration c = new Configuration {BasePath = "localhost:5050/v1"};
			GameApi api = new GameApi();
			GameRules r = new GameRules();
			r.Name = "Test";
			r.MaxPlayers = 4;
			r.PlayerNamesVisible = true;
			
			Console.WriteLine("Create Games : ");
			
			api.CreateGame(r);
			api.CreateGame(new GameRules() {Name = "Test1", MaxPlayers = 4, PlayerNamesVisible = true});
			api.CreateGame(new GameRules() {Name = "Test2", MaxPlayers = 10, PlayerNamesVisible = true});

			List<int> games = api.GetGames();
			
			Console.WriteLine(games.Count == 3 ? "Success" : "Fail");
			
			
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
		}
	}
}