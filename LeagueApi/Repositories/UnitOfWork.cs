using System.Threading.Tasks;
using LeagueApi.Data;
using LeagueApi.Models;

namespace LeagueApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepository<Team> _teams;
        private IRepository<Match> _matches;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Team> Teams => _teams ?? (_teams = new Repository<Team>(_context));
        public IRepository<Match> Matches => _matches ?? (_matches = new Repository<Match>(_context));

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
