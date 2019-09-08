using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using LY.PF.Web.Areas.Mpa.Models.Common.Modals;
using LY.PF.Web.Controllers;

namespace LY.PF.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class CommonController : PFControllerBase
    {
        public PartialViewResult LookupModal(LookupModalViewModel model)
        {
            return PartialView("Modals/_LookupModal", model);
        }
    }
}