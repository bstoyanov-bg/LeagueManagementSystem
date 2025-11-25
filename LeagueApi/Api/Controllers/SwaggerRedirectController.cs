using System.Web.Mvc;

namespace LeagueApi.Controllers
{
    public class SwaggerRedirectController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
