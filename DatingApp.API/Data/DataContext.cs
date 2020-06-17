using DatingApp.API.Models;

using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<ValueModel> Values { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            optionsBuilder
                .UseSqlServer(config.GetConnectionString(nameof(DataContext)))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .EnableSensitiveDataLogging();
        }
    }
}
