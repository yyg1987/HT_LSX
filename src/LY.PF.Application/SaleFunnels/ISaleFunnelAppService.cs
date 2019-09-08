using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LY.PF.SaleFunnels.Dtos;
using LY.PF.Dto;

namespace LY.PF.SaleFunnels
{
	/// <summary>
    /// 销售漏斗服务接口
    /// </summary>
    public interface ISaleFunnelAppService : IApplicationService
    {
        #region 销售漏斗管理

        /// <summary>
        /// 根据查询条件获取销售漏斗分页列表
        /// </summary>
        Task<PagedResultDto<SaleFunnelListDto>> GetPagedSaleFunnelsAsync(GetSaleFunnelInput input);

        /// <summary>
        /// 通过Id获取销售漏斗信息进行编辑或修改 
        /// </summary>
        Task<GetSaleFunnelForEditOutput> GetSaleFunnelForEditAsync(NullableIdDto<System.Guid> input);

		  /// <summary>
        /// 通过指定id获取销售漏斗ListDto信息
        /// </summary>
		Task<SaleFunnelListDto> GetSaleFunnelByIdAsync(EntityDto<System.Guid> input);



        /// <summary>
        /// 新增或更改销售漏斗
        /// </summary>
        Task CreateOrUpdateSaleFunnelAsync(CreateOrUpdateSaleFunnelInput input);





        /// <summary>
        /// 新增销售漏斗
        /// </summary>
        Task<SaleFunnelEditDto> CreateSaleFunnelAsync(SaleFunnelEditDto input);

        /// <summary>
        /// 更新销售漏斗
        /// </summary>
        Task UpdateSaleFunnelAsync(SaleFunnelEditDto input);

        /// <summary>
        /// 删除销售漏斗
        /// </summary>
        Task DeleteSaleFunnelAsync(EntityDto<System.Guid> input);

        /// <summary>
        /// 批量删除销售漏斗
        /// </summary>
        Task BatchDeleteSaleFunnelAsync(List<System.Guid> input);

        List<SaleData> GetData(string input);
        SaleModelData GetAreaData(string input);
        SaleModelData GetHospitalData(string input);
        List<SalePieData> GetPieData(string input);
        List<SalePieData> GetAreaPieData(string input);
        List<SalePieData> GetProductTypePieData(string input);

        #endregion

        #region Excel导出功能

        /// <summary>
        /// 获取销售漏斗信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetSaleFunnelToExcel();

        #endregion
    }
}
