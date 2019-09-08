using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using LY.PF.Indents;
using LY.PF.Indents.Authorization;
using LY.PF.Indents.Dtos;
using LY.PF.Web.Areas.Mpa.Models.IndentManage;
using LY.PF.Web.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LY.PF.Web.Areas.Mpa.Controllers
{
    public class IndentManageController : PFControllerBase
    {

        private readonly IIndentAppService _indentAppService;

        public IndentManageController(IIndentAppService indentAppService)
        {
            _indentAppService = indentAppService;
           
        }

        public  ActionResult Index()
        {
		   var model = new GetIndentInput {FilterText = Request.QueryString["filterText"]};
            return View(model);



           
        }

	 

		/// <summary>
        /// 根据id获取进行编辑或者添加的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		[AbpMvcAuthorize(IndentAppPermissions.Indent_CreateIndent,IndentAppPermissions.Indent_EditIndent)]
        public async Task<PartialViewResult> CreateOrEditIndentModal(System.Guid? id)
        {
		var input=new NullableIdDto<System.Guid>{Id=id};
	 
                 var output=    await _indentAppService.GetIndentForEditAsync(input);

				 var viewModel=new CreateOrEditIndentModalViewModel(output);


            return PartialView("_CreateOrEditIndentModal",viewModel);
        }
	 
       
    }
}