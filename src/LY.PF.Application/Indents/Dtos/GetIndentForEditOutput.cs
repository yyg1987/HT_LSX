using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace LY.PF.Indents.Dtos
{
	/// <summary>
    /// 用于添加或编辑 订单时使用的DTO
    /// </summary>
  
    public class GetIndentForEditOutput 
    {
 

	      /// <summary>
         /// Indent编辑状态的DTO
        /// </summary>
    public IndentEditDto Indent{get;set;}

        public List<ComboboxItemDto> ProductTypes { get; set; }
        public GetIndentForEditOutput()
        {
            ProductTypes = new List<ComboboxItemDto>();
        }

    }
}
