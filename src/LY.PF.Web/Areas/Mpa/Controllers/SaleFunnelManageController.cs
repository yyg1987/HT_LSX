using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using LY.PF.SaleFunnels;
using LY.PF.SaleFunnels.Authorization;
using LY.PF.SaleFunnels.Dtos;
using LY.PF.Web.Areas.Mpa.Models.SaleFunnelManage;
using LY.PF.Web.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LY.PF.Web.Areas.Mpa.Controllers
{
    public class SaleFunnelManageController : PFControllerBase
    {

        private readonly ISaleFunnelAppService _saleFunnelAppService;

        public SaleFunnelManageController(ISaleFunnelAppService saleFunnelAppService)
        {
            _saleFunnelAppService = saleFunnelAppService;

        }

        public ActionResult Index()
        {
            var model = new GetSaleFunnelInput { FilterText = Request.QueryString["filterText"] };
            return View(model);




        }



        /// <summary>
        /// 根据id获取进行编辑或者添加的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpMvcAuthorize(SaleFunnelAppPermissions.SaleFunnel_CreateSaleFunnel, SaleFunnelAppPermissions.SaleFunnel_EditSaleFunnel)]
        public async Task<PartialViewResult> CreateOrEditSaleFunnelModal(System.Guid? id)
        {
            var input = new NullableIdDto<System.Guid> { Id = id };

            var output = await _saleFunnelAppService.GetSaleFunnelForEditAsync(input);

            var viewModel = new CreateOrEditSaleFunnelModalViewModel(output);


            return PartialView("_CreateOrEditSaleFunnelModal", viewModel);
        }


    }
}