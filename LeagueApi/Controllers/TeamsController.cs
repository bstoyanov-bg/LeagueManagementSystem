using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using LeagueApi.Models;
using LeagueApi.Repositories;
using System.Data.Entity;

namespace LeagueApi.Controllers
{
    [RoutePrefix("api/teams")]
    public class TeamsController : ApiController
    {
        private readonly IUnitOfWork _uow;

        public TeamsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            var teams = await _uow.Teams.GetAll().ToListAsync();

            return Ok(teams);
        }

        [HttpGet, Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var team = await _uow.Teams.GetByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Create([FromBody] TeamCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var team = new Team
            {
                Name = dto.Name,
                City = dto.City
            };

            await _uow.Teams.AddAsync(team);
            await _uow.SaveChangesAsync();

            return Created($"/api/teams/{team.Id}", team);
        }

        [HttpPut, Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] TeamUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existing = await _uow.Teams.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = dto.Name;
            existing.City = dto.City;

            _uow.Teams.Update(existing);
            await _uow.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete, Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var existing = await _uow.Teams.GetByIdAsync(id);

            if (existing == null)
            {
                return NotFound();
            }

            _uow.Teams.Delete(existing);
            await _uow.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
