using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using LY.PF.ProductTypes;
using LY.PF.ProductTypes.Authorization;
using LY.PF.ProductTypes.Dtos;
using LY.PF.Web.Areas.Mpa.Models.ProductTypeManage;
using LY.PF.Web.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LY.PF.Web.Areas.Mpa.Controllers
{
    public class ProductTypeManageController : PFControllerBase
    {

        private readonly IProductTypeAppService _productTypeAppService;

        public ProductTypeManageController(IProductTypeAppService productTypeAppService)
        {
            _productTypeAppService = productTypeAppService;
           
        }

        public  ActionResult Index()
        {
		   var model = new GetProductTypeInput {FilterText = Request.QueryString["filterText"]};
            return View(model);



           
        }

	 

			  /// <summary>
        /// 根据id获取进行编辑或者添加的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		[AbpMvcAuthorize(ProductTypeAppPermissions.ProductType_CreateProductType,ProductTypeAppPermissions.ProductType_EditProductType)]
        public async Task<PartialViewResult> CreateOrEditProductTypeModal(int? id)
        {
		var input=new NullableIdDto<int>{Id=id};
	 
                 var output=    await _productTypeAppService.GetProductTypeForEditAsync(input);

				 var viewModel=new CreateOrEditProductTypeModalViewModel(output);


            return PartialView("_CreateOrEditProductTypeModal",viewModel);
        }
	 
       
    }
}