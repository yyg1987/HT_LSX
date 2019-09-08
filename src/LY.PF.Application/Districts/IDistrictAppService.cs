using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LY.PF.Districts.Dtos;
using LY.PF.Dto;

namespace LY.PF.Districts
{
    /// <summary>
    /// 订单服务接口
    /// </summary>
    public interface IDistrictAppService : IApplicationService
    {
        #region 订单管理

        /// <summary>
        /// 根据查询条件获取订单分页列表
        /// </summary>
        Task<PagedResultDto<DistrictListDto>> GetPagedDistrictsAsync(GetDistrictInput input);

        /// <summary>
        /// 通过Id获取订单信息进行编辑或修改 
        /// </summary>
        Task<GetDistrictForEditOutput> GetDistrictForEditAsync(NullableIdDto<int> input);

        /// <summary>
        /// 通过指定id获取订单ListDto信息
        /// </summary>
        Task<DistrictListDto> GetDistrictByIdAsync(EntityDto<int> input);



        /// <summary>
        /// 新增或更改订单
        /// </summary>
        Task CreateOrUpdateDistrictAsync(CreateOrUpdateDistrictInput input);





        /// <summary>
        /// 新增订单
        /// </summary>
        Task<DistrictEditDto> CreateDistrictAsync(DistrictEditDto input);

        /// <summary>
        /// 更新订单
        /// </summary>
        Task UpdateDistrictAsync(DistrictEditDto input);

        /// <summary>
        /// 删除订单
        /// </summary>
        Task DeleteDistrictAsync(EntityDto<int> input);

        /// <summary>
        /// 批量删除订单
        /// </summary>
        Task BatchDeleteDistrictAsync(List<int> input);

        #endregion

        #region Excel导出功能



        /// <summary>
        /// 获取订单信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetDistrictToExcel();

        #endregion





    }
}
