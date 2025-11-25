using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueApi.DTOs;

namespace LeagueApi.Services
{
    public interface IRankingService
    {
        Task<IEnumerable<TeamRankingDto>> GetRankingsAsync();
    }
}
