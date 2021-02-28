using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Tgm.Roborally.Server.Authentication;

namespace Tgm.Roborally.Server {
	/// <summary>
	///     Program
	/// </summary>
	public class Program {
		private const string VERSION = "2.3.0";

		/// <summary>
		///     Main
		/// </summary>
		/// <param name="args">[0] = Admin key</param>
		public static void Main(string[] args) {
			if (args.Length > 0) GameAuth.ChangeAdminKey(args[0]);
			string line = string.Concat(Enumerable.Repeat("-", VERSION.Length));
			Console.WriteLine("+---------------------" + line    + "+");
			Console.WriteLine("| Robo Rally Server v"  + VERSION + " |");
			Console.WriteLine("+---------------------" + line    + "+");
			Console.Write("map-repo : ");
			Console.WriteLine(ServerProperties.mapRepo);
			Console.Write("admin-key : ");
			Console.WriteLine(GameAuth.AdminKey);
			Console.WriteLine("github : https://github.com/FactoryRally/game-controller");
			Console.WriteLine("\n\nStart API\n");
			CreateWebHostBuilder(args).Build().Run();
			Thread.Sleep(2000);
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