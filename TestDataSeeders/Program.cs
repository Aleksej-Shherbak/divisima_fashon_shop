using System;
using System.Collections.Generic;
using System.IO;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestDataSeeders.Seeders;

namespace TestDataSeeders
{
    class Program
    {
        static void Main(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrEmpty(envName))
            {
                throw new
                    InvalidOperationException(
                        "Environment variable is empty! Please, " +
                        "check the ASPNETCORE_ENVIRONMENT environment variable");
            }

            var settingPath = $"./SharedSettings.{envName}.json";

            IConfigurationRoot configuration = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile( $"appsettings.{envName}.json")
                .AddJsonFile(settingPath, false)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DivisimaDb");
            builder.UseNpgsql(connectionString, opt =>
            {
                opt.MigrationsAssembly("EntityFramework");
                opt.SetPostgresVersion(10, 10);
            });

            using var context = new ApplicationDbContext(builder.Options);
            context.Database.EnsureCreated();

            Console.WriteLine("What do you want?");
            Console.WriteLine("Press 1 to run seeding.");
            Console.WriteLine("Press 2 to clear database.");
            Console.WriteLine("Press 3 to clear database and run seeding.");
            Console.WriteLine("Press 4 for exit.");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    RunAllSeeders(context);
                    Console.WriteLine("ALL SEEDERS WERE APPLIED!!!");
                    break;
                case "2":
                    ClearUpDb(context);
                    Console.WriteLine("YOU DATABASE WAS RECREATED!!!");
                    break;
                case "3":
                    ClearUpDb(context);
                    RunAllSeeders(context);
                    Console.WriteLine("DATABASE WAS RECREATED AND ALL SEEDERS WERE APPLIED!!!");
                    break;
                case "4":
                    Console.WriteLine("GOODBYE!!!");
                    break;
                default:
                    Console.WriteLine("Unknown command. Please, run the program again");
                    break;
            }
        }

        private static void ClearUpDb(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        private static void RunAllSeeders(ApplicationDbContext context)
        {
            // register seeders here
            var seeders = SeederRegistrationConfig.GetSeeders();

            seeders.ForEach(s =>
            {
                string seederName = s.GetType().Name;

                Console.WriteLine($"Running {seederName} ...");
                s.RunSeeding(context);
                Console.WriteLine($"Done {seederName} ...");
                Console.WriteLine(new string('=', 20));
            });
        }
    }
}