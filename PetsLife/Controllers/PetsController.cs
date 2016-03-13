using System.Web.Mvc;

namespace PetsLife.Controllers
{
    [Authorize]
    public class PetsController : Controller
    {
        // GET: Pets
        public ActionResult Index()
        {
            return View();
        }

        [Route("{ownerId:int}")]
        public ActionResult New(int ownerId)
        {
            ViewBag.OwnerId = ownerId;
            return View();
        }
    }
}