using System;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApp.Data;

namespace WebAppTests.Fixtures
{
    public class Fixture
    {
        private static readonly int _seed;

        static Fixture()
        {
            _seed = GetSeed();
            Configuration = GetConfiguration();
            CreateDatabase();
        }

        private static int GetSeed()
        {
            var randTick = DateTime.Now.Ticks & 0x0000FFFF;
            var seed = int.TryParse(Environment.GetEnvironmentVariable("TEST_SEED"), out var seedOverride) 
                ? seedOverride 
                : (int)randTick;

            Console.WriteLine($"Using test seed: {seed}");
            return seed;
        }

        private static IConfiguration GetConfiguration() 
            => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        private static void CreateDatabase()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(Configuration["DbConnection"])
                .Options;
            new AppDbContext(options).Database.Migrate();
        }

        public Fixture()
        {
            Randomizer.Seed = new Random(_seed);
        }

        protected static IConfiguration Configuration { get; }
    }
}