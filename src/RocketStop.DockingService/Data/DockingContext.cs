using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using RocketStop.DockingService.Models;

namespace RocketStop.DockingService.Data
{
    public class DockingContext : DbContext
    {
        public DockingContext(DbContextOptions options) : base(options) { }

        public DbSet<Docking> Dockings { get; set; }
    }

    public class DockingContextFactory : IDbContextFactory<DockingContext>
    {
        public DockingContext Create(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DockingContext>();
            optionsBuilder.UseSqlServer(
                "data source=.;initial catalog=dockingdb;integrated security=false;user id=sa;Password=S3cr3tSqu1rr3l;"
            );
            return new DockingContext(optionsBuilder.Options);
        }
    }

    public class DockingContextInit
    {
        public static void Initialize(string connectionString, ILogger logger)
        {
            TryConnect(connectionString, logger);
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(connectionString);
            using (var context = new DockingContext(optionsBuilder.Options))
            {
                Migrate(context);
                Seed(context);
            }
        }

        private static void TryConnect(string connectionString, ILogger logger)
        {
            // Change the database to master for testing the connection
            var scsb = new SqlConnectionStringBuilder(connectionString);
            scsb.InitialCatalog = "master";
            connectionString = scsb.ToString();

            int retries = 6;
            using (var connection = new SqlConnection(connectionString))
            while (true)
            {
                try
                {
                    logger.LogInformation("Connecting to SQL...");
                    connection.Open();
                    logger.LogInformation("Connected!");
                    return;
                }
                catch
                {
                    if (--retries > 0)
                    {
                        logger.LogWarning("Failed to connect. Retrying in 10 seconds...");
                        Thread.Sleep(TimeSpan.FromSeconds(10));
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private static void Migrate(DockingContext context)
        {
            try
            {
                context.Database.Migrate();
            }
            catch {}
        }

        private static void Seed(DockingContext context)
        {
            if (!context.Dockings.Any())
            {
                context.Add(new Docking {Bay = "A", ShipName = "Heart of Gold"});
                context.Add(new Docking {Bay = "B", ShipName = "Hot Black Desiato's Stunt Ship"});
                context.SaveChanges();
            }
        }
    }

}