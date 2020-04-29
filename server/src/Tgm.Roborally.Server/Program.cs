using System;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace Tgm.Roborally.Server
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Config : ");
            Console.WriteLine(ServerProperties.mapRepo);
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
                .UseUrls("http://localhost:5050/");
    }
}