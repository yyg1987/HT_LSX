using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace LY.PF.Districts.Dtos
{
	/// <summary>
    /// 用于添加或编辑 订单时使用的DTO
    /// </summary>
  
    public class GetDistrictForEditOutput 
    {
 

	      /// <summary>
         /// District编辑状态的DTO
        /// </summary>
    public DistrictEditDto District{get;set;}
    public List<ComboboxItemDto> ParentDistricts { get; set; }

        public GetDistrictForEditOutput()
        {
            ParentDistricts = new List<ComboboxItemDto>();
        }


    }
}
