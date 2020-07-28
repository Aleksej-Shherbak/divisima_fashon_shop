using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    var sharedFolder = Path.Combine(env.ContentRootPath, "..", "Shared");

                    config
                        .AddJsonFile(Path.Combine(sharedFolder, $"SharedSettings.{env.EnvironmentName}.json"),
                            optional: true) // When running using dotnet run
                        .AddJsonFile($"SharedSettings.{env.EnvironmentName}.json",
                            optional: true) // When app is published
                        .AddJsonFile("./AdminSettings/appsettings.json", optional: true)
                        .AddJsonFile($"./AdminSettings/appsettings.{env.EnvironmentName}.json", optional: true);

                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((ctx, log) =>
                {
                    log.ClearProviders();
                    log.AddConsole();
                })
                .UseDefaultServiceProvider((ctx, opts) =>
                {
                    /* elided for brevity */
                })
                .UseStartup<Startup>()
                .Build();
    }
}