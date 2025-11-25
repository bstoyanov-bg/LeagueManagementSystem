using System;
using System.Linq;
using LeagueApi.Models;

namespace LeagueApi.Data
{
    public static class SeedData
    {
        public static void Seed(AppDbContext ctx)
        {
            if (!ctx.Teams.Any())
            {
                var a = new Team { Name = "Red FC", City = "Redville" };
                var b = new Team { Name = "Blue United", City = "Bluecity" };
                var c = new Team { Name = "Green Rovers", City = "Greenville" };
                ctx.Teams.Add(a);
                ctx.Teams.Add(b);
                ctx.Teams.Add(c);
                ctx.SaveChanges();

                ctx.Matches.Add(new Match
                {
                    HomeTeamId = a.Id,
                    AwayTeamId = b.Id,
                    HomeTeamScore = 2,
                    AwayTeamScore = 1,
                    PlayedAt = DateTime.UtcNow.AddDays(-2)
                });

                ctx.Matches.Add(new Match
                {
                    HomeTeamId = b.Id,
                    AwayTeamId = c.Id,
                    HomeTeamScore = 0,
                    AwayTeamScore = 0,
                    PlayedAt = DateTime.UtcNow.AddDays(-1)
                });

                ctx.SaveChanges();
            }
        }
    }
}
