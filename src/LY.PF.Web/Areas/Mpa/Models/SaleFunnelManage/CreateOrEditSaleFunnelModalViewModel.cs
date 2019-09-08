using Abp.AutoMapper;
using LY.PF.SaleFunnels.Dtos;

namespace LY.PF.Web.Areas.Mpa.Models.SaleFunnelManage
{
    /// <summary>
    /// 新建或编辑销售漏斗时使用的Viewmodel
    /// </summary>
    [AutoMap(typeof(GetSaleFunnelForEditOutput ))]
    public class CreateOrEditSaleFunnelModalViewModel : GetSaleFunnelForEditOutput
    {
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="output"></param>
	   public CreateOrEditSaleFunnelModalViewModel(GetSaleFunnelForEditOutput output)
        {
            output.MapTo(this);
        }



		 /// <summary>
        /// 是否处于编辑状态
        /// </summary>
	  public bool IsEditMode { get { return SaleFunnel.Id.HasValue; } }

	    
		
        /// <summary>
        /// 模糊查询字段
        /// </summary>
        public string FilterText { get; set; }

    }
}
