using System;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Tgm.Roborally.Server.Authentication;

namespace Tgm.Roborally.Server {
	/// <summary>
	/// Program
	/// </summary>
	public class Program {
		private const string version = "0.7.0";
		/// <summary>
		/// Main
		/// </summary> 
		/// <param name="args">[0] = Admin key</param>
		public static void Main(string[] args) {
			if (args.Length > 0) {
				GameAuth.ChangeAdminKey(args[0]);
			}

			Console.WriteLine("map-repo : ");
			Console.WriteLine(ServerProperties.mapRepo);
			Console.WriteLine("admin-key : ");
			Console.WriteLine(GameAuth.AdminKey);
			CreateWebHostBuilder(args).Build().Run();
			Thread.Sleep(2000);
		}

		/// <summary>
		/// Create the web host builder.
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