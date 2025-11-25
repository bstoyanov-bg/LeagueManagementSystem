using System.Threading.Tasks;
using System.Web.Http;
using LeagueApi.Services;

namespace LeagueApi.Controllers
{
    [RoutePrefix("api/rankings")]
    public class RankingsController : ApiController
    {
        private readonly IRankingService _rankingService;

        public RankingsController(IRankingService rankingService)
        {
            _rankingService = rankingService;
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var ranking = await _rankingService.GetRankingsAsync();

            return Ok(ranking);
        }
    }
}
