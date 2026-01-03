using FootballManager.Model;
using Microsoft.EntityFrameworkCore;


namespace FootballManager.Data
{
    internal class Context : DbContext
    {
        private readonly string executionPath;
        private readonly string projectPath;
        private readonly string dbPath;

        public DbSet<Player> Players { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Goals> Goals { get; set; }

        public Context()
        {
            executionPath = AppDomain.CurrentDomain.BaseDirectory;
            projectPath = Path.Combine(executionPath, "..", "..", "..");
            dbPath = Path.Combine(projectPath, "liga.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
