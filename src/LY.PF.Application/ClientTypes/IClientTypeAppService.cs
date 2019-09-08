using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LY.PF.ClientTypes.Dtos;
using LY.PF.Dto;

namespace LY.PF.ClientTypes
{
    /// <summary>
    /// 订单服务接口
    /// </summary>
    public interface IClientTypeAppService : IApplicationService
    {
        #region 订单管理

        /// <summary>
        /// 根据查询条件获取订单分页列表
        /// </summary>
        Task<PagedResultDto<ClientTypeListDto>> GetPagedClientTypesAsync(GetClientTypeInput input);

        /// <summary>
        /// 通过Id获取订单信息进行编辑或修改 
        /// </summary>
        Task<GetClientTypeForEditOutput> GetClientTypeForEditAsync(NullableIdDto<System.Int32> input);

        /// <summary>
        /// 通过指定id获取订单ListDto信息
        /// </summary>
        Task<ClientTypeListDto> GetClientTypeByIdAsync(EntityDto<System.Int32> input);



        /// <summary>
        /// 新增或更改订单
        /// </summary>
        Task CreateOrUpdateClientTypeAsync(CreateOrUpdateClientTypeInput input);





        /// <summary>
        /// 新增订单
        /// </summary>
        Task<ClientTypeEditDto> CreateClientTypeAsync(ClientTypeEditDto input);

        /// <summary>
        /// 更新订单
        /// </summary>
        Task UpdateClientTypeAsync(ClientTypeEditDto input);

        /// <summary>
        /// 删除订单
        /// </summary>
        Task DeleteClientTypeAsync(EntityDto<int> input);

        /// <summary>
        /// 批量删除订单
        /// </summary>
        Task BatchDeleteClientTypeAsync(List<int> input);

        #endregion

        #region Excel导出功能



        /// <summary>
        /// 获取订单信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetClientTypeToExcel();

        #endregion





    }
}
