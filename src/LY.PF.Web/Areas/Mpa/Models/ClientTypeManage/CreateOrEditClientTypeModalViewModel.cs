using Abp.AutoMapper;
using LY.PF.ClientTypes.Dtos;

namespace LY.PF.Web.Areas.Mpa.Models.ClientTypeManage
{
    /// <summary>
    /// 新建或编辑订单时使用的Viewmodel
    /// </summary>
    [AutoMap(typeof(GetClientTypeForEditOutput ))]
    public class CreateOrEditClientTypeModalViewModel : GetClientTypeForEditOutput
    {
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="output"></param>
	   public CreateOrEditClientTypeModalViewModel(GetClientTypeForEditOutput output)
        {
            output.MapTo(this);
        }



		 /// <summary>
        /// 是否处于编辑状态
        /// </summary>
	  public bool IsEditMode { get { return ClientType.Id.HasValue; } }

	    
		
        /// <summary>
        /// 模糊查询字段
        /// </summary>
        public string FilterText { get; set; }

    }
}
