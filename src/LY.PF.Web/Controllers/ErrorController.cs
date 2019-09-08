using System.Web.Mvc;
using Abp.Auditing;

namespace LY.PF.Web.Controllers
{
    public class ErrorController : PFControllerBase
    {
        [DisableAuditing]
        public ActionResult E404()
        {
            return View();
        }
    }
}