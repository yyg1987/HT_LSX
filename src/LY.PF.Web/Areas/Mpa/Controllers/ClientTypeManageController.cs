using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using LY.PF.ClientTypes;
using LY.PF.ClientTypes.Authorization;
using LY.PF.ClientTypes.Dtos;
using LY.PF.Web.Areas.Mpa.Models.ClientTypeManage;
using LY.PF.Web.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LY.PF.Web.Areas.Mpa.Controllers
{
    public class ClientTypeManageController : PFControllerBase
    {

        private readonly IClientTypeAppService _clientTypeAppService;

        public ClientTypeManageController(IClientTypeAppService clientTypeAppService)
        {
            _clientTypeAppService = clientTypeAppService;
           
        }

        public  ActionResult Index()
        {
		   var model = new GetClientTypeInput {FilterText = Request.QueryString["filterText"]};
            return View(model);



           
        }

	 

			  /// <summary>
        /// 根据id获取进行编辑或者添加的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		[AbpMvcAuthorize(ClientTypeAppPermissions.ClientType_CreateClientType,ClientTypeAppPermissions.ClientType_EditClientType)]
        public async Task<PartialViewResult> CreateOrEditClientTypeModal(int? id)
        {
		var input=new NullableIdDto<int>{Id=id};
	 
                 var output=    await _clientTypeAppService.GetClientTypeForEditAsync(input);

				 var viewModel=new CreateOrEditClientTypeModalViewModel(output);


            return PartialView("_CreateOrEditClientTypeModal",viewModel);
        }
	 
       
    }
}