using Abp.Runtime.Validation;
using LY.PF.Dto;
 

namespace LY.PF.SaleFunnels.Dtos
{
	/// <summary>
    /// 销售漏斗查询Dto
    /// </summary>
    public class GetSaleFunnelInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

		/// <summary>
	    /// 模糊查询参数
		/// </summary>
		public string FilterText { get; set; }

		/// <summary>
	    /// 用于排序的默认值
		/// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
			
		
                Sorting = "Id";
            }
        }
    }
}
