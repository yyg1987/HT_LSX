using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace LY.PF.SaleOrders.Dtos
{
	/// <summary>
    /// 用于添加或编辑 订单时使用的DTO
    /// </summary>
  
    public class GetSaleOrderForEditOutput 
    {
 

	      /// <summary>
         /// SaleOrder编辑状态的DTO
        /// </summary>
    public SaleOrderEditDto SaleOrder{get;set;}
    public List<ComboboxItemDto> ProductTypes { get; set; }
        public GetSaleOrderForEditOutput()
        {
            ProductTypes = new List<ComboboxItemDto>();
        }


    }
}
