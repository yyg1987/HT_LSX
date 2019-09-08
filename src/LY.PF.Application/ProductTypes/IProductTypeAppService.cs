using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LY.PF.ProductTypes.Dtos;
using LY.PF.Dto;

namespace LY.PF.ProductTypes
{
    /// <summary>
    /// 订单服务接口
    /// </summary>
    public interface IProductTypeAppService : IApplicationService
    {
        #region 订单管理

        /// <summary>
        /// 根据查询条件获取订单分页列表
        /// </summary>
        Task<PagedResultDto<ProductTypeListDto>> GetPagedProductTypesAsync(GetProductTypeInput input);

        /// <summary>
        /// 通过Id获取订单信息进行编辑或修改 
        /// </summary>
        Task<GetProductTypeForEditOutput> GetProductTypeForEditAsync(NullableIdDto<int> input);

        /// <summary>
        /// 通过指定id获取订单ListDto信息
        /// </summary>
        Task<ProductTypeListDto> GetProductTypeByIdAsync(EntityDto<int> input);



        /// <summary>
        /// 新增或更改订单
        /// </summary>
        Task CreateOrUpdateProductTypeAsync(CreateOrUpdateProductTypeInput input);





        /// <summary>
        /// 新增订单
        /// </summary>
        Task<ProductTypeEditDto> CreateProductTypeAsync(ProductTypeEditDto input);

        /// <summary>
        /// 更新订单
        /// </summary>
        Task UpdateProductTypeAsync(ProductTypeEditDto input);

        /// <summary>
        /// 删除订单
        /// </summary>
        Task DeleteProductTypeAsync(EntityDto<int> input);

        /// <summary>
        /// 批量删除订单
        /// </summary>
        Task BatchDeleteProductTypeAsync(List<int> input);

        List<ComboboxItemDto> GetProductTypes();


        #endregion

        #region Excel导出功能



        /// <summary>
        /// 获取订单信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetProductTypeToExcel();

        #endregion





    }
}
