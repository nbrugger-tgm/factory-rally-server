using System;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Tgm.Roborally.Test {
	class Program {
		static void Main(string[] args) {
			Configuration c       = new Configuration {BasePath = "http://localhost:5050/v1"};
			GameApi       api     = new GameApi(c);
			PlayersApi    players = new PlayersApi(c);

			TestStartGame(api,players,c);
			TestConsumer(api, players, c);
		}

		private static void TestStartGame(GameApi api, PlayersApi players, Configuration config) {
			GameRules rules = new GameRules(true, 2, "Test", 1, password: null);

			PrintHeader("Create Game");
			int game = api.CreateGame(rules);
			Print("Game ID: " + game);
			Print("Stats : "  + api.GetGameState(game));
			PrintHeader("Join Game");
			JoinResponse response = players.Join(game);
			Print($"id : {response.Id}");
			Print($"pat : {response.Pat}");
			players.Configuration.ApiKey["pat"] = response.Pat;
			api.Configuration.ApiKey["pat"]     = response.Pat;
			config.ApiKey["pat"]                = response.Pat;
			players.Join(game);

			Print(players.GetPlayer(game, response.Id).ToJson());
			PrintHeader("Start Game");
			api.CommitAction(game, ActionType.STARTGAME);
			PrintHeader("Fetch Events");
			EventHandlingApi eventApi = new EventHandlingApi(config);
			for (int i = 0; i < 8; i++) {
				Print(eventApi.FetchNextEvent(game).Type.ToString());
			}
		}
		
		private static void TestConsumer(GameApi api, PlayersApi players, Configuration config) {
			GameRules rules = new GameRules(true, 2, "Test", 1, password: null);

			PrintHeader("Create Game");
			int game = api.CreateGame(rules);
			Print("Game ID: " + game);
			Print("Stats : "  + api.GetGameState(game));
			PrintHeader("Join Game");
			players.Join(game);
			ConsumerApi consumerApi = new ConsumerApi(config);
			JoinResponse response = consumerApi.RegisterConsumer(game,new ConsumerRegistration("Test","Ein consumer"));
			Print($"id : {response.Id}");
			Print($"pat : {response.Pat}");
			players.Configuration.ApiKey["pat"] = response.Pat;
			api.Configuration.ApiKey["pat"]     = response.Pat;
			config.ApiKey["pat"]                = response.Pat;
			JoinResponse somePlayer = players.Join(game);

			Print(players.GetPlayer(game, somePlayer.Id).ToJson());
			PrintHeader("Start Game");
			api.CommitAction(game, ActionType.STARTGAME);
			PrintHeader("Fetch Events");
			EventHandlingApi eventApi = new EventHandlingApi(config);
			for (int i = 0; i < 9; i++) {
				Print(eventApi.FetchNextEvent(game).Type.ToString());
			}
		}

		private static void PrintHeader(string createGame) => Print("----------[" + createGame + "]----------");

		private static void Print(string createGame) => Console.Out.WriteLine(createGame);
	}
}