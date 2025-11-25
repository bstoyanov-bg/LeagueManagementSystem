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
                
            }).ToList();

            return rankings;
        }
    }
}
