using System.Web.Mvc;
using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using LY.PF.Authorization;
using LY.PF.Web.Controllers;

namespace LY.PF.Web.Areas.Mpa.Controllers
{
    [DisableAuditing]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_AuditLogs)]
    public class AuditLogsController : PFControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}