using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using LY.PF.Authorization;
using LY.PF.Web.Controllers;

namespace LY.PF.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class DashboardController : PFControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}