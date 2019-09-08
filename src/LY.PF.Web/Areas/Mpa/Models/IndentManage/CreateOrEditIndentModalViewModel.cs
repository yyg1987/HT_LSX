using Abp.AutoMapper;
using LY.PF.Indents.Dtos;

namespace LY.PF.Web.Areas.Mpa.Models.IndentManage
{
    /// <summary>
    /// 新建或编辑订单时使用的Viewmodel
    /// </summary>
    [AutoMap(typeof(GetIndentForEditOutput ))]
    public class CreateOrEditIndentModalViewModel : GetIndentForEditOutput
    {
		/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="output"></param>
	   public CreateOrEditIndentModalViewModel(GetIndentForEditOutput output)
        {
            output.MapTo(this);
        }



		 /// <summary>
        /// 是否处于编辑状态
        /// </summary>
	  public bool IsEditMode { get { return Indent.Id.HasValue; } }

	    
		
        /// <summary>
        /// 模糊查询字段
        /// </summary>
        public string FilterText { get; set; }

    }
}
