using Abp.AutoMapper;
using LY.PF.ProductTypes.Dtos;

namespace LY.PF.Web.Areas.Mpa.Models.ProductTypeManage
{
    /// <summary>
    /// 新建或编辑订单时使用的Viewmodel
    /// </summary>
    [AutoMap(typeof(GetProductTypeForEditOutput ))]
    public class CreateOrEditProductTypeModalViewModel : GetProductTypeForEditOutput
    {
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="output"></param>
	   public CreateOrEditProductTypeModalViewModel(GetProductTypeForEditOutput output)
        {
            output.MapTo(this);
        }



		 /// <summary>
        /// 是否处于编辑状态
        /// </summary>
	  public bool IsEditMode { get { return ProductType.Id.HasValue; } }

	    
		
        /// <summary>
        /// 模糊查询字段
        /// </summary>
        public string FilterText { get; set; }

    }
}
