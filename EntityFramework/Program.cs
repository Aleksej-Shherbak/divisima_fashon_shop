using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EntityFramework
{
    class Program : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var settingPath = Path.GetFullPath(Path.Combine($"../Shared/SharedSettings.{envName}.json"));

            IConfigurationRoot configuration = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile( $"appsettings.{envName}.json")
                .AddJsonFile(settingPath, false)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            builder.UseNpgsql(connectionString, opt =>
            {
                opt.MigrationsAssembly("EntityFramework");
                opt.SetPostgresVersion(10, 10);
            });
            return new ApplicationDbContext(builder.Options);
        }
    }
}