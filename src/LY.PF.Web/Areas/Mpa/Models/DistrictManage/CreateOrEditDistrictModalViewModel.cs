using Abp.AutoMapper;
using LY.PF.Districts.Dtos;

namespace LY.PF.Web.Areas.Mpa.Models.DistrictManage
{
    /// <summary>
    /// 新建或编辑订单时使用的Viewmodel
    /// </summary>
    [AutoMap(typeof(GetDistrictForEditOutput ))]
    public class CreateOrEditDistrictModalViewModel : GetDistrictForEditOutput
    {
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="output"></param>
	   public CreateOrEditDistrictModalViewModel(GetDistrictForEditOutput output)
        {
            output.MapTo(this);
        }



		 /// <summary>
        /// 是否处于编辑状态
        /// </summary>
	  public bool IsEditMode { get { return District.Id.HasValue; } }

	    
		
        /// <summary>
        /// 模糊查询字段
        /// </summary>
        public string FilterText { get; set; }

    }
}
