using System.Web.Mvc;

namespace PetsLife.Controllers
{
    [Authorize]
    public class PetOwnersController : Controller
    {
        // GET: PetOwners
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.OwnerId = id;
            return View();
        }

        [Route("{ownerId:int}")]
        public ActionResult Approval(int ownerId)
        {
            ViewBag.OwnerId = ownerId;
            return View();
        }
    }
}