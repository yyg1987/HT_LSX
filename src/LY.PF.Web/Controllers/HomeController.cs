using System.Web.Mvc;

namespace LY.PF.Web.Controllers
{
    public class HomeController : PFControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}