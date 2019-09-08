using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace LY.PF.SaleFunnels.Dtos
{
	/// <summary>
    /// 用于添加或编辑 销售漏斗时使用的DTO
    /// </summary>
  
    public class GetSaleFunnelForEditOutput 
    {
 

	      /// <summary>
         /// SaleFunnel编辑状态的DTO
        /// </summary>
    public SaleFunnelEditDto SaleFunnel{get;set;}
        public List<ComboboxItemDto> ProductTypes { get; set; }
        public List<ComboboxItemDto> Districts { get; set; }
        public List<ComboboxItemDto> ClientTypes { get; set; }

        public GetSaleFunnelForEditOutput()
        {
            ProductTypes = new List<ComboboxItemDto>();
            Districts = new List<ComboboxItemDto>();
            ClientTypes = new List<ComboboxItemDto>();

        }


    }
}
