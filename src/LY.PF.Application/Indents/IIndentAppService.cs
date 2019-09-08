using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LY.PF.Indents.Dtos;
using LY.PF.Dto;

namespace LY.PF.Indents
{
    /// <summary>
    /// 订单服务接口
    /// </summary>
    public interface IIndentAppService : IApplicationService
    {
        #region 订单管理

        /// <summary>
        /// 根据查询条件获取订单分页列表
        /// </summary>
        Task<PagedResultDto<IndentListDto>> GetPagedIndentsAsync(GetIndentInput input);

        /// <summary>
        /// 通过Id获取订单信息进行编辑或修改 
        /// </summary>
        Task<GetIndentForEditOutput> GetIndentForEditAsync(NullableIdDto<System.Guid> input);

        /// <summary>
        /// 通过指定id获取订单ListDto信息
        /// </summary>
        Task<IndentListDto> GetIndentByIdAsync(EntityDto<System.Guid> input);



        /// <summary>
        /// 新增或更改订单
        /// </summary>
        Task CreateOrUpdateIndentAsync(CreateOrUpdateIndentInput input);





        /// <summary>
        /// 新增订单
        /// </summary>
        Task<IndentEditDto> CreateIndentAsync(IndentEditDto input);

        /// <summary>
        /// 更新订单
        /// </summary>
        Task UpdateIndentAsync(IndentEditDto input);

        /// <summary>
        /// 删除订单
        /// </summary>
        Task DeleteIndentAsync(EntityDto<System.Guid> input);

        /// <summary>
        /// 批量删除订单
        /// </summary>
        Task BatchDeleteIndentAsync(List<System.Guid> input);

        #endregion

        #region Excel导出功能



        /// <summary>
        /// 获取订单信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetIndentToExcel();

        #endregion





    }
}
