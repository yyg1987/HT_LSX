using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using LY.PF.Common;
using LY.PF.ProductTypes;
using LY.PF.SaleOrders;
using LY.PF.SaleOrders.Authorization;
using LY.PF.SaleOrders.Dtos;
using LY.PF.Web.Areas.Mpa.Models.SaleOrderManage;
using LY.PF.Web.Controllers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LY.PF.Web.Areas.Mpa.Controllers
{
    public class SaleOrderManageController : PFControllerBase
    {

        private readonly ISaleOrderAppService _saleOrderAppService;
        private readonly IProductTypeAppService _productTypeAppService;


        public SaleOrderManageController(ISaleOrderAppService saleOrderAppService, IProductTypeAppService productTypeAppService)
        {
            _saleOrderAppService = saleOrderAppService;
            _productTypeAppService = productTypeAppService;


        }

        public  ActionResult Index()
        {
		   var model = new GetSaleOrderInput {FilterText = Request.QueryString["filterText"]};
            ViewBag.ProductTypes = _productTypeAppService.GetProductTypes();
            return View(model);
           
        }

	 

			  /// <summary>
        /// 根据id获取进行编辑或者添加的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		[AbpMvcAuthorize(SaleOrderAppPermissions.SaleOrder_CreateSaleOrder,SaleOrderAppPermissions.SaleOrder_EditSaleOrder)]
        public async Task<PartialViewResult> CreateOrEditSaleOrderModal(System.Guid? id)
        {
		var input=new NullableIdDto<System.Guid>{Id=id};
	 
                 var output=    await _saleOrderAppService.GetSaleOrderForEditAsync(input);

				 var viewModel=new CreateOrEditSaleOrderModalViewModel(output);


            return PartialView("_CreateOrEditSaleOrderModal",viewModel);
        }


        [AbpMvcAuthorize(SaleOrderAppPermissions.SaleOrder_CreateSaleOrder, SaleOrderAppPermissions.SaleOrder_EditSaleOrder)]
        public ActionResult Upload( int productType,string productName, string remark, int status, string isOverride)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productType.ToString()))
                {
                    return Json(new { state = ResultType.error.ToString(), message = "没有选择产品类型" });
                }
                //获取当前文件流
                HttpPostedFileBase hpf = HttpContext.Request.Files["myfile"] as HttpPostedFileBase;
                if (hpf == null || string.IsNullOrWhiteSpace(hpf.FileName))
                {
                    return Json(new { state = ResultType.error.ToString(), message = "没有指定上传的文件" });
                }
                //在指定路径创建子文件夹
                DirectoryInfo di = Directory.CreateDirectory(Server.MapPath(AppConsts.Dir));
                //获取文件名
                string sentFileName = Path.GetFileName(hpf.FileName);
                //全路径
                string savedFileName = Path.Combine(di.FullName, sentFileName);
                //校验后缀
                if (Path.GetExtension(sentFileName).ToLower() != ".jpg" &&Path.GetExtension(sentFileName).ToLower() != ".png"&& Path.GetExtension(sentFileName).ToLower() != ".gif")
                {
                    throw new ArgumentException("上传的文件不是图片");
                }
                //string version = ValidFileVersion(sentFileName);
                //if (version == null)
                //{
                //    throw new ArgumentException("文件名格式不正确");
                //}

                bool bOverride = false;
                if ((isOverride != null) && (isOverride.ToLower() == "on"))
                {
                    bOverride = true;
                }
                else
                {
                    if (System.IO.File.Exists(savedFileName))
                    {
                        return Json(new { state = ResultType.error.ToString(), message = "文件已存在" });
                    }
                }

                hpf.SaveAs(savedFileName);

                //获取请求的域名+协议
                //string authorityPart = Request.Url.GetLeftPart(UriPartial.Authority);
                string rootPart = System.Configuration.ConfigurationManager.AppSettings["Upload"];
                if (string.IsNullOrEmpty(rootPart))
                {
                    throw new Exception("内部配置错误");
                }
                if (!this._saleOrderAppService.Add(productType, productName, remark, status, sentFileName, rootPart))
                {
                    System.IO.File.Delete(savedFileName);
                    return Json(new { state = ResultType.error.ToString(), message = "添加添加产品失败" });
                }

                return Json(new { state = ResultType.success.ToString(), message = "添加产品成功" });
            }
            catch (ArgumentException e)
            {
                var msg = new { state = ResultType.error.ToString(), message = e.Message };
                return Json(msg);
            }
            catch (Exception)
            {
                //TODO: 记录日志
                var msg = new { state = ResultType.error.ToString(), message = "内部错误" };
                return Json(msg);
            }
        }

        [HttpPost]
        public ActionResult Download(Guid keyValue)
        {
            var input = new  EntityDto<System.Guid>{ Id = keyValue };

            var saleOrder = _saleOrderAppService.GetSaleOrderByIdAsync(input).Result;
            if (saleOrder == null)
            {
                return Error("记录不存在");
            }

            string rs = Path.Combine("~/", saleOrder.ImageUrl);
            string srcpath = Server.MapPath(rs);
            if (!System.IO.File.Exists(srcpath))
            {
                return Error("文件不存在");
            }

            string canonicalUrl = saleOrder.ImageUrl.Replace('\\', '/');
            return Success(canonicalUrl);
        }

    }
}