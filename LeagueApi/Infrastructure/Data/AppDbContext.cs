using System.Data.Entity;
using LeagueApi.Models;

namespace LeagueApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Migrations.Configuration>());
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.HomeMatches)
                .WithRequired(m => m.HomeTeam)
                .HasForeignKey(m => m.HomeTeamId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.AwayMatches)
                .WithRequired(m => m.AwayTeam)
                .HasForeignKey(m => m.AwayTeamId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>().Property(t => t.Name).IsRequired().HasMaxLength(100);
        }
    }
}
