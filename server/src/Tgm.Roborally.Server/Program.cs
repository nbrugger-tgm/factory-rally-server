using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Tgm.Roborally.Server.Authentication;
using Tgm.Roborally.Server.CSExtensions;
using Tgm.Roborally.Server.Engine;
using Tgm.Roborally.Server.Engine.Abstraction;
using Tgm.Roborally.Server.Engine.Managers;
using Tgm.Roborally.Server.Mods;

namespace Tgm.Roborally.Server {
	/// <summary>
	///     Program
	/// </summary>
	public class Program {
		private const string VERSION = "2.12.5";

		/// <summary>
		///     Main
		/// </summary>
		/// <param name="args">[0] = Admin key</param>
		public static void Main(string[] args) {
			if (args.Length > 0) GameAuth.ChangeAdminKey(args[0]);

			PrintVersion();
			ReadConfig();
			PrintInfo();
			InitLogic();
			InitMods();

			StartAPI(args);
		}

		private static readonly Mod[] InternalMods = {
			new Vanilla()
			//TODO: Insert other internal mods here like optional expansions or such
		};

		private static void InitMods() {
			Console.Out.WriteLine("\n------------[MODS]------------");
			loadMods(InternalMods);
			loadExternalMods();
			GameManager.Instance.ModLoader.Mods.ForEach(e => e.Ready());
		}

		private static void loadExternalMods() => loadMods(GameManager.Instance.ModLoader.Load());

		private static void loadMods(Mod[] mods) {
			foreach (Mod mod in mods)
				try {
					GameManager.Instance.ModLoader.AddMod(mod);
				}
				catch (Exception e) {
					Console.WriteLine("[ERROR] " + e.Message);
				}
		}

		private static void StartAPI(string[] args) {
			Console.WriteLine("\n\nStart API\n");
			CreateWebHostBuilder(args).Build().Run();
			Thread.Sleep(2000);
		}

		private static void InitLogic() {
			MapManager.createInstance(ServerProperties.mapRepo);
			GameManager.Init(new DefaultImplementation());
		}

		private static void ReadConfig() {
			//TODO: READ CONFIG!
		}


		private static void PrintInfo() {
			Console.Write("map-repo : ");
			Console.WriteLine(ServerProperties.mapRepo);
			Console.Write("admin-key : ");
			Console.WriteLine(GameAuth.AdminKey);
			Console.WriteLine("github : https://github.com/FactoryRally/game-controller");
		}

		private static void PrintVersion() {
			string line = string.Concat(Enumerable.Repeat("-", VERSION.Length));
			Console.WriteLine("+--------------------" + line    + "-+");
			Console.WriteLine("| Robo Rally Server v" + VERSION + " |");
			Console.WriteLine("+--------------------" + line    + "-+");
		}

		/// <summary>
		///     Create the web host builder.
		/// </summary>
		/// <param name="args"></param>
		/// <returns>IWebHostBuilder</returns>
		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				   .UseStartup<Startup>()
				   .UseUrls(
					   "http://0.0.0.0:5050/",
					   "http://localhost:5050/");
	}
}