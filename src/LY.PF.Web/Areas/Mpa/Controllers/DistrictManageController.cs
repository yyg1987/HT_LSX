using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using LY.PF.Districts;
using LY.PF.Districts.Authorization;
using LY.PF.Districts.Dtos;
using LY.PF.Web.Areas.Mpa.Models.DistrictManage;
using LY.PF.Web.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LY.PF.Web.Areas.Mpa.Controllers
{
    public class DistrictManageController : PFControllerBase
    {

        private readonly IDistrictAppService _districtAppService;

        public DistrictManageController(IDistrictAppService districtAppService)
        {
            _districtAppService = districtAppService;
           
        }

        public  ActionResult Index()
        {
		   var model = new GetDistrictInput {FilterText = Request.QueryString["filterText"]};
            return View(model);



           
        }

	 

			  /// <summary>
        /// 根据id获取进行编辑或者添加的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		[AbpMvcAuthorize(DistrictAppPermissions.District_CreateDistrict,DistrictAppPermissions.District_EditDistrict)]
        public async Task<PartialViewResult> CreateOrEditDistrictModal(int? id)
        {
		var input=new NullableIdDto<int>{Id=id};
	 
                 var output=    await _districtAppService.GetDistrictForEditAsync(input);

				 var viewModel=new CreateOrEditDistrictModalViewModel(output);


            return PartialView("_CreateOrEditDistrictModal",viewModel);
        }
	 
       
    }
}