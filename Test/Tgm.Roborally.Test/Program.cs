using System;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Tgm.Roborally.Test {
	class Program {
		static void Main(string[] args) {
			Configuration c = new Configuration { BasePath = "http://localhost:5050/v1" };

			GameApi   api   = new GameApi(c);
			GameRules rules = new GameRules(true, 4, "Test", 1);
			
			int game = api.CreateGame(rules);
			Console.Out.WriteLine(game);
			
		}
	}
}