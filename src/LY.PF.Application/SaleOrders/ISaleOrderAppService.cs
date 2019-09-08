using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LY.PF.SaleOrders.Dtos;
using LY.PF.Dto;

namespace LY.PF.SaleOrders
{
    /// <summary>
    /// 订单服务接口
    /// </summary>
    public interface ISaleOrderAppService : IApplicationService
    {
        #region 订单管理

        /// <summary>
        /// 根据查询条件获取订单分页列表
        /// </summary>
        Task<PagedResultDto<SaleOrderListDto>> GetPagedSaleOrdersAsync(GetSaleOrderInput input);

        /// <summary>
        /// 通过Id获取订单信息进行编辑或修改 
        /// </summary>
        Task<GetSaleOrderForEditOutput> GetSaleOrderForEditAsync(NullableIdDto<System.Guid> input);

        /// <summary>
        /// 通过指定id获取订单ListDto信息
        /// </summary>
        Task<SaleOrderListDto> GetSaleOrderByIdAsync(EntityDto<System.Guid> input);



        /// <summary>
        /// 新增或更改订单
        /// </summary>
        Task CreateOrUpdateSaleOrderAsync(CreateOrUpdateSaleOrderInput input);





        /// <summary>
        /// 新增订单
        /// </summary>
        Task<SaleOrderEditDto> CreateSaleOrderAsync(SaleOrderEditDto input);

        /// <summary>
        /// 更新订单
        /// </summary>
        Task UpdateSaleOrderAsync(SaleOrderEditDto input);

        /// <summary>
        /// 删除订单
        /// </summary>
        Task DeleteSaleOrderAsync(EntityDto<System.Guid> input);

        /// <summary>
        /// 批量删除订单
        /// </summary>
        Task BatchDeleteSaleOrderAsync(List<System.Guid> input);
        bool Add(int productType, string productName, string remark, int status, string fileName, string authorityPart);


        #endregion

        #region Excel导出功能



        /// <summary>
        /// 获取订单信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetSaleOrderToExcel();

        #endregion





    }
}
