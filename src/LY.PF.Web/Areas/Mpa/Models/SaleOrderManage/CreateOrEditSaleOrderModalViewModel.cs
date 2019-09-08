using Abp.AutoMapper;
using LY.PF.SaleOrders.Dtos;

namespace LY.PF.Web.Areas.Mpa.Models.SaleOrderManage
{
    /// <summary>
    /// 新建或编辑订单时使用的Viewmodel
    /// </summary>
    [AutoMap(typeof(GetSaleOrderForEditOutput ))]
    public class CreateOrEditSaleOrderModalViewModel : GetSaleOrderForEditOutput
    {
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="output"></param>
	   public CreateOrEditSaleOrderModalViewModel(GetSaleOrderForEditOutput output)
        {
            output.MapTo(this);
        }



		 /// <summary>
        /// 是否处于编辑状态
        /// </summary>
	  public bool IsEditMode { get { return SaleOrder.Id.HasValue; } }

	    
		
        /// <summary>
        /// 模糊查询字段
        /// </summary>
        public string FilterText { get; set; }

    }
}
