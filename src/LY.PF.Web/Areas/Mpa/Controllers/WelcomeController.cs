using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using LY.PF.Web.Controllers;

namespace LY.PF.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class WelcomeController : PFControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}