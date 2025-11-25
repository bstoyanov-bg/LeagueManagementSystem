using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using LeagueApi.DTOs;
using LeagueApi.Repositories;
using LeagueApi.Scoring;

namespace LeagueApi.Services
{
    public class RankingService : IRankingService
    {
        private readonly IUnitOfWork _uow;
        private readonly IScoringStrategy _scoring;

        public RankingService(IUnitOfWork uow, IScoringStrategy scoring)
        {
            _uow = uow;
            _scoring = scoring;
        }

        public async Task<IEnumerable<TeamRankingDto>> GetRankingsAsync()
        {
            var teams = await _uow.Teams.GetAll().ToListAsync();
            var matches = await _uow.Matches.GetAll()
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToListAsync();

            var rankings = teams.Select(t => new TeamRankingDto
            {
                TeamId = t.Id,
                TeamName = t.Name,
                Played = 0,
                Wins = 0,
                Draws = 0,
                Losses = 0,
                GoalsFor = 0,
                GoalsAgainst = 0,
                Points = 0
            }).ToDictionary(r => r.TeamId);

            foreach (var m in matches)
            {
                if (!rankings.ContainsKey(m.HomeTeamId) || !rankings.ContainsKey(m.AwayTeamId))
                {
                    continue;
                }

                var home = rankings[m.HomeTeamId];
                var away = rankings[m.AwayTeamId];

                home.Played++; 
                away.Played++;

                home.GoalsFor += m.HomeTeamScore;
                home.GoalsAgainst += m.AwayTeamScore;

                away.GoalsFor += m.AwayTeamScore;
                away.GoalsAgainst += m.HomeTeamScore;

                var (homePoints, awayPoints) = _scoring.GetPoints(m.HomeTeamScore, m.AwayTeamScore);

                home.Points += homePoints;
                away.Points += awayPoints;

                if (homePoints > awayPoints)
                {
                    home.Wins++; away.Losses++;
                }
                else if (homePoints < awayPoints)
                {
                    away.Wins++; home.Losses++;
                }
                else
                {
                    home.Draws++; away.Draws++;
                }
            }

            return rankings.Values
                .OrderByDescending(r => r.Points)
                .ThenByDescending(r => r.GoalDifference)
                .ThenByDescending(r => r.GoalsFor)
                .ToList();
        }
    }
}
