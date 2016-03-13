using System.Web.Mvc;

namespace PetsLife.Controllers
{
    [Authorize]
    public class PetWalkersController : Controller
    {
        // GET: PetWalkers
        public ActionResult Index()
        {
            return View();
        }
    }
}