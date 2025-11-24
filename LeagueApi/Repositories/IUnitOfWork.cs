using System;
using System.Threading.Tasks;
using LeagueApi.Models;

namespace LeagueApi.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Team> Teams { get; }
        IRepository<Match> Matches { get; }
        Task<int> SaveChangesAsync();
    }
}
