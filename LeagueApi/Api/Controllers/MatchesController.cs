using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using LeagueApi.Models;
using LeagueApi.Repositories;
using System.Data.Entity;

namespace LeagueApi.Controllers
{
    [RoutePrefix("api/matches")]
    public class MatchesController : ApiController
    {
        private readonly IUnitOfWork _uow;

        public MatchesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var matches = await _uow.Matches.GetAll()
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToListAsync();

            return Ok(matches);
        }

        [HttpGet, Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var match = await _uow.Matches.GetAll()
                 .Include(x => x.HomeTeam)
                 .Include(x => x.AwayTeam)
                 .FirstOrDefaultAsync(x => x.Id == id);

            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Create([FromBody] MatchCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.HomeTeamId == dto.AwayTeamId)
            {
                return BadRequest("Home and away team must be different.");
            }

            var match = new Match
            {
                HomeTeamId = dto.HomeTeamId,
                AwayTeamId = dto.AwayTeamId,
                HomeTeamScore = dto.HomeTeamScore,
                AwayTeamScore = dto.AwayTeamScore,
                PlayedAt = dto.PlayedAt
            };

            await _uow.Matches.AddAsync(match);
            await _uow.SaveChangesAsync();

            return Created($"/api/matches/{match.Id}", match);
        }

        [HttpPut, Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] MatchUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existing = await _uow.Matches.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.HomeTeamId = dto.HomeTeamId;
            existing.AwayTeamId = dto.AwayTeamId;
            existing.HomeTeamScore = dto.HomeTeamScore;
            existing.AwayTeamScore = dto.AwayTeamScore;
            existing.PlayedAt = dto.PlayedAt;

            _uow.Matches.Update(existing);
            await _uow.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete, Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var existing = await _uow.Matches.GetByIdAsync(id);

            if (existing == null)
            {
                return NotFound();
            }

            _uow.Matches.Delete(existing);
            await _uow.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
