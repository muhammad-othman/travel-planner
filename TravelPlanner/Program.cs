using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Design;
using TravelPlanner.Model;
using Microsoft.EntityFrameworkCore;

namespace TravelPlanner
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TravelPlannerContext>
    {
        public TravelPlannerContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();
            var builder = new DbContextOptionsBuilder<TravelPlannerContext>();
            var connectionString = configuration.GetConnectionString("TravelPlannerConnectionString");
            builder.UseSqlServer(connectionString);
            return new TravelPlannerContext(builder.Options);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(SetupConfiguration)
                .UseStartup<Startup>().UseUrls("http://localhost:7779/")
                .Build();

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            // Removing the default configuration options
            builder.Sources.Clear();

            builder.AddJsonFile("config.json", false, true)
                   .AddEnvironmentVariables();
        }
    }
}
